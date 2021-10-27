using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dating_APP.Services
{
	public class TokenService : ITokenService
	{
		
		private readonly SymmetricSecurityKey key;
		private readonly UserManager<AppUser> userManager;

		public TokenService(IConfiguration config, UserManager<AppUser> userManager)
		{
			key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
			this.userManager = userManager;
		}
		public async Task<string> CreateToken(AppUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)//Adding or claims
			};

			var roles = await userManager.GetRolesAsync(user);

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //creating some creds
			var tokenDesc = new SecurityTokenDescriptor //describing how the token will look
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDesc); //create the token
			return tokenHandler.WriteToken(token); //return the written token
		}
	}
}
