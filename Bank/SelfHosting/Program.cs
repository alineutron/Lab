using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using AT;
using Domain;
using Microsoft.Owin.Hosting;
using Owin;

namespace SelfHosting
{
	class Program
	{
		static void Main(string[] args)
		{
			string baseAddress = "http://localhost:9120/";

			using (WebApp.Start<Startup>(url: baseAddress))
			{


				ILoanRepository loanrepo = new MemoryLoanRepository();
				var now = new Now();
				var lm = new LoanManager(loanrepo, now);

				var userInput = new UserInput(lm, loanrepo); //a bad practice here where i am passing the repo which is also in lm
				userInput.Get();


				HttpClient client = new HttpClient();

				var response = client.GetAsync(baseAddress + "api/bank").Result;

				Console.WriteLine(response);
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);
				Console.ReadLine();


			}
		}
	}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			HttpConfiguration config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			app.UseWebApi(config);
		}
	}

	public class BankController : ApiController
	{
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}
