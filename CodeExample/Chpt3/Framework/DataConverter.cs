using System;
using System.IO;
using System.Xml.Serialization;
using DCE.Repository;
using DCE.Parser;
using DCE;
using DCE.Writer;
using System.Xml;
using System.Xml.Schema;
using System.Configuration;

namespace DCE
{
	public class DataConverter
	{
		private Matrix  dceSchema;
		private IWriter  dataWriter;
		private BooleanCursor  dataReader;
		private string ruleFile;
		private string ruleSchema;

		public DataConverter(string rulePath,string ruleSchemaPath)
		{
			//Rule file is validated with a framework schema file 
			//that checks for well-formed characteristics and conformity, 
			//ensuring that all mandatory attributes/elements are present 
			//and arranged in a defined order. 
			ruleFile = rulePath;
			ruleSchema = ruleSchemaPath;

			XmlTextReader xmlRule = new XmlTextReader(ruleFile);
			XmlValidatingReader xsdSchema = new XmlValidatingReader(xmlRule);
			xsdSchema.ValidationEventHandler +=new ValidationEventHandler(xsdSchema_ValidationEventHandler);

			xsdSchema.Schemas.Add("", ruleSchema);
			while(xsdSchema .Read()){}

			xsdSchema.Close();
			xmlRule.Close();

			//Rules stored in XML file are de-hydrated 
			//in an object representation format. 
			FileStream schemaStream = new FileStream(rulePath, FileMode.Open);
			XmlSerializer schemaSz = new XmlSerializer(typeof(Matrix));
			dceSchema = (Matrix)schemaSz.Deserialize(schemaStream );
			schemaStream.Close();

			//This loop invokes the AssignIndex function that 
			//assigns a running sequence number to every instance of Band, Row and 
			//Column objects. This sequence number is assigned recursively to Index  
			//property of CellsAttribute. There is no way to capture this information 
			//during de-serialization stage therefore it needs to be manually assigned.
			foreach ( Band curBand in dceSchema.Bands)
			{
				AssignIndex(curBand);
			}

		}

		public void Convert(BooleanCursor reader,IWriter writer)
		{
			dataWriter = writer;
			dataReader = reader;
			
			//Parsing kicks off with the invocation of this method. 
			//Parsing code has been packaged inside ConvertBand, ConvertRow and 
			//ConvertCol method. These methods instantiate an appropriate parser and 
			//based on the return value of Parse method it invokes 
			//Writer WriteXXX method. 
			foreach ( Band curBand in  dceSchema.Bands ) 
			{
				ConvertBand(curBand, dataReader.Next());
			}

			//Close the underlying reader and writer
			dataReader.BaseReader.Close();
			dataWriter.BaseWriter.Close();
		}


		private void ConvertBand(Band band,string data)
		{
			//This method is responsible for initiating parsing of 
			//band section. A new instance of band parser is created
			//by passing the current data and band information
			BandParser bandParser;
			bandParser = new BandParser( dataReader,data,band);
			//loop till data contains appropriate band identifer
			while ( bandParser.Parse()== true ) 
			{
				//invoke the writer band start method
				dataWriter.WriteStartBand(band,bandParser.Data);
				//iterate through individual row of band
				foreach ( Row row in band.Rows)
				{
					//a row can be child band
					//if it is then a recursive call to CovertBand
					//is triggered 
					if ( row.ChildBand != null ) 
						ConvertBand	(row.ChildBand,data);
					else
						ConvertRow(row,data);
					//get the next data
					data =  dataReader.Next();
				}
				//invoke the writer band end method
				dataWriter.WriteEndBand(band);
				bandParser.Data = data;
			}
		}

		private void ConvertRow(Row row,string data)
		{
			//This method is responsible for initiating parsing of 
			//row section. A new instance of row parser is created
			//by passing the current data and row information
			RowParser rowParser = new RowParser( dataReader,data,row);
			//invoke the writer band start method
			dataWriter.WriteStartRow(row, rowParser.Data);
			//invoke row parser 
			if (  rowParser.Parse() == false ) 
			{
				//if there is no matching data found based on row identifier
				//then bypass the column processing and invoke 
				//the writer row end method
				dataWriter.WriteEndRow(row);
				return ;
			}
			//initiate the column parsing
			ColumnParser colParser  = new ColumnParser(dataReader);
			
			//iterate through individual columns 
			//and process the column level information
			foreach ( Column col in row.Columns)
			{
				colParser.Data = data;
				colParser.CellsAttribute = col;
				ConvertCol(row,col,data,colParser);
			}
			//invoke the writer row end method
			dataWriter.WriteEndRow(row);

		}

		private void AssignIndex(Band curBand)
		{
			if ( curBand == null ) return ;

			int rowIndex;
			int colIndex;
			rowIndex=1;
			foreach(Row curRow in curBand.Rows)
			{
				curRow.Index = rowIndex;
				curRow.ParentCell= curBand;
				AssignIndex(curRow.ChildBand);
				colIndex=1;
				foreach(Column curCol in curRow.Columns)
				{
					curCol.Index = colIndex;
					curCol.ParentCell= curRow;
					colIndex++;
				}
				rowIndex++;
			}
		}

		private void ConvertCol(Row row,Column col,string data,ColumnParser colParser)
		{
			//This method is responsible for initiating parsing of 
			//column section
			colParser.Parse();
			//invoke writer column start and end method
			dataWriter.WriteStartColumn(col, colParser.Data);
			dataWriter.WriteEndColumn(col);;			
		}

		private void xsdSchema_ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			throw new ApplicationException(e.Message);			
		}
	}
}
