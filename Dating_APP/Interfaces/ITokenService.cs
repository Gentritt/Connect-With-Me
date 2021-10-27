using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Interfaces
{
	public interface ITokenService
	{
		Task<string> CreateToken(AppUser user);

	}
}
