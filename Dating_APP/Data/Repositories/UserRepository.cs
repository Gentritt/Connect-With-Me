using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _context;
		public UserRepository(DataContext context)
		{
			_context = context;
		}
		public async Task<AppUser> GetUserByIdAsync(int id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<AppUser> GetUserByUsernameAsync(string username)
		{
			return await _context.Users.
				Include(p=> p.Photos)
				.SingleOrDefaultAsync(u => u.UserName == username);
		}

		public async Task<IEnumerable<AppUser>> GetUsersAsync()
		{
			return await _context.Users
				.Include(p=> p.Photos)
				.ToListAsync();
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public void Update(AppUser user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}
	}
}
