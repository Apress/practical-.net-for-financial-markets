using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace STP.Security
{
	class CodeExample
		{
		[STAThread]
		static void Main(string[] args)
		{
			//An instance of ContractNoteInfo is created. 
			ContractNoteInfo noteInfo = new ContractNoteInfo("MSFT",100,24);
			//ContractNoteInfo is decorated with Serializable attribute, 
			//so the entire object graph with help of BinaryFormatter is 
			//flattened into raw bytes and this task is achieved by 
			//with help of SerializeContractNote method
			byte[] data = SerializeContractNote(noteInfo);
			//Generate public and private key for demonstration purpose
			GenerateKey();
			//Security Framework is initialized and a new instance of DataSecurity 
			//is created and this instance returned by DataSecurityManager 
			//is exclusively meant for instances of ContractNoteInfo. This 
			//behavior is similar to XmlSerializer where there exists strong 
			//coupling between an object instance and the type associated with it.
			DataSecurityManager secMgr = new DataSecurityManager();
			DataSecurity dataSec = secMgr.Secure(typeof(ContractNoteInfo));
			//The serialized byte array of ContractNoteInfo is then passed to 
			//Create method of DataSecurity that is then handed internally to 
			//NonRepudiationProvider which creates digital signature and 
			//associates it with SecureEnvelope.  Also, secure envelope itself 
			//is marked serializable so its entire object graph itself can now 
			//be serialized and transmitted over wire. 
			SecureEnvelope secureEnvelope = dataSec.Create(data);
			Console.WriteLine("Secure Envelope successfully created..");
			Console.ReadLine();
		}

		public static void GenerateKey()
		{
			RSACryptoServiceProvider rsaCrypto = new RSACryptoServiceProvider();
			string pubprivKey = rsaCrypto.ToXmlString(true);
			StreamWriter writer = new StreamWriter(@"C:\PubPrivKey.txt");
			writer.WriteLine(pubprivKey);
			writer.Close();
		}

		public static byte[] SerializeContractNote(ContractNoteInfo noteInfo)
		{
			MemoryStream memStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memStream,noteInfo);
			int dataLength = (int)memStream.Length;
			byte[] data = new byte[dataLength];
			memStream.Position = 0;
			memStream.Read(data,0,dataLength);
			memStream.Close();
			return data;
		}
	}
}
