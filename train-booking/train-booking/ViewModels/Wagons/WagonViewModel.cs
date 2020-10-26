using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.ViewModels.Wagons
{
    public class WagonViewModel
    {
        public int WagonId { get; set; }

        [Required(ErrorMessage = "Поле Тип вагону обов'язкове.")]
        [Display(Name = "Тип вагону")]
        public string TypeWagon { get; set; }

        [Required(ErrorMessage = "Поле Кількість місць в вагоні обов'язкове.")]
        [Display(Name = "Кількість місць")]
        public int PlaceCount { get; set; }

        [Required(ErrorMessage = "Поле Ціна квитка обов'язкове.")]
        [Display(Name = "Ціна квитка")]
        public int PlacePrice { get; set; }
        public Train Train { get; set; }
        public int TrainId { get; set; }

    }
}
