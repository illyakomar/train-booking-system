﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Dispatcher
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

        public virtual User User { get; set; }
    }
}
