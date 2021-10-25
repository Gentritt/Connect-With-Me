using Dating_APP.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Models
{
	public class AppUser
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
		public DateTime LastActive { get; set; } = DateTime.Now; 
		public string Gender { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public ICollection<Photo> Photos { get; set; }

		public ICollection<UserLike> LikedByUsers { get; set; } //likeby
		public ICollection<UserLike> LikedUsers { get; set; } //users that currently user has liked

		public ICollection<Message> MessagesSent { get; set; }
		public ICollection<Message> MessagesRecieved { get; set; }



	}
}
