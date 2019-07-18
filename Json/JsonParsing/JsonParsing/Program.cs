using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace JsonParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            var process = new ParseJson();
            process.Parse();
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }

    public class ParseJson
    {
        public void Parse()
        {
            var json = File.ReadAllText("sampleJson.txt");
            var jObject = JObject.Parse(json);
            Console.WriteLine(jObject["glossary"]["title"]);
            if(jObject["glossary"]["GlossDiv"]["GlossList"]["GlossEntry"].Contains("Abbrev"))
                Console.WriteLine(jObject["glossary"]["GlossDiv"]["GlossList"]["GlossEntry"]["Abbrev"]);
        }
    }

}
