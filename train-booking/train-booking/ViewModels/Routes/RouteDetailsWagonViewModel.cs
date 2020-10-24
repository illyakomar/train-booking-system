using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.ViewModels.Routes
{
    public class RouteDetailsWagonViewModel
    {
        public Wagon Wagon { get; set; }
        public int NumberTrain { get; set; }
        public string TypeTrain { get; set; }
 
    }
}
