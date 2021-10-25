using Dating_APP.Dtos;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dating_APP.Controllers.API
{
	//*/[Authorize]*/
	public class LikesController : BaseApiController
	{
		private readonly IUserRepository _userRepository;
		private readonly ILikesRepository _likesRepository;
		public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
		{
			_userRepository = userRepository;
			_likesRepository = likesRepository;
		}

		[HttpPost("{username}")]
		
		public async Task<ActionResult> AddLike(string username)
		{
			var userId = int.Parse( HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var sourceUserId = userId;

			var likedUser = await _userRepository.GetUserByUsernameAsync(username);

			var sourceUser = await _likesRepository.GetUsersWithLikes(sourceUserId);

			if (likedUser == null) return NotFound();

			if (sourceUser.UserName == username) return BadRequest("You cant like yourself! :P ");

			var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

			if (userLike != null) return BadRequest("You already liked this user");

			userLike = new UserLike
			{
				SourceUserId = sourceUserId,
				LikeUserId = likedUser.Id
			};

			sourceUser.LikedUsers.Add(userLike);
			if (await _userRepository.SaveAllAsync()) return Ok();

			return BadRequest("Failed to like user");


		}

		[HttpGet]

		public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
		{
			var users =  await _likesRepository.GetUserLikes(predicate, User.GetUserId());
			return Ok(users);
		} 





	}
}
