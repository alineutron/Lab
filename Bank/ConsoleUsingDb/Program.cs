using System;
using System.Collections.Generic;
using System.IO;
using AT;
using Autofac;
using Domain;
using Newtonsoft.Json;

namespace ConsoleUsingDb
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			
			Console.WriteLine("Do you want to use file storage y|Y");
			var input = Console.ReadLine();
			if (input.Equals("y") || input.Equals("Y"))
			{
				builder.RegisterType<FileLoanRepository>().As<ILoanRepository>().SingleInstance();
			}
				
			else
				builder.RegisterType<MemoryLoanRepository>().As<ILoanRepository>().SingleInstance();
			builder.RegisterType<LoanManager>();
			builder.RegisterType<MyBank>();
			builder.RegisterType<UserInput>();
			builder.RegisterType<Now>().As<INow>();
			var container = builder.Build();
		
			using (var scope = container.BeginLifetimeScope())
			{
				var writer = scope.Resolve<MyBank>();
				writer.Start(); 
			}
		}
	}

	public class MyBank 
	{
		private readonly UserInput _userInput;

		public MyBank( UserInput userInput)
		{
			_userInput = userInput;
		}
		public void Start()
		{
			_userInput.Get();

		}

		
	}
}
