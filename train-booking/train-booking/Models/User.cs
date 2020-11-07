using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace train_booking.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual TrainDriver TrainDrivers { get; set; }
        public virtual Dispatcher Dispatchers { get; set; }
    }
}
