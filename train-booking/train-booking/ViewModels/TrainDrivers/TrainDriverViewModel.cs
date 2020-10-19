using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.Account;

namespace train_booking.ViewModels.TrainDrivers
{
    public class TrainDriverViewModel
    {
        public int TrainDriverId { get; set; }
        public string UserId { get; set; }
        public string TrainId { get; set; }
        public DateTime BirthDate { get; set; }
        public string HealthStatus { get; set; }
        public int CertificateNumber { get; set; }

        public UserViewModel User { get; set; }

        public TrainDriverViewModel() { }

        public TrainDriverViewModel(TrainDriver trainDriver)
        {
            TrainDriverId = trainDriver.TrainDriverId;
            UserId = trainDriver.UserId;
            BirthDate = trainDriver.BirthDate;
            HealthStatus = trainDriver.HealthStatus;
            CertificateNumber = trainDriver.CertificateNumber;
            User = new UserViewModel(trainDriver.User);
        }


        public TrainDriverViewModel(TrainDriverFormViewModel model)
        {
            BirthDate = model.BirthDate;
            HealthStatus = model.HealthStatus;
            CertificateNumber = model.CertificateNumber;
        }
    }
}
