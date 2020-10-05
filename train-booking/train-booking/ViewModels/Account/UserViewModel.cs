using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
namespace train_booking.ViewModels.Account
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле Email обов'язкове.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле Прізвище обов'язкове.")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле Ім'я обов'язкове.")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле По Батькові обов'язкове.")]
        [Display(Name = "По Батькові")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле паспорт обов'язкове.")]
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }

        [Required(ErrorMessage = "Поле Пароль обов'язкове.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public UserViewModel() { }

        
        public UserViewModel(User model)
        {
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
            Passport = model.Passport;
            UserName = model.Email;
            
        }

        public UserViewModel(RegisterViewModel model)
        {
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
            Password = model.Password;
            Passport = model.Passport;
            UserName = model.Email;
            
        }

        

    }
}
