 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Models
{
	public class UserLike
	{
		public AppUser SourceUser { get; set; }
		public int SourceUserId { get; set; }

		public AppUser LikedUser { get; set; }
		public int LikeUserId { get; set; }

	}
}
