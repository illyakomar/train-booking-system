using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Route
    {
        public int Id { get; set; }
        public int Distance { get; set; }
        public string Destination { get; set; }
        public string DeparturePoint { get; set; }
        public DateTime DestinationDate { get; set; }
        public DateTime DeparturePointDate { get; set; }

        public virtual User User { get; set; }

    }
}
