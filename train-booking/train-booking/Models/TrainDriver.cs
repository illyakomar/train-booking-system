﻿using System;

namespace train_booking.Models
{
    public class TrainDriver
    {
        public int TrainDriverId { get; set; }
        public string UserId { get; set; }
        public string TrainId { get; set; }
        public DateTime BirthDate { get; set; }
        public string HealthStatus { get; set; }
        public int CertificateNumber { get; set; }

        public virtual User User { get; set; }
    }
}
