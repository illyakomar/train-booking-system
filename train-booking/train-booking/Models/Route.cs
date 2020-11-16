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
        public string NavigationUrl { get; set; }
        public int TrainId { get; set; }
        public int TrainDriverId { get; set; }

        public virtual Train Train { get; set; }
        public virtual TrainDriver TrainDriver { get; set; }
    }
}
