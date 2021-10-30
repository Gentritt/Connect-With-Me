using AutoMapper;
using Dating_APP.Data.Repositories;
using Dating_APP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext context;
		private readonly IMapper mapper;

		public UnitOfWork(DataContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}
		public IUserRepository userRepository => new UserRepository(context,mapper);

		public IMessageRepository messageRepository => new MessagesRepository(context, mapper);

		public ILikesRepository likesRepository => new LikesRepository(context);

		public IPhotoRepository photoRepository => new PhotoRepository(context);

		public async Task<bool> Complete()
		{
			return await context.SaveChangesAsync() > 0;
		}

		public bool HasChanges()
		{
			return context.ChangeTracker.HasChanges();
		}
	}
}
