using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Formaters
{
	public class DataContractJsonSerializerExample
	{
		public void DoIt()
		{
			var p = new PersonDCJS() {FirstName = "Asad", LastName = "Mirza"};
			FileStream writer = new FileStream("dcjs.txt", FileMode.Create);
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PersonDCJS));
			ser.WriteObject(writer, p);
			writer.Close();

			Console.WriteLine("Serialization done press enter to deserialize");
			Console.ReadLine();

			FileStream reader = new FileStream("dcjs.txt", FileMode.Open);
			PersonDCJS personDCJS = (PersonDCJS) ser.ReadObject(reader);
			reader.Close();
			Console.WriteLine(personDCJS.FirstName + "   " + personDCJS.LastName);
			Console.ReadLine();

		}
	}

	public class PersonDCJS
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}