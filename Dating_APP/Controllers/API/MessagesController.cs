 using AutoMapper;
using Dating_APP.Dtos;
using Dating_APP.Extensions;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Controllers.API
{
	[Authorize]
	public class MessagesController : BaseApiController
	{
		private readonly IUserRepository userRepository;
		private readonly IMessageRepository messageRepository;
		private readonly IMapper mapper;

		public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
		{
			this.userRepository = userRepository;
			this.messageRepository = messageRepository;
			this.mapper = mapper;
		}

		[HttpPost]

		public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
		{
			var username = User.GetUsername(); //gets username

			if (username == createMessageDto.RecipientUsername.ToLower()) 
				return BadRequest("Cant Sent messages to urself"); 

			var sender = await userRepository.GetUserByUsernameAsync(username); //gets the username of sender

			var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername); //the recipient
			if (recipient == null) return NotFound();

			var message = new Message
			{
				Sender = sender,
				Recipient = recipient,
				SenderUsername = sender.UserName,
				RecipientUsername = recipient.UserName,
				Content = createMessageDto.Content
			};
			messageRepository.AddMessage(message);

			if (await messageRepository.SaveAllAsync()) return Ok(mapper.Map<MessageDto>(message));

			return BadRequest("Failed to send message");




		}
		//[HttpGet]

		//public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
		//{
		//	messageParams.Username = User.GetUsername();
		//	var messages = await messageRepository.GetMessagesForUser(messageParams);

		//	/*Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages)*/;

		//	return Ok(messages);

		//}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUserTest(string container = "Unread", string Username = null)
		{
			Username = User.GetUsername();
			var messages = await messageRepository.GetMessagesForUserTest(container,Username);


			return Ok(messages);

		}



		[HttpGet("thread/{username}")]

		public async Task <ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
		{
			var currentUsername = User.GetUsername();

			return Ok(await messageRepository.GetMessageThread(currentUsername, username));
		}

	}
}
