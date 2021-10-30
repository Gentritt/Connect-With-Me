using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Interfaces
{
	public interface IUnitOfWork
	{
		IUserRepository userRepository { get; }
		IMessageRepository messageRepository { get; }
		ILikesRepository likesRepository { get; }
		IPhotoRepository photoRepository { get; }

		Task<bool> Complete();
		bool HasChanges();
	}
}
