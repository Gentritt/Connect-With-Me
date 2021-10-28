using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Dtos
{
	public class PhotoForApprovalDto
	{

		public int Id { get; set; }
		public string Username { get;  set; }
		public string Url { get; set; }
		public bool isApproved { get; set; }
	}
}
