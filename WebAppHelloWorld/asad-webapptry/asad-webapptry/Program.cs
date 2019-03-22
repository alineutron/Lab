using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using asad_webapptry.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace asad_webapptry
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = BuildWebHost(args);
			MigrateDataBase(host);
			host.Run();
		}

		private static void MigrateDataBase(IWebHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				context.Database.Migrate();
			}
		}

		private static IWebHost BuildWebHost(string[] args)
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
