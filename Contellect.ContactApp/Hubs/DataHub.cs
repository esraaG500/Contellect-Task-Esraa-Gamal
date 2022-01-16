using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Hubs
{
    public class DataHub : Hub
    {
        public async Task SendMessage(string user, string rowID , int pendingUpdateStatus)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, rowID, pendingUpdateStatus);
        }
    }
}
