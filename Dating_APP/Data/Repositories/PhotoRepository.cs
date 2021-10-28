using Dating_APP.Dtos;
using Dating_APP.Interfaces;
using Dating_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Data.Repositories
{
	public class PhotoRepository : IPhotoRepository
	{
		private readonly DataContext context;

		public PhotoRepository(DataContext context)
		{
			this.context = context;
		}
		public async Task<Photo> GetPhotoById(int id)
		{
			return await context.Photos
				.IgnoreQueryFilters()
				.SingleOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhoto()
		{
			return await context.Photos
				.IgnoreQueryFilters()
				.Where(p => p.IsApproved == false)
				.Select(u => new PhotoForApprovalDto
				{
					Id = u.Id,
					Username = u.AppUser.UserName,
					Url = u.Url,
					isApproved = u.IsApproved


				}).ToListAsync();
		}

		public void RemovePhoto(Photo photo)
		{
			context.Photos.Remove(photo);
		}
	}
}
