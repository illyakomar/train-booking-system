using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.ViewModels.Dispatchers
{
    public class DispatcherFormViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле Прізвище обов'язкове.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле Ім'я обов'язкове.")]
        [Display(Name = "ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле По батькові обов'язкове.")]
        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле Email обов'язкове.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Паспорт обов'язкове.")]
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "Поле Пароль є обов'язкове.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Підтвердження паролю є обов'язкове.")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле День народження обов'язкове.")]
        [Display(Name = "День народження")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Поле Адреса обов'язкове.")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        public int DispatcherId { get; set; }
    }
}
