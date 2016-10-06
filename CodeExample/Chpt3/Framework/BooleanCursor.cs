using System;
using System.Collections;
using System.IO;

namespace DCE
{
	public class BooleanCursor 
	{
		private TextReader _dataReader;
		private string[] _data;
		private int _readCounter = 0;

		public BooleanCursor(TextReader dataSource)
		{
			_dataReader = dataSource;
			_data = new string[2];
			_readCounter = 1;
		}	

		public TextReader BaseReader
		{
			get{return _dataReader;}
		}

		public string Previous()
		{
			_readCounter = 0 ;
			return _data[_readCounter];
		}

		public string Next()
		{
			if ( _readCounter == 0 ) 
			{
				_readCounter = 1;
			}
			else
			{
				_readCounter = 1;
				_data[0] = _data[1];
				_data[1] = _dataReader.ReadLine();
			}
			return _data[_readCounter];
		}
	}
}
