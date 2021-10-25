using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dating_APP.Helpers
{
	public static class ClaimsUsername
	{
		public static string GetUsername(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Name)?.Value;
		}
		public static int GetUserId(this ClaimsPrincipal user)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			var claim = user.FindFirst(ClaimTypes.NameIdentifier);
			return int.Parse(claim != null ? claim.Value : null);
		}

	}
}
