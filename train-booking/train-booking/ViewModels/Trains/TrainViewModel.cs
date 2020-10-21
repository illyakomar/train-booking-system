using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.ViewModels.Trains
{
    public class TrainViewModel
    {
        public int TrainId { get; set; }

        [Required(ErrorMessage = "Поле номер потяга є обов'язкове.")]
        [Display(Name = "Номер потягу")]
        public int NumberTrain { get; set; }

        [Required(ErrorMessage = "Поле Тип потяга є обов'язкове.")]
        [Display(Name = "Тип потягу")]
        public string TypeTrain { get; set; }

        public IEnumerable<Wagon> Wagon { get; set; }
    }
}
