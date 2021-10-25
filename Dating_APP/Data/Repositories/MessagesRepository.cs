using Dating_APP.Dtos;
using Dating_APP.Helpers;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Data.Repositories
{
	public class MessagesRepository : IMessageRepository
	{
		private readonly DataContext _context;
		public MessagesRepository(DataContext dataContext)
		{
			_context = dataContext;
		}
		public void AddMessage(Message message)
		{
			_context.Messages.Add(message);
		}

		public void DeletMessage(Message message)
		{
			_context.Messages.Remove(message);
		}

		public async Task<Message> GetMessage(int id)
		{
			return await _context.Messages.FindAsync(id);
		}

		public Task<PagedList<MessageDto>> GetMessagesForUser()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<MessageDto>> GetMessageThread(int currentUserId, int recipientId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> SaveAllAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
