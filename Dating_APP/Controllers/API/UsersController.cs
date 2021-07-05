using Dating_APP.Data;
using Dating_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dating_APP.Controllers.API
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly DataContext _context;

		public UsersController(DataContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			var users = await _context.Users.ToListAsync();

			return users;
		}

		//api/users/3
		[HttpGet("{id}")]
		public async Task<ActionResult<AppUser>> GetUsers(int id)
		{
			return await _context.Users.FindAsync(id);
		}
	}
}
