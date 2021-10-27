using Dating_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Controllers.API
{
	public class AdminController : BaseApiController
	{
		private readonly UserManager<AppUser> userManager;

		public AdminController(UserManager<AppUser> userManager)
		{
			this.userManager = userManager;
		}

		[Authorize(Policy ="RequireAdminRole")]
		[HttpGet("users-with-roles")]

		public async Task<ActionResult> GetUserWithRoles()
		{
			var users = await userManager.Users.Include(r => r.userRoles) //get users with their roles
				.ThenInclude(r=> r.Role)
				.OrderBy(u=> u.UserName)
				.Select(u=> new
				{
					u.Id,
					Username = u.UserName,
					KnownAs = u.KnownAs,
					Roles = u.userRoles.Select(r=> r.Role.Name).ToList() 
				})
				.ToListAsync();

			return Ok(users);
		}

		[HttpPost("edit-roles/{username}")]

		public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
		{
			var selectedRoles = roles.Split(",").ToArray();
			var user = await userManager.FindByNameAsync(username);

			if (user == null) return NotFound("Could not find user");
			var userRoles = await userManager.GetRolesAsync(user);

			var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

			if (!result.Succeeded) return BadRequest("failed to add to roles");

			result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
			if (!result.Succeeded) return BadRequest("failed to remove from roles");

			return Ok(await userManager.GetRolesAsync(user));

		}

		[Authorize(Policy = "RequirePhotoRole")]
		[HttpGet("photos-to-moderate")]

		public ActionResult GetPhotosForModeration()
		{
			return Ok("Admins or moderators can see this");
		}
	}
}
