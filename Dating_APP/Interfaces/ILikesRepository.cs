using Dating_APP.Dtos;
using Dating_APP.Helpers;
using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Interfaces
{
	public interface ILikesRepository
	{
		Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
		Task<AppUser> GetUsersWithLikes(int userId);
		Task<IEnumerable<LikeDto>> GetUserLikes(string predicate,int userId);

	}
}
