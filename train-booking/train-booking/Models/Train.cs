using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Train
    {
        public int TrainId { get; set; }
        public int NumberTrain { get; set; }
        public string TypeTrain { get; set; }

        public virtual ICollection<Route> Route { get; set; }
        public virtual ICollection<Wagon> Wagon { get; set; }
    }
}
