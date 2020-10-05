using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.ViewModels.Account;
using train_booking.Models;

namespace train_booking.ViewModels.Dispatchers
{
    public class DispatcherViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }

        public UserViewModel User { get; set; }

        public DispatcherViewModel() { }

        public DispatcherViewModel(Dispatcher dispatcher)
        {
            Id = dispatcher.Id;
            UserId = dispatcher.UserId;
            BirthDate = dispatcher.BirthDate;
            Address = dispatcher.Address;
            User = new UserViewModel(dispatcher.User);
        }


        public DispatcherViewModel(DispatcherFormViewModel model)
        {
            BirthDate = model.BirthDate;
            Address = model.Address;
        }
    }
}
