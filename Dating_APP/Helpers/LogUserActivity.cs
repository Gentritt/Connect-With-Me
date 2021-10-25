using Dating_APP.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Dating_APP.Helpers
{
	public class LogUserActivity : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var resultContext = await next();

			if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

			var userId = resultContext.HttpContext.User.GetUserId();
			//var username = resultContext.HttpContext.User.GetUsername();
			var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

			var user = await repo.GetUserByIdAsync(userId);
			user.LastActive = DateTime.Now;
			await repo.SaveAllAsync();
		}
	}
}
