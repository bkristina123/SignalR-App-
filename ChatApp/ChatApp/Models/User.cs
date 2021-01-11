using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class User : IdentityUser
{
    public ICollection<ChatUser> Chats { get; set; }
}