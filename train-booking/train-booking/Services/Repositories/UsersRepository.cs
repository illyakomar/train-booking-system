using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;

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
