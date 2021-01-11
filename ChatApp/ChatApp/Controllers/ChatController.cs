using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Database;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[Controller]")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hub;

        public ChatController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        [HttpPost("[Action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName)
        {
            await _hub.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            await _hub.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        public async Task<IActionResult> SendMessage(int chatId,
            string message, 
            string roomName,
            [FromServices] AppDbContext context)
        {
            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            context.Messages.Add(Message);

            await _hub.Clients.Group(roomName)
                .SendAsync("RecieveMessage", Message);
            return Ok();
        }
    }
}