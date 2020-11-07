using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.Account;
using train_booking.ViewModels.Dispatchers;
using train_booking.ViewModels.TrainDrivers;

namespace train_booking.Services.Interfaces
{
    public interface IUsersRepository
    {
        Task<bool> RegisterDispatcher(DispatcherViewModel model);
        Task<bool> RegisterTrainDriver(TrainDriverViewModel model);
        Task<bool> Update(UserViewModel model);
        Task<bool> IsEmailUnique(string email);
        Task<string> GetUserEmailById(string id);
        Task<User> GetUserById(string id);
        Task<User> GetUserByLogin(string userName);
    }
}
