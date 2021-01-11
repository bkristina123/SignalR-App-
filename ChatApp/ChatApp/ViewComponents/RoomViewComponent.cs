using ChatApp.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace ChatApp.ViewComponents 
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public RoomViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _context.ChatUsers
                .Include(x=> x.Chat)
                .Where(x=> x.UserId.Equals(userId))
                .Select(x => x.Chat)
                .ToList();
            return View(chats);
        }
    }
}
