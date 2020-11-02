using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.ViewModels.Passengers
{
    public class AdminFormViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле Прізвище обов'язкове.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле Ім'я обов'язкове.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле По батькові обов'язкове.")]
        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле Email обов'язкове.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль є обов'язкове.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Підтвердження паролю є обов'язкове.")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        public string ConfirmPassword { get; set; }
    }
}
