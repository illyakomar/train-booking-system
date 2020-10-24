using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.TrainDrivers;

namespace train_booking.ViewModels.Routes
{
    public class RouteDetailsViewModel
    {
        public Route Route { get; set; }
        public Train Train { get; set; }
        public TrainDriverViewModel TrainDriver { get; set; }
        public List<RouteDetailsWagonViewModel> Wagones { get; set; }
    }
}
