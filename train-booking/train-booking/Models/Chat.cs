﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime ChatDate { get; set; }
    }
}
