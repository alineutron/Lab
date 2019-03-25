using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Formaters
{
	/// <summary> 
	/// The NetDataContractSerializer differs from the DataContractSerializer in one important way: the NetDataContractSerializer  
	/// includes CLR type information in the serialized XML, whereas the DataContractSerializer does not. Therefore, the NetDataContractSerializer 
	///  can be used only if both the serializing and deserializing ends share the same CLR types. 
	/// </summary> 

	public class NetDataContractSerializerExample
	{
		public void DoIt()
		{

			//var p = new PersonNdcs("Asad", "Mirza"); 
			var p = new PersonNdcs() {LastName = "Mirza", FirstName = "asad"};
			FileStream fs = new FileStream("NetDataContractSerializerExample.xml", FileMode.Create);
			XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
			NetDataContractSerializer ser = new NetDataContractSerializer();
			ser.WriteObject(writer, p);
			writer.Close();
			fs.Close();
			Console.WriteLine("Serialization done press enter to deserialize");
			Console.ReadLine();

			fs = new FileStream("NetDataContractSerializerExample.xml", FileMode.Open);
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
			ser = new NetDataContractSerializer();

			// Deserialize the data and read it from the instance. 
			PersonNdcs deserializedPerson = (PersonNdcs) ser.ReadObject(reader, true);
			fs.Close();

			Console.WriteLine($"{deserializedPerson.FirstName}     {deserializedPerson.LastName}");
			Console.ReadLine();
		}

	}

	[DataContract(Name = "Customer", Namespace = "http://www.contoso.com")]
	public class PersonNdcs
	{

		[DataMember()] public string FirstName;

		[DataMember] public string LastName;
	}
}