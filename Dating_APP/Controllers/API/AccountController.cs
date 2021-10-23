using AutoMapper;
using Dating_APP.Data;
using Dating_APP.Dtos;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Cors;
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
		private readonly ITokenService _token;
		private readonly IMapper _mapper;
		public AccountController(DataContext context, ITokenService token, IMapper mapper)
		{
			_context = context;
			_token = token;
			_mapper = mapper;


		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto register)
		{
			if (await UserExits(register.Username)) return BadRequest("Username is taken");
			var user = _mapper.Map<AppUser>(register);

			using var hmac = new HMACSHA512();

			user.UserName = register.Username.ToLower();
				user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
				user.PasswordSalt = hmac.Key;
			
			
			
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return new UserDto {
				Username = user.UserName,
				Token = _token.CreateToken(user),
				KnownAs = user.KnownAs,

			};
		}
		[HttpPost("login")] //test
		public async Task<ActionResult<UserDto>> Login(LoginDto login)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login.Username);
			if (user == null) return Unauthorized("Invalid Username");

			using var hmac = new HMACSHA512(user.PasswordSalt);

			var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

			for (int i = 0; i < computedhash.Length; i++)
			{
				if (computedhash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Passowrd"); // If Password hash doesn't match
			}
			return new UserDto
			{
				Username = user.UserName,
				Token = _token.CreateToken(user),
				KnownAs = user.KnownAs,
				//PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
			};
		}

		private async Task<bool> UserExits(string username)
		{
			return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
		}
	}
}
