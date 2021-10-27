using AutoMapper;
using Dating_APP.Data;
using Dating_APP.Dtos;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly ITokenService _token;
		private readonly IMapper _mapper;
		public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService token, IMapper mapper)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			_token = token;
			_mapper = mapper;


		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if (await UserExits(registerDto.Username)) return BadRequest("Username is taken");
			var user = _mapper.Map<AppUser>(registerDto);
			user.UserName = registerDto.Username.ToLower();

			var result = await userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded) return BadRequest(result.Errors);

			var roleResult = await userManager.AddToRoleAsync(user, "Member");
			if (!roleResult.Succeeded) return BadRequest(result.Errors);

			return new UserDto {
				Username = user.UserName,
				Token = await _token.CreateToken(user),
				KnownAs = user.KnownAs,
				Gender = user.Gender

			};
		}
		[HttpPost("login")] //test
		public async Task<ActionResult<UserDto>> Login(LoginDto login)
		{
			var user = await userManager.Users
				.Include(p=> p.Photos)
				.SingleOrDefaultAsync(x => x.UserName == login.Username.ToLower());

			if (user == null) return Unauthorized("Invalid Username");

			var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

			if (!result.Succeeded) return Unauthorized("Check your password");
			return new UserDto
			{
				Username = user.UserName,
				Token = await _token.CreateToken(user),
				KnownAs = user.KnownAs,
				PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
				Gender = user.Gender
			};
		}

		private async Task<bool> UserExits(string username)
		{
			return await userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
		}
	}
}
