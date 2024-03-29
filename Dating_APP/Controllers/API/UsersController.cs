﻿using AutoMapper;
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
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPhotoService _photoService;

		public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
		{
			this.unitOfWork = unitOfWork;
			_mapper = mapper;
			_photoService = photoService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
		{
			var user = await unitOfWork.userRepository.GetUserByUsernameAsync(User.GetUsername());
			userParams.CurrentUsername = user.UserName;
			if (string.IsNullOrEmpty(userParams.Gender))
				userParams.Gender = user.Gender == "male" ? "female" : "male";

			var users = await unitOfWork.userRepository.GetMembersAsync(userParams);

			Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
			return Ok (users);

		}

		//api/users/3

		[HttpGet("{username}", Name = "GetUser")]
		public async Task<ActionResult<MemberDto>> GetUser(string username)
		{
			var currentUsername = User.GetUsername();
			return await unitOfWork.userRepository.GetMemberAsync(username, isCurrentUser: currentUsername == username);
			
		}

		[HttpPut]

		public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
		{
			var username = User.GetUsername();
			var user = await unitOfWork.userRepository.GetUserByUsernameAsync(username);

			_mapper.Map(memberUpdateDto, user);

			unitOfWork.userRepository.Update(user);

			if (await unitOfWork.userRepository.SaveAllAsync()) return NoContent();
			return BadRequest("Failed to update User");
		}

		[HttpPost("add-photo")]
		
		public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
		{
			var user = await unitOfWork.userRepository.GetUserByUsernameAsync(User.GetUsername()); // Getting the user

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

			if (await unitOfWork.userRepository.SaveAllAsync())
			{
				//return _mapper.Map<Photo, PhotoDto>(photo);
				return CreatedAtRoute("GetUser", new {username = user.UserName } ,_mapper.Map<Photo, PhotoDto>(photo));
			}
				

			return BadRequest("Problem adding a photo");
		}

		[HttpDelete("delete-photo/{photoId}")]
		public async Task<ActionResult> DeletePhoto(int photoId)
		{
			var user = await unitOfWork.userRepository.GetUserByUsernameAsync(User.GetUsername());

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
