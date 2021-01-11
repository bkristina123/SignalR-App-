using ChatApp.Database;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var chats = _context.Chats
                .Include(x => x.Users)
                .Where(x=> !x.Users
                .Any(y => y.UserId
                    .Equals(User.FindFirst(ClaimTypes.NameIdentifier).Value))).
                ToList();

            return View(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            var chatUser = new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Admin
            };

            chat.Users.Add(chatUser);

            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                Role = UserRole.Member

            };

            _context.ChatUsers.Add(chatUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat", "Home", new { Id = id});
        }

        public IActionResult Chat(int id)
        {
            var chat = _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x=> x.Id.Equals(id));
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string text)
        {
            var message = new Message
            {
                ChatId = chatId,
                Text = text,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return RedirectToAction("Chat", new { Id = chatId});
        }
    }
}
