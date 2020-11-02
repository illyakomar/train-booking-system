using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.ViewModels.Tickets
{
    public class ReservationConfirmationViewModel
    {
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "По Батькові")]
        public string MiddleName { get; set; }

        [Display(Name = "Паспорт")]
        public string Passport { get; set; }

        [Display(Name = "Пункт Відправлення")]
        public string DeparturePoint { get; set; }

        [Display(Name = "Пункт Призначення")]
        public string Destination { get; set; }

        [Display(Name = "Дата та час відправлення")]
        public DateTime? DeparturePointDate { get; set; }
        [Display(Name = "Дата та час призначення")]
        public DateTime? DestinationDate { get; set; }

        [Display(Name = "Тип вагона")]
        public string WagonType { get; set; }

        [Display(Name = "Номер вагона")]
        public int WagonId { get; set; }

        [Display(Name = "Місце")]
        public int SeatNumber { get; set; }

        [Display(Name = "Ціна")]
        public int PlacePrice { get; set; }
        public string UserId { get; set; }
        public int SeatId { get; set; }
    }
}
