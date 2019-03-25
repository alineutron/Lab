using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TryFormaters
{
	class Program
	{
		static void Main(string[] args)
		{
			var exmaple = new BinaryFormatersExmaple();
			exmaple.LetsDoIt();
		}
	}

	public class BinaryFormatersExmaple
	{
		public void LetsDoIt()
		{
			var listOfName = new List<string>();
			listOfName.Add("object1");
			listOfName.Add("object2");
			listOfName.Add("object3");

			var fs = new FileStream("datafile.dat", FileMode.Create);
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				formatter.Serialize(fs, listOfName);
			}
			catch (Exception)
			{
				Console.WriteLine("some error");

			}
			finally
			{
				fs.Close();
			}

			Console.WriteLine("Serialization done. Press enter to continue deserializing it");
			Console.ReadLine();

			fs = new FileStream("datafile.dat",FileMode.Open);
			try
			{
				var list = (List<string>) formatter.Deserialize(fs);
				list.ForEach(x => { Console.WriteLine(x); });

			}
			catch (Exception)
			{
				Console.WriteLine("Something happend");

			}
			finally
			{
				fs.Close();
			}
			Console.ReadLine();
		}
	}
}
