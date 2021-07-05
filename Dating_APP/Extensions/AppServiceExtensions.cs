using Dating_APP.Data;
using Dating_APP.Data.Repositories;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Extensions
{
	public static class AppServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddAutoMapper(typeof (AutoMapperProfiles).Assembly);
			services.AddDbContext<DataContext>(options => {
				options.UseSqlite(config.GetConnectionString("DefaultConnection"));
			});
			return services;
		}
	}
}
