using System;
using System.ComponentModel.DataAnnotations;
using train_booking.ViewModels.TrainDrivers;
using train_booking.ViewModels.Trains;

namespace train_booking.ViewModels.Routes
{
    public class RoutesViewModel
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public TrainViewModel Train { get; set; }
        public int TrainDriverId { get; set; }
        public TrainDriverViewModel TrainDriver { get; set; }
        [Required(ErrorMessage = "Поле Пункт прибуття обов'язкове.")]
        [Display(Name = "Пункт Прибуття")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Поле Пункт Відправлення обов'язкове.")]
        [Display(Name = "Пункт Відправлення")]
        public string DeparturePoint { get; set; }
        public string NavigationUrl { get; set; }
        [StartLessThanEnd("DestinationDate", ErrorMessage = "Дата та час Відправлення не може бути більшою за дату та час прибуття.")]
        [Required(ErrorMessage = "Дата та час Відправлення обов'язкове.")]
        public DateTime DeparturePointDate { get; set; }
        [Required(ErrorMessage = "Дата та час прибуття обов'язкове.")]
        public DateTime DestinationDate { get; set; }
        [Required(ErrorMessage = "Поле Дистанція обов'язкове.")]
        [Display(Name = "Пункт Дистанція обов'язкове")]
        public int Distance { get; set; }
    }
}
