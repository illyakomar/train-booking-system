using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.ViewModels.Account
{
    public class LoginAndRegisterViewModel
    {
        public LoginViewModel Login {get; set;}
        public RegisterViewModel Register { get; set; }

    }
}
