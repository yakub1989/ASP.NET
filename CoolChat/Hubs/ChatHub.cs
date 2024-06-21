using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;  
using ChatApp.Models;
using System.Linq;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatContext _context;
        public ChatHub(ChatContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string user, string message, string room)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", user, message);
            var chatMessage = new ChatMessage
            {
                User = user,
                Message = message,
                Room = room
            };
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }
        public async Task JoinRoom(string room)
        {
            await  Groups.AddToGroupAsync(Context.ConnectionId, room);
            var messages = await _context.ChatMessages
            .Where(m => m.Room == room)
            .OrderBy(m => m.Timestamp)
            .ToListAsync();
            await Clients.Caller.SendAsync("LoadMessageHistory", messages);
        }
        public async Task LeaveRoom(string room)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }
        public async Task LoadAllRoomsMessages()
        {
            var messages = await _context.ChatMessages.ToListAsync();
            await Clients.Caller.SendAsync("LoadAllRoomsMessages", messages);
        }
    }
}