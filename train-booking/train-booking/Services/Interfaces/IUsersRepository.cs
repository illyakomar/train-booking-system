using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.Services.Interfaces
{
    public interface IUsersRepository
    {
        Task<bool> IsEmailUnique(string email);
        Task<string> GetUserEmailById(string id);
        Task<User> GetUserById(string id);
        Task<User> GetUserByLogin(string userName);
        Task AddResetCodeForUser(User user, string resetCode);
        Task ClearResetCodeForUser(User user);
    }
}
