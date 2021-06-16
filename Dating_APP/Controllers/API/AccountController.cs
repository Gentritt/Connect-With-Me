using Dating_APP.Data;
using Dating_APP.Dtos;
using Dating_APP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dating_APP.Controllers.API
{
	public class AccountController : BaseApiController
	{
		private readonly DataContext _context;
		public AccountController(DataContext context)
		{
			_context = context;
		}

		[HttpPost("register")]
		public async Task<ActionResult<AppUser>> Register(RegisterDto register)
		{
			if (await UserExits(register.Username)) return BadRequest("Username is taken");

			using var hmac = new HMACSHA512();

			var user = new AppUser
			{
				UserName = register.Username.ToLower(),
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
				PasswordSalt = hmac.Key
			};
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return user;
		}

		[HttpPost("login")]

		public async Task<ActionResult<AppUser>> Login(LoginDto login)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login.Username);
			if (user == null) return Unauthorized("Invalid Username");

			using var hmac = new HMACSHA512(user.PasswordSalt);

			var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

			for (int i = 0; i < computedhash.Length; i++)
			{
				if (computedhash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Passowrd"); // If Password hash doesn't match
			}
			return user;
		}

		private async Task<bool> UserExits(string username)
		{
			return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
		}
	}
}
