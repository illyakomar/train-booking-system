using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int WagonId { get; set; }
        public int SeatNumber { get; set; }
        public bool SeatAvailability { get; set; }
        public DateTime LastBookingTime { get; set; }

        public virtual Wagon Wagon { get; set; }
        public virtual User User { get; set; }
    }
}
