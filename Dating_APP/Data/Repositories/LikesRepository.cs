using Dating_APP.Dtos;
using Dating_APP.Extensions;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Data.Repositories
{
	public class LikesRepository : ILikesRepository
	{
		private readonly DataContext _context;
		public LikesRepository(DataContext context)
		{
			_context = context;
		}
		public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
		{
			return await _context.Likes.FindAsync(sourceUserId, likedUserId);
		}

		public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int UserId)
		{
			var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
			var likes = _context.Likes.AsQueryable();

			if (predicate == "liked")
			{
				likes = likes.Where(like => like.SourceUserId == UserId);
				users = likes.Select(like => like.LikedUser); //users from our likes table
			}
			if (predicate == "likedBy")
			{
				likes = likes.Where(like => like.LikeUserId == UserId);
				users = likes.Select(like => like.SourceUser); //gives the lists of users that liked the current login user
			}

			return await users.Select(user => new LikeDto
			{
				Username = user.UserName,
				KnownAs = user.KnownAs,
				Age = user.DateOfBirth.CalculateAge(),
				photoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
				City = user.City,
				Id = user.Id
			}).ToListAsync();
		}

		public async Task<AppUser> GetUsersWithLikes(int userId)
		{
			return await _context.Users.Include(x => x.LikedUsers).FirstOrDefaultAsync(x => x.Id == userId);
		}
	}
}
