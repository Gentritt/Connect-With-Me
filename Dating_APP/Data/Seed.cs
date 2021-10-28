using Dating_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dating_APP.Data
{
	public class Seed
	{
		public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			if (await userManager.Users.AnyAsync()) return;

			var userData = await System.IO.File.ReadAllTextAsync("Data/userSeedData.json");
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

			if (users == null) return;
			var roles = new List<AppRole>
			{
				new AppRole{Name="Member"},
				new AppRole{Name="Admin"},
				new AppRole{Name="Moderator"}
			};

			foreach (var role in roles)
			{
				await roleManager.CreateAsync(role);
			}
			foreach (var item in users)
			{
				item.Photos.First().IsApproved = true;
				item.UserName = item.UserName.ToLower();

				await userManager.CreateAsync(item, "Password");
				await userManager.AddToRoleAsync(item, "Member");

			}

			var admin = new AppUser
			{
				UserName = "admin"
			};
			await userManager.CreateAsync(admin, "Password");
			await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
		}
	}
}
