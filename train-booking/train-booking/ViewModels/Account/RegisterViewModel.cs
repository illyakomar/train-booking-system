using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле Email є обов'язкове.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Пароль є обов'язкове.")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Пароль занадто короткий")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Підтвердження паролю є обов'язкове.")]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Пароль занадто короткий")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Номер паспорту")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "Поле Прізвище є обов'язкове.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле Ім'я є обов'язкове.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле По батькові є обов'язкове.")]
        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        public string Role { get; set; }

    }
}
