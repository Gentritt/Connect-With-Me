﻿ using AutoMapper;
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
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;

		public MessagesController(IUnitOfWork unitOfWork , IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		[HttpPost]

		public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
		{
			var username = User.GetUsername(); //gets username

			if (username == createMessageDto.RecipientUsername.ToLower()) 
				return BadRequest("Cant Sent messages to urself"); 

			var sender = await unitOfWork.userRepository.GetUserByUsernameAsync(username); //gets the username of sender

			var recipient = await unitOfWork.userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername); //the recipient
			if (recipient == null) return NotFound();

			var message = new Message
			{
				Sender = sender,
				Recipient = recipient,
				SenderUsername = sender.UserName,
				RecipientUsername = recipient.UserName,
				Content = createMessageDto.Content
			};
			unitOfWork.messageRepository.AddMessage(message);

			if (await unitOfWork.Complete()) return Ok(mapper.Map<MessageDto>(message));

			return BadRequest("Failed to send message");




		}
		//[HttpGet]

		//public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
		//{
		//	messageParams.Username = User.GetUsername();
		//	var messages = await unitOfWork.messageRepository.GetMessagesForUser(messageParams);

		//	/*Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages)*/;

		//	return Ok(messages);

		//}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUserTest(string container = "Unread", string Username = null)
		{
			Username = User.GetUsername();
			var messages = await unitOfWork.messageRepository.GetMessagesForUserTest(container,Username);


			return Ok(messages);

		}



		[HttpGet("thread/{username}")]

		public async Task <ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
		{
			var currentUsername = User.GetUsername();

			return Ok(await unitOfWork.messageRepository.GetMessageThread(currentUsername, username));
		}

		[HttpDelete("{id}")]

		public async Task<ActionResult> DeleteMessage(int id)
		{
			var username = User.GetUsername();

			var message = await unitOfWork.messageRepository.GetMessage(id);
			if (message.Sender.UserName != username && message.Recipient.UserName != username) return Unauthorized(); 

			if(message.Sender.UserName == username)
			{
				message.SenderDeleted = true;
			}
			if(message.Recipient.UserName == username)
			{
				message.RecipientDeleted = true;
			}

			if(message.SenderDeleted && message.RecipientDeleted)
			{
				unitOfWork.messageRepository.DeleteMessage(message);
			}

			if (await unitOfWork.Complete()) return Ok();

			return BadRequest("Problem deleting the message");
		}

	}
}
