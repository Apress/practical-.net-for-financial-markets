using System;
using System.Reflection;

	class Reflector
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter name of the type to search for : " );
			//Prompt for type name
			string typeName = Console.ReadLine();
			//Initiate the search
			SearchType(typeName);
		}

		public static void SearchType(string typeName)
		{
			//iterate thru assembly
			foreach(Assembly curAssem in AppDomain.CurrentDomain.GetAssemblies())
			{
				//iterate thru module 
				foreach(Module curModule in curAssem.GetModules())
				{
					//iterate thru type
					foreach(Type curType in curModule.GetTypes())
					{
						
						if ( curType.Name == typeName ) 
						{
							Console.WriteLine("Found inside Assembly : " +curAssem.FullName);
							//on successful search, display the type information
							RetrieveTypeInfo(curType);
							break;
						}
					}
				}
			}
		}

		public static void RetrieveTypeInfo(Type type)
		{
			//display all methods defined in this type
			Console.WriteLine("Type Full Name : " +type.FullName);
			Console.WriteLine("List of Methods");
			Console.WriteLine("----------------");
			foreach(MethodInfo curMethod in type.GetMethods())
			{
				Console.WriteLine(curMethod.Name);
			}

			//display properties defined in this type
			Console.WriteLine("List of Properties");
			Console.WriteLine("------------------");
			foreach(PropertyInfo propInfo in type.GetProperties())
			{
				Console.WriteLine(propInfo.Name);
			}

			//display fields defined in this type
			Console.WriteLine("List of Fields");
			Console.WriteLine("--------------");
			foreach(FieldInfo fldInfo in type.GetFields())
			{
				Console.WriteLine(fldInfo.Name);
			}



		}
	}

