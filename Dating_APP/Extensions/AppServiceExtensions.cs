using Dating_APP.Data;
using Dating_APP.Data.Repositories;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Services;
using Dating_APP.SignalR;
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
			services.AddSingleton<PresenceTracker>();
			services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
			services.AddScoped<IPhotoService, PhotoService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<LogUserActivity>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IPhotoRepository, PhotoRepository>();
			services.AddAutoMapper(typeof (AutoMapperProfiles).Assembly);
			services.AddDbContext<DataContext>(options => {
				options.UseSqlite(config.GetConnectionString("DefaultConnection"));
			});
			return services;
		}
	}
}
