using AutoMapper;
using Dating_APP.Dtos;
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
			var username = User.GetUsername();

			if (username == createMessageDto.RecipientUsername.ToLower()) 
				return BadRequest("Cant Sent messages to urself");

			var sender = await userRepository.GetUserByUsernameAsync(username);

			var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
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
	}
}
