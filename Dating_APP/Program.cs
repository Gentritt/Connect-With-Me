using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dating_APP.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dating_APP
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build(); 
			using var scope = host.Services.CreateScope(); // Seeding the data from Json File when application Starts
			var services = scope.ServiceProvider;
			try
			{
				var context = services.GetRequiredService<DataContext>();
				await context.Database.MigrateAsync();
				await Seed.SeedUsers(context);
			}
			catch (Exception ex)
			{

				var logger = services.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "An Error Occurred during migration");
			}
			await host.RunAsync();

		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
