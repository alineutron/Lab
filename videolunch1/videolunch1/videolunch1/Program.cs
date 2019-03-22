using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using videolunch1.Data;

namespace videolunch1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost(args);
			MigrateDatabase(host);
			host.Run();

			CreateWebHostBuilder(args).Build().Run();
		}

		public static void MigrateDatabase(IWebHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				context.Database.Migrate();
			}
		}

		public static IWebHost BuildWebHost(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
