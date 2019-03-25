using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Formaters
{
	public class DataContractSerializerExample
	{

		public void DoIt()
		{
			var p = new PersonDCS() {FirstName = "Asad"};
			FileStream writer = new FileStream("datacontractxmlserializer.xml", FileMode.Create);
			DataContractSerializer ser = new DataContractSerializer(typeof(PersonDCS));
			ser.WriteObject(writer, p);
			writer.Close();

			Console.WriteLine("Serialization done press enter to deserialize");
			Console.ReadLine();

			DataContractSerializer dcs = new DataContractSerializer(typeof(PersonDCS));
			FileStream fs = new FileStream("datacontractxmlserializer.xml", FileMode.Open);
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());

			PersonDCS games = (PersonDCS) dcs.ReadObject(reader);
			Console.WriteLine(games.LastName + "  " + games.FirstName);
			reader.Close();
			fs.Close();
			Console.ReadLine();

		}

	}

	[DataContract(Name = "Individual")]
	public class PersonDCS

	{
		[DataMember] public string FirstName { get; set; }

		[DataMember(EmitDefaultValue = false)] public string LastName { get; set; }



	}
}