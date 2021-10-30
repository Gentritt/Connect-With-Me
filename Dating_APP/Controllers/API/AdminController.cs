using Dating_APP.Data.Repositories;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Dating_APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Controllers.API
{
	public class AdminController : BaseApiController
	{
		private readonly UserManager<AppUser> userManager;
		private readonly IUnitOfWork unitOfWork;
		private readonly IPhotoService photoService;

		public AdminController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IPhotoService photoService)
		{
			this.userManager = userManager;
			this.unitOfWork = unitOfWork;
			this.photoService = photoService;
		}

		[Authorize(Policy ="RequireAdminRole")]
		[HttpGet("users-with-roles")]

		public async Task<ActionResult> GetUserWithRoles()
		{
			var users = await userManager.Users.Include(r => r.userRoles) //get users with their roles
				.ThenInclude(r=> r.Role)
				.OrderBy(u=> u.UserName)
				.Select(u=> new
				{
					u.Id,
					Username = u.UserName,
					KnownAs = u.KnownAs,
					Roles = u.userRoles.Select(r=> r.Role.Name).ToList() 
				})
				.ToListAsync();

			return Ok(users);
		}

		[HttpPost("edit-roles/{username}")]

		public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
		{
			var selectedRoles = roles.Split(",").ToArray();
			var user = await userManager.FindByNameAsync(username);

			if (user == null) return NotFound("Could not find user");
			var userRoles = await userManager.GetRolesAsync(user);

			var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

			if (!result.Succeeded) return BadRequest("failed to add to roles");

			result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
			if (!result.Succeeded) return BadRequest("failed to remove from roles");

			return Ok(await userManager.GetRolesAsync(user));

		}

		[Authorize(Policy = "RequireAdminRole")]
		[HttpGet("photos-to-moderate")]

		public async Task<ActionResult> GetPhotosForModeration()
		{
			var photos = await unitOfWork.photoRepository.GetUnapprovedPhoto();
			return Ok(photos);
		}

		[Authorize(Policy ="ModeratePhotoRole")]
		[HttpPost("approve-photo/{photoId}")]

		public async Task<ActionResult> ApprovePhoto(int PhotoId)
		{
			var photo = await unitOfWork.photoRepository.GetPhotoById(PhotoId);

			if (photo == null) return NotFound("Could not find photo");

			photo.IsApproved = true;

			var user = await unitOfWork.userRepository.GetUserByPhotoId(PhotoId);
			if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

			return Ok();
		}

		[Authorize(Policy ="ModeratePhotoRole")]
		[HttpPost("reject-photo/{photoId}")]
		public async Task<ActionResult> RejectPhoto(int photoId)
		{
			var photo = await unitOfWork.photoRepository.GetPhotoById(photoId);
			if (photo == null) return NotFound("Could not find the photo");

			if(photo.PublicId != null)
			{
				var result = await photoService.DeletePhotoAsync(photo.PublicId);

				if(result.Result == "ok")
				{
					unitOfWork.photoRepository.RemovePhoto(photo);
				}
			}
			else
			{
				unitOfWork.photoRepository.RemovePhoto(photo);

			}
			return Ok();
		}
	}
}
