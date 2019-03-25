using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Formaters
{
	public class XmlSerilizerExample
	{

		public void DoIt()
		{
			//var p = new PersonNdcs("Asad", "Mirza"); 
			var p = new PersonXml() {LastName = "Mirza", FirstName = "asad"};
			XmlSerializer serializer =
				new XmlSerializer(typeof(PersonXml));
			Stream writer = new FileStream("xmlserializerExample.xml", FileMode.Create);
			// Serialize the object, and close the TextWriter 
			serializer.Serialize(writer, p);
			writer.Close();

			Console.WriteLine("Serialization done press enter to deserialize");
			Console.ReadLine();

			PersonXml i;
			using (Stream reader = new FileStream("xmlserializerExample.xml", FileMode.Open))
			{
				i = (PersonXml) serializer.Deserialize(reader);
			}

			Console.WriteLine($"{i.FirstName}     {i.LastName}");
			Console.ReadLine();
		}

	}

	[DataContract(Name = "Customer", Namespace = "http://www.contoso.com")]
	public class PersonXml

	{
		[DataMember()] public string FirstName;

		[DataMember] public string LastName;

	}

}