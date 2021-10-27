using Dating_APP.Models;
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
		public static async Task SeedUsers(DataContext context)
		{
			if (await context.Users.AnyAsync()) return;

			var userData = await System.IO.File.ReadAllTextAsync("Data/userSeedData.json");
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
			foreach (var item in users)
			{
				item.UserName = item.UserName.ToLower();

				context.Users.Add(item);

			}
			await context.SaveChangesAsync();
		}
	}
}
