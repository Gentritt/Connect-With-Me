using AutoMapper;
using Dating_APP.Dtos;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.SignalR
{
	public class MessageHub : Hub
	{
		private readonly IMessageRepository messageRepository;
		private readonly IMapper mapper;
		private readonly IUserRepository userRepository;

		public MessageHub(IMessageRepository messageRepository, IMapper mapper,  IUserRepository userRepository)
		{
			this.messageRepository = messageRepository;
			this.mapper = mapper;
			this.userRepository = userRepository;
		}

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();

			var otherUser = httpContext.Request.Query["user"].ToString();
			var groupName = GetGroupName(Context.User.GetUsername(),otherUser);

			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			await AddToGroup(Context, groupName);

			var messages = await messageRepository.GetMessageThread(Context.User.GetUsername(), otherUser);
			await Clients.Group(groupName).SendAsync("ReceiveMessageThread", messages);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await RemoveFromMessageGroup(Context.ConnectionId);
			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessage(CreateMessageDto createMessageDto)
		{
			var username = Context.User.GetUsername(); //gets username

			if (username == createMessageDto.RecipientUsername.ToLower())
				throw new HubException("Cant Sent messages to urself");

			var sender = await userRepository.GetUserByUsernameAsync(username); //gets the username of sender

			var recipient = await userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername); //the recipient
			if (recipient == null) throw new HubException("Not Found user");

			var message = new Message
			{
				Sender = sender,
				Recipient = recipient,
				SenderUsername = sender.UserName,
				RecipientUsername = recipient.UserName,
				Content = createMessageDto.Content
			};
			var groupName = GetGroupName(sender.UserName, recipient.UserName);

			var group = await messageRepository.GetMessageGroup(groupName);

			if(group.Connections.Any(x=> x.Username == recipient.UserName))
			{
				message.DateRead = DateTime.UtcNow;
			}
			messageRepository.AddMessage(message);

			if (await messageRepository.SaveAllAsync())
			{
				
				await Clients.Group(groupName).SendAsync("NewMessage", mapper.Map<MessageDto>(message));
			}

			
		}

		public async Task<bool> AddToGroup(HubCallerContext context, string groupName)
		{
			var group = await messageRepository.GetMessageGroup(groupName);

			var connction = new Connection(Context.ConnectionId, Context.User.GetUsername());
			if (group == null)
			{
				group = new Group(groupName);
				messageRepository.AddGroup(group);
			}
			group.Connections.Add(connction);
			return await messageRepository.SaveAllAsync();
		}

		private async Task RemoveFromMessageGroup(string connectionID)
		{
			var connection = await messageRepository.GetConnection(connectionID);
			messageRepository.RemoveConnection(connection);

			await messageRepository.SaveAllAsync();
		}
		private string GetGroupName(string caller, string other)
		{
			var stringCompare = string.CompareOrdinal(caller, other) < 0;
			return stringCompare ? $"{caller}{other}" : $"{other}-{caller}";
		}
	}
}
