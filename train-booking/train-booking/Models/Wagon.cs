using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Wagon
    {
        public int WagonId { get; set; }
        public int TrainId { get; set; }
        public string TypeWagon { get; set; }
        public int PlaceCount { get; set; }

        public virtual Train Train { get; set; }
    }
}
