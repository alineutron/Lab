

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;

namespace TryFormaters
{
	class Program
	{
		static void Main(string[] args)
		{
			var example = new FormaterExample();
			example.LetsDoIt();
		}
		
	}
	public class FormaterExample
	{
		public void LetsDoIt()
		{
			var p = new Person() { LastName = "Mirza", FirstName = "asad" };
			XmlSerializer serializer = new XmlSerializer(typeof(Person));
			Stream writer = new FileStream("xmlserializerExample.xml", FileMode.Create);
			// Serialize the object, and close the TextWriter 
			serializer.Serialize(writer, p);
			writer.Close();

			Console.WriteLine("Serialization done press enter to deserialize");
			Console.ReadLine();

			Person i;
			using (Stream reader = new FileStream("xmlserializerExample.xml", FileMode.Open))
			{
				i = (Person)serializer.Deserialize(reader);
			}

			Console.WriteLine($"{i.FirstName}     {i.LastName}");
			Console.ReadLine();
		}
	}

	[DataContract(Name = "Customer", Namespace = "http://www.google.com")]
	public class Person
	{
		[DataMember()]
		public string FirstName { get; set; }
		[DataMember]
		public string LastName { get; set; }
	}

}
