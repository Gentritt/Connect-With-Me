using Dating_APP.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.SignalR
{
	[Authorize]
	public class PresenceHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());

			await base.OnDisconnectedAsync(exception);
		}

	}
}
