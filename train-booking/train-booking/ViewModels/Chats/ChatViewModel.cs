using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.ViewModels.Chats
{
    public class ChatViewModel
    {
        public string UserId { get; set; }
        public List<User> Users { get; set; }
        public List<MessageViewModel> Messages { get; set; }
    }
}
