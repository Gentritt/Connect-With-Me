using AutoMapper;
using Dating_APP.Data;
using Dating_APP.Dtos;
using Dating_APP.Extensions;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Dating_APP.Controllers.API
{
	[Authorize]
	public class UsersController : BaseApiController
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly IPhotoService _photoService;
		public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_photoService = photoService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
		{
			var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
			userParams.CurrentUsername = user.UserName;
			if (string.IsNullOrEmpty(userParams.Gender))
				userParams.Gender = user.Gender == "male" ? "female" : "male";

			var users = await _userRepository.GetMembersAsync(userParams);

			Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
			return Ok (users);

		}

		//api/users/3

		[HttpGet("{username}", Name = "GetUser")]
		public async Task<ActionResult<MemberDto>> GetUser(string username)
		{
			return await _userRepository.GetMemberAsync(username);
			
		}

		[HttpPut]

		public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
		{
			var username = User.GetUsername();
			var user = await _userRepository.GetUserByUsernameAsync(username);

			_mapper.Map(memberUpdateDto, user);

			_userRepository.Update(user);

			if (await _userRepository.SaveAllAsync()) return NoContent();
			return BadRequest("Failed to update User");
		}

		[HttpPost("add-photo")]
		
		public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
		{
			var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername()); // Getting the user

			var result = await _photoService.AddPhotoAsync(file);  // Getting the result back from photoService

			if (result.Error != null) return BadRequest(result.Error.Message);

			var photo = new Photo
			{
				Url = result.SecureUrl.AbsoluteUri,
				PublicId = result.PublicId
			}; // Create a new Photo

			if(user.Photos.Count  == 0) //Checking if user has any photos if the photo is set to isMain
			{
				photo.IsMain = true;

			}

			user.Photos.Add(photo);

			if (await _userRepository.SaveAllAsync())
			{
				//return _mapper.Map<Photo, PhotoDto>(photo);
				return CreatedAtRoute("GetUser", new {username = user.UserName } ,_mapper.Map<Photo, PhotoDto>(photo));
			}
				

			return BadRequest("Problem adding a photo");
		}

		[HttpDelete("delete-photo/{photoId}")]
		public async Task<ActionResult> DeletePhoto(int photoId)
		{
			var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

			var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

			if (photo == null) return NotFound();

			if (photo.IsMain) return BadRequest("You cannot delete your main photo");

			if (photo.PublicId != null)
			{
				var result = await _photoService.DeletePhotoAsync(photo.PublicId);
				if (result.Error != null) return BadRequest(result.Error.Message);
			}

		      user.Photos.Remove(photo);

			return Ok();


		}
	}
}
