using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int PlaceNumber { get; set; }
        public int Price { get; set; }
        public bool Benefits { get; set; }
        public DateTime CreationTime { get; set; }
        public bool AdditionalServices { get; set; }

        public virtual User User { get; set; }
    }
}
