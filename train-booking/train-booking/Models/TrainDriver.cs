using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class TrainDriver
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TrainId { get; set; }
        public DateTime BirthDate { get; set; }
        public string HealthStatus { get; set; }
        public int CertificateNumber { get; set; }

        public virtual User User { get; set; }
        public virtual Train Train { get; set; }

    }
}
