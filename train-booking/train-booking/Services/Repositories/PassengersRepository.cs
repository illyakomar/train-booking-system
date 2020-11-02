using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Passengers;

namespace train_booking.Services.Repositories
{
    public class PassengersRepository : IPassengersRepository
    {
        private TrainBookingContext _context;
        private readonly UserManager<User> _userManager;

        public PassengersRepository(TrainBookingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<User> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> Update(PassengerFormViewModel model)
        {
            var user = _context.User.Where(x => x.Email == model.Email).FirstOrDefault();

            if (user != null)
            {
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.Passport = model.Passport;
                user.Email = model.Email;

                _context.User.Update(user);

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> UpdateAdmin(AdminFormViewModel model)
        {
            var user = _context.User.Where(x => x.Email == model.Email).FirstOrDefault();

            if (user != null)
            {
                user.LastName = model.LastName;
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.Email = model.Email;

                _context.User.Update(user);

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
