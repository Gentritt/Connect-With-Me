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
		void AddGroup(Group group);
		void RemoveConnection(Connection connection);
		Task<Connection> GetConnection(string connectionID);
		Task<Group> GetMessageGroup(string groupName);
		Task<Group> GetGroupForConnection(string connectionId);
		void AddMessage(Message message);
		void DeleteMessage(Message message);
		Task<Message> GetMessage(int id);
		Task<IEnumerable<MessageDto>> GetMessagesForUser(MessageParams messageParams);
		Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName);
		Task<bool> SaveAllAsync();
		Task<IEnumerable<MessageDto>> GetMessagesForUserTest(string container,string username);


	}
}
