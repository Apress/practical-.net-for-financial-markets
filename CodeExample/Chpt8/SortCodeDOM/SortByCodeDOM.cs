using System;
using System.Reflection;
using System.Collections;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Text;
using System.IO;

	public class SortByCodeDOM
	{
		string fldName;
		public SortByCodeDOM(string fld)
		{
			fldName=fld;
		}

		public IComparer GetComparer()
		{
			Console.WriteLine("---------------------------------");
			Console.WriteLine("Creating CodeDOM object graph...");
			CodeCompileUnit compileUnit = new CodeCompileUnit();

			//Step1 - create a new namespace
			CodeNamespace newNameSpace = new
				CodeNamespace("SorterAssembly");
			

			//Step2 - Import namespaces
			newNameSpace.Imports.Add(new
				CodeNamespaceImport("System"));
			newNameSpace.Imports.Add(new
				CodeNamespaceImport("System.Collections"));
			newNameSpace.Imports.Add(new
				CodeNamespaceImport("SharedAssembly"));
			compileUnit.Namespaces.Add(newNameSpace);

			//Step3 - Defines new Type
			CodeTypeDeclaration newType = new
				CodeTypeDeclaration("SortCode");
			newType.BaseTypes.Add(typeof(IComparer));
			newNameSpace.Types.Add(newType);

			//Step4 - Declare Method
			CodeMemberMethod compareMethod = new CodeMemberMethod();
			compareMethod.ReturnType = new CodeTypeReference(typeof(
				int));
			compareMethod.Name = "Compare";
			compareMethod.Attributes = MemberAttributes.Public |
				MemberAttributes.Final;
			newType.Members.Add(compareMethod);

			//Step5 - Declare Parameters
			CodeParameterDeclarationExpression param1 = new
				CodeParameterDeclarationExpression(typeof(object),"x");
			CodeParameterDeclarationExpression param2 = new
				CodeParameterDeclarationExpression(typeof(object),"y");
			compareMethod.Parameters.Add(param1);
			compareMethod.Parameters.Add(param2);

			//Step6 - Populate Method Body
			//Declare the Variable
			CodeVariableDeclarationStatement leftObj = new
				CodeVariableDeclarationStatement("StockData","leftObj");
			CodeVariableDeclarationStatement rightObj = new
				CodeVariableDeclarationStatement("StockData","rightObj");
			compareMethod.Statements.Add(leftObj);
			compareMethod.Statements.Add(rightObj);


			//Cast x argument 
			CodeCastExpression leftcastExp = new
				CodeCastExpression("StockData",new CodeVariableReferenceExpression("x"));
			CodeAssignStatement leftcastStmt = new
				CodeAssignStatement(new
				CodeVariableReferenceExpression("leftObj"),leftcastExp);
			compareMethod.Statements.Add(leftcastStmt );

			//Cast y argument 
			CodeCastExpression rightcastExp = new
				CodeCastExpression("StockData",new CodeVariableReferenceExpression("y"));
			CodeAssignStatement rightcastStmt = new
				CodeAssignStatement(new
				CodeVariableReferenceExpression("rightObj"),rightcastExp);
			compareMethod.Statements.Add(rightcastStmt);

			//Compare both field value and return the result
			CodePropertyReferenceExpression leftExp= new
				CodePropertyReferenceExpression(new
				CodeVariableReferenceExpression("leftObj"),fldName);
			CodePropertyReferenceExpression rightExp = new
				CodePropertyReferenceExpression(new
				CodeVariableReferenceExpression("rightObj"),fldName);
			CodeMethodInvokeExpression methodExp = new
				CodeMethodInvokeExpression(leftExp,"CompareTo",rightExp);
			CodeMethodReturnStatement retStmt = new
				CodeMethodReturnStatement(methodExp);
			compareMethod.Statements.Add(retStmt);

			Console.WriteLine("Translating CodeDOM object graph into text...");	
			//create c-sharp code provider
			CodeDomProvider csharpProv = new CSharpCodeProvider();
			ICodeGenerator csharpCodeGen = csharpProv.CreateGenerator();
			StringBuilder builder = new StringBuilder();
			StringWriter writer = new StringWriter(builder);
			//code generation option
			CodeGeneratorOptions opt = new CodeGeneratorOptions();
			//convert code-dom graph into source code
			csharpCodeGen.GenerateCodeFromCompileUnit(compileUnit,writer,opt);
			Console.WriteLine("Translating CodeDOM object graph into assembly...");	
			//create c-sharp compiler
			ICodeCompiler csharpCompiler = csharpProv.CreateCompiler();
			CompilerParameters param = new CompilerParameters(new string[]{"System.dll","SharedAssembly.dll"});
			param.GenerateExecutable=false;
			param.GenerateInMemory=true;
			//compile the source code
			CompilerResults results =  csharpCompiler.CompileAssemblyFromDom(param,compileUnit);
			//check for any errors
			if  ( results.Errors.Count > 0 ) return null;
			//create instance of IComparer 
			IComparer comparer=results.CompiledAssembly.CreateInstance("SorterAssembly.SortCode") as IComparer;
			return comparer;
		}
	}
