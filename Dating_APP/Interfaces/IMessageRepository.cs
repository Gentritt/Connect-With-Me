using Dating_APP.Dtos;
using Dating_APP.Helpers;
using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Interfaces
{
	public interface IMessageRepository
	{
		void AddMessage(Message message);
		void DeletMessage(Message message);
		Task<Message> GetMessage(int id);
		Task<PagedList<MessageDto>> GetMessagesForUser();
		Task<IEnumerable<MessageDto>> GetMessageThread(int currentUserId, int recipientId);
		Task<bool> SaveAllAsync();

	}
}
