using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aniverse.UI.Hubs
{
    public class ChatHub : Hub
    {
        private string _channelName;
        private readonly IDictionary<string, UserConnection> _connection;
        public ChatHub(IDictionary<string, UserConnection> connection)
        {
            _channelName = "Chat bot";
            _connection = connection; 
        }
        public async Task SendMessage(string message)
        {
            if(_connection.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room)
                    .SendAsync("RecevieMessage", userConnection.User, message);
            }
        }
        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
            _connection[Context.ConnectionId] = userConnection;
            await Clients.Groups(userConnection.Room).SendAsync("RecevieMessage", _channelName, $"{userConnection.User} has joined {userConnection.Room}");
        }

    }
}
