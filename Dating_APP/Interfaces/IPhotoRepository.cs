using Dating_APP.Dtos;
using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Interfaces
{
	public interface IPhotoRepository
	{
		Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhoto();
		Task<Photo> GetPhotoById(int id);
		void RemovePhoto(Photo photo);
	}
}
