using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Helpers
{
	public class UserParams: PaginationParams
	{


		public string CurrentUsername { get; set; }
		public string Gender { get; set; }

		public string OrderBy { get; set; } = "lastActive";
	}
}

