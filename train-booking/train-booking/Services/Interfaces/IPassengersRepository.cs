using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.Passengers;

namespace train_booking.Services.Interfaces
{
    public interface IPassengersRepository
    {
        Task<User> GetById(string id);
        Task<bool> Update(PassengerFormViewModel model);
        Task<bool> UpdateAdmin(AdminFormViewModel model);
    }
}
