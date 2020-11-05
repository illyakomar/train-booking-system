using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.ViewModels.Chats
{
    public class MessageViewModel
    {
        public User user { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }
}
