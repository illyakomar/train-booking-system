using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Models
{
    public class Transportation
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int TicketId { get; set; }
        public int DispatcherId { get; set; }
        public int TrainId { get; set; }
        public int TrainDriverId { get; set; }

        public virtual Route Route { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Dispatcher Dispatcher { get; set; }
        public virtual Train Train { get; set; }
        public virtual TrainDriver TrainDriver { get; set; }

    }
}
