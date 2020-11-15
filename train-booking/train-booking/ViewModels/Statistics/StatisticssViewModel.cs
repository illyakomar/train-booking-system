using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.ViewModels.Statistics
{
    public class StatisticssViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public string HealthStatus { get; set; }
        public int CertificateNumber { get; set; }

        public List<User> Users { get; set; }
        public List<Route> Routes { get; set; }
        public List<TrainDriver> TrainDrivers { get; set; }
        public List<Dispatcher> Dispatchers { get; set; }

    }
}
