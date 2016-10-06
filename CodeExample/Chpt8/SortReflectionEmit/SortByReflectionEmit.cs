using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using SharedAssembly;

	public class SortByReflectionEmit
	{
		string fldName;
		public SortByReflectionEmit(string fld)
		{
			fldName = fld;
		}

		public IComparer GetComparer()
		{
			AssemblyName asmName = new AssemblyName();
			asmName.Name = "SorterAssembly";
      
			//Define a new in-memory assembly
			AssemblyBuilder asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly
				(asmName,AssemblyBuilderAccess.Run);
      
			//Define a new module
			ModuleBuilder modBuilder = asmBuilder.DefineDynamicModule("SorterModule");
      
			//Create a new Type and implement IComparer interface
			TypeBuilder typeBuilder = modBuilder.DefineType
				("SortCode",TypeAttributes.Public);
			typeBuilder.AddInterfaceImplementation(typeof(IComparer));

			//Create Compare Method with 2 input arguments and also declare return type   
			//as int
			MethodBuilder methodBuilder = typeBuilder.DefineMethod
											("Compare",MethodAttributes.Public | MethodAttributes.Virtual,typeof(int),
											new Type[] {typeof(object),typeof(object)});
      
			//Implements IComparer Compare Method
			MethodInfo compareMethod = typeof(IComparer).GetMethod("Compare");
			typeBuilder.DefineMethodOverride(methodBuilder,compareMethod);

			//Generate IL code for the above declared method
			ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      
      
			//Declare two local variables i.e. leftObj and rightObj
			ilGenerator.DeclareLocal(typeof(StockData));
			ilGenerator.DeclareLocal(typeof(StockData));
			//Declare local variable to hold result returned by CompareTo method 
			ilGenerator.DeclareLocal(typeof(int));

			//Cast x object to StockData type and store it inside local variable
			ilGenerator.Emit(OpCodes.Ldarg_1);
			ilGenerator.Emit(OpCodes.Isinst,typeof(StockData));
			ilGenerator.Emit(OpCodes.Stloc_0);
      
			//Cast y object to StockData type and store it inside local variable
			ilGenerator.Emit(OpCodes.Ldarg_2);
			ilGenerator.Emit(OpCodes.Isinst,typeof(StockData));
			ilGenerator.Emit(OpCodes.Stloc_1);
    
			//Access field of x object using reflection
			FieldInfo xField = typeof(StockData).GetField(fldName);

			//Access the field of x object
			ilGenerator.Emit(OpCodes.Ldloc_0);
      
			if ( xField.FieldType.IsValueType == true ) 
				ilGenerator.Emit(OpCodes.Ldflda,xField);
			else        
				ilGenerator.Emit(OpCodes.Ldfld,xField);
      
			//Access field of y object using reflection
			FieldInfo yField = typeof(StockData).GetField(fldName);

			//Access the field of y object
			ilGenerator.Emit(OpCodes.Ldloc_1);
			ilGenerator.Emit(OpCodes.Ldfld,yField);

			//Boxing Operation in case field value returns a value type
			if ( yField.FieldType.IsValueType == true ) 
			{
				ilGenerator.Emit(OpCodes.Box,yField.FieldType);
			}
      
			//Invoke Compare Method and return the comparison result
			MethodInfo invokeCompare = yField.FieldType.GetMethod("CompareTo",
				new Type[]{typeof(object)});
			ilGenerator.Emit(OpCodes.Call,invokeCompare);
			ilGenerator.Emit(OpCodes.Stloc_2);
      
			Label codeBranch = ilGenerator.DefineLabel();
			ilGenerator.Emit(OpCodes.Br_S,codeBranch);
			ilGenerator.MarkLabel(codeBranch);
			ilGenerator.Emit(OpCodes.Ldloc_2);
			ilGenerator.Emit(OpCodes.Ret);

			//Create the Type
			typeBuilder.CreateType();
      
			//Instantiate the dynamic type 
			IComparer comparer=  asmBuilder.CreateInstance("SortCode",true) 
				as IComparer;
			return comparer;
		}
	} 
