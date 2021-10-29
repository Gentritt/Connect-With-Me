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
		private readonly PresenceTracker tracker;

		public PresenceHub(PresenceTracker tracker)
		{
			this.tracker = tracker;
		}
		public override async Task OnConnectedAsync()
		{
			 await tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId); // getting a user when connects

			await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());

			var currentUsers = await tracker.GetOnlineUsers();
			await Clients.All.SendAsync("GetOnlineUsers", currentUsers); // sent to everyne when someone connects
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{

			await tracker.UserDisconnected(Context.User.GetUsername(), Context.ConnectionId);
			await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());
			var currentUsers = await tracker.GetOnlineUsers();

			await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
			await base.OnDisconnectedAsync(exception);
		}

	}
}
