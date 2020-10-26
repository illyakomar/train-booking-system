using System;

namespace train_booking.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int PlaceNumber { get; set; }
        public bool Benefits { get; set; }
        public DateTime CreationTime { get; set; }
        public bool AdditionalServices { get; set; }
        public int UserId { get; set; }
        public int TrainId { get; set; }

        public virtual User User { get; set; }
        public virtual Train Train { get; set; }
    }
}
