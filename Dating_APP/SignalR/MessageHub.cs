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
		private readonly IHubContext<PresenceHub> presenceHub;
		private readonly PresenceTracker presenceTracker;

		public MessageHub(IMessageRepository messageRepository, IMapper mapper,  IUserRepository userRepository,
			IHubContext<PresenceHub> presenceHub, PresenceTracker presenceTracker)
		{
			this.messageRepository = messageRepository;
			this.mapper = mapper;
			this.userRepository = userRepository;
			this.presenceHub = presenceHub;
			this.presenceTracker = presenceTracker;
		}

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();

			var otherUser = httpContext.Request.Query["user"].ToString();
			var groupName = GetGroupName(Context.User.GetUsername(),otherUser);
			await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
			var group = await AddToGroup(groupName);
			await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

			var messages = await messageRepository.GetMessageThread(Context.User.GetUsername(), otherUser);
			await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			var group = await RemoveFromMessageGroup();
			await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
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
			var groupName = GetGroupName(sender.UserName, recipient.UserName); // create a group for users in the same group

			var group = await messageRepository.GetMessageGroup(groupName);

			if(group.Connections.Any(x=> x.Username == recipient.UserName)) //check if the user is in the same group
			{
				message.DateRead = DateTime.UtcNow;
			}

			else
			{
				var connections = await presenceTracker.GetConnectionsForUser(recipient.UserName);
				if(connections != null)
				{
					await presenceHub.Clients.Clients(connections).SendAsync("NewMessageRecevied", 
						new {username = sender.UserName, knownAs = sender.KnownAs }); // if the user is onlin but not connected to the same group
				}
			}
			messageRepository.AddMessage(message);

			if (await messageRepository.SaveAllAsync())
			{
				
				await Clients.Group(groupName).SendAsync("NewMessage", mapper.Map<MessageDto>(message)); 
			}

			
		}

		public async Task<Group> AddToGroup(string groupName)
		{
			var group = await messageRepository.GetMessageGroup(groupName);

			var connction = new Connection(Context.ConnectionId, Context.User.GetUsername());
			if (group == null)
			{
				group = new Group(groupName);
				messageRepository.AddGroup(group);
			}
			group.Connections.Add(connction);

			if (await messageRepository.SaveAllAsync()) return group;
			throw new HubException("Failed to join group");
		}

		private async Task<Group> RemoveFromMessageGroup()
		{
			var group = await messageRepository.GetGroupForConnection(Context.ConnectionId);

			var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

			messageRepository.RemoveConnection(connection);

			if(await messageRepository.SaveAllAsync()) return group;

			throw new HubException("Failed to remove from group");
		}
		private string GetGroupName(string caller, string other)
		{
			var stringCompare = string.CompareOrdinal(caller, other) < 0;
			return stringCompare ? $"{caller}{other}" : $"{other}-{caller}";
		}
	}
}
