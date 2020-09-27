using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Train
    {
        public int Id { get; set; }
        public int TrainDriverId { get; set; }
        public int NumberTrain { get; set; }
        public string TypeTrain { get; set; }
        public string TypeWagon { get; set; }
        public int WagonsCount { get; set; }
        public int PlaceCount { get; set; }

        public virtual ICollection<Route> Routes { get; set; }
        public virtual TrainDriver TrainDriver { get; set; }
    }
}
