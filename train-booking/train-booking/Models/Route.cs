using System;
using System.Collections.Generic;

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

        public virtual ICollection<Train> Trains { get; set; }
    }
}
