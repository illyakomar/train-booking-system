using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Account;
using train_booking.ViewModels.Dispatchers;
using train_booking.ViewModels.TrainDrivers;

namespace train_booking.Services.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private TrainBookingContext _context;
        private UserManager<User> _userManager;

        public UsersRepository(TrainBookingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> RegisterDispatcher(UserViewModel user, DispatcherViewModel model)
        {
           // await RegisterUser(user, "Dispatcher");

            var userFromDb = _context.User.Where(x => x.Email == user.Email).FirstOrDefault();

            _context.Dispatcher.Add(new Dispatcher
            {
                User = userFromDb,
                BirthDate = model.BirthDate,
                Address = model.Address
     
            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RegisterTrainDriver(UserViewModel user, TrainDriverViewModel model)
        {
          //  await RegisterUser(user, "TrainDriver");

            var userFromDb = _context.User.Where(x => x.Email == user.Email).FirstOrDefault();

            _context.TrainDriver.Add(new TrainDriver
            {
                User = userFromDb,
                BirthDate = model.BirthDate,
                HealthStatus = model.HealthStatus,
                CertificateNumber = model.CertificateNumber

            });

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(UserViewModel model)
        {
            var user = _context.User.Where(x => x.Email == model.Email).FirstOrDefault();

            if (user != null)
            {
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.Passport = model.Passport;

                _context.User.Update(user);

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }


        public async Task<bool> IsEmailUnique(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == email) == null;
        }

        public async Task<string> GetUserEmailById(string id)
        {
            return (await _userManager.FindByIdAsync(id)).Email;
        }

       

        public async Task<User> GetUserById(string id)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByLogin(string userName)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddResetCodeForUser(User user, string resetCode)
        {
            user.ResetCode = resetCode;
            user.LastResetCodeCreationTime = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task ClearResetCodeForUser(User user)
        {
            user.ResetCode = null;
            await _context.SaveChangesAsync();
        }
    }
}
