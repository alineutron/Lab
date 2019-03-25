using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Formaters
{
	public class BinaryFormatterExample

	{
		public void DoIt()
		{
			var listOfName = new List<string>();
			listOfName.Add("Asad");
			listOfName.Add("Ali");
			listOfName.Add("Mirza");

			var fs = new FileStream("datafile.Dat", FileMode.Create);
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				formatter.Serialize(fs, listOfName);
			}
			catch (Exception e)
			{

			}
			finally
			{
				fs.Close();
			}

			Console.WriteLine("Serialization done press enter to deserialize");
			Console.ReadLine();
			fs = new FileStream("datafile.Dat", FileMode.Open);
			try
			{
				var list = ((List<string>) formatter.Deserialize(fs));
				list.ForEach(x => { Console.WriteLine(x); });
			}
			catch (Exception e)
			{
			}
			finally
			{
				fs.Close();
			}

		}

	}
}