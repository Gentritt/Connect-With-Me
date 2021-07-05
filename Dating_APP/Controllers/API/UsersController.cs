using AutoMapper;
using Dating_APP.Data;
using Dating_APP.Dtos;
using Dating_APP.Interfaces;
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
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public UsersController(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
		{
			var users = await _userRepository.GetUsersAsync();
			var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
			return Ok (usersToReturn);

		}

		//api/users/3
		[HttpGet("{username}")]
		public async Task<ActionResult<MemberDto>> GetUser(string username)
		{
			var user = await _userRepository.GetUserByUsernameAsync(username);
			return _mapper.Map<MemberDto>(user);
		}
	}
}
