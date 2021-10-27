using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Models
{
	public class AppRole:IdentityRole<int>
	{
		public ICollection<AppUserRole> userRoles { get; set; }
	}
}
