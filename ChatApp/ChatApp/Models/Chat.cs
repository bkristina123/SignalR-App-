using System.Collections.Generic;

namespace ChatApp.Models
{
    public class Chat
    {
        public Chat()
        {
            Users = new List<ChatUser>();
        }

        public int Id { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ChatType Type { get; set; }
        public string Name { get; set; }
        public ICollection<ChatUser> Users { get; set; }

    }
}
