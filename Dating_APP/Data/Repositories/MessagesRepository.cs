using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dating_APP.Dtos;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Data.Repositories
{
	public class MessagesRepository : IMessageRepository
	{
		private readonly DataContext _context;
		private readonly IMapper mapper;

		public MessagesRepository(DataContext dataContext, IMapper mapper)
		{
			_context = dataContext;
			this.mapper = mapper;
		}
		public void AddMessage(Message message)
		{
			_context.Messages.Add(message);
		}

		public void DeleteMessage(Message message)
		{
			_context.Messages.Remove(message);
		}

		public async Task<Message> GetMessage(int id)
		{
			return await _context.Messages
				.Include(u=> u.Sender)
				.Include(u=> u.Recipient)
				.SingleOrDefaultAsync(x=> x.Id == id); //get message for a user based on {id}
		}

		public async Task<IEnumerable<MessageDto>> GetMessagesForUser(MessageParams messageParams)
		{
			var query = _context.Messages.OrderByDescending(m => m.MessageSent)
				.AsQueryable(); //returns the messages sent

			query = messageParams.Container switch
			{
				"Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username),
				"Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username),
				_ => query.Where(u=> u.Recipient.UserName == messageParams.Username && u.DateRead == null)
			};

			var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);
			return await messages.ToListAsync();

		}

		public async Task<IEnumerable<MessageDto>> GetMessagesForUserTest(string container="Unread", string Username = null)
		{
			var query = _context.Messages.OrderByDescending(m => m.MessageSent)
					.AsQueryable();

			query = container switch
			{
				"Inbox" => query.Where(u => u.Recipient.UserName == Username && u.RecipientDeleted == false),
				"Outbox" => query.Where(u => u.Sender.UserName ==Username && u.SenderDeleted == false),
				_ => query.Where(u => u.Recipient.UserName == Username &&u.RecipientDeleted==false && u.DateRead == null )
			};

			var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);
			return await messages.ToListAsync();
		}

		public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
		{
			var messages = await _context.Messages
				.Include(u=> u.Sender).ThenInclude(p=> p.Photos)
				.Include(u => u.Recipient).ThenInclude(p => p.Photos)
				.Where(m => m.Recipient.UserName == currentUserName && m.RecipientDeleted==false
				&& m.Sender.UserName == recipientUserName
				|| m.Recipient.UserName == recipientUserName
				&& m.Sender.UserName == currentUserName && m.SenderDeleted == false

				).OrderBy(m=> m.MessageSent)
				.ToListAsync();
			var unReadMessages = messages.Where(m => m.DateRead == null && m.Recipient.UserName == currentUserName)
				.ToList();

			if (unReadMessages.Any())
			{
				foreach (var message in unReadMessages)
				{
					message.DateRead = DateTime.Now;
				}

				await _context.SaveChangesAsync();
			}

			return mapper.Map<IEnumerable<MessageDto>>(messages);
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
