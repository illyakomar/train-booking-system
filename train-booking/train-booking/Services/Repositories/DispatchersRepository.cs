using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Dispatchers;

namespace train_booking.Services.Repositories
{
    public class DispatchersRepository : IDispatchersRepository
    {
        private TrainBookingContext _context;

        public DispatchersRepository(TrainBookingContext context)
        {
            _context = context;
        }

        public async Task<Dispatcher> GetByUserId(string id)
        {
            return await _context.Dispatcher.Include(x => x.User).FirstAsync(x => x.UserId == id);
        }
        public async Task<Dispatcher> GetById(int id)
        {
            return await _context.Dispatcher.Include(x => x.User).FirstOrDefaultAsync(x => x.DispatcherId == id);
        }

        public async Task<bool> Update(int id, DispatcherFormViewModel model)
        {
            var dispatcher = await GetById(id);

            if (dispatcher != null)
            {
                dispatcher.BirthDate = model.BirthDate;
                dispatcher.Address = model.Address;

                _context.Dispatcher.Update(dispatcher);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> Remove(int id)
        {
            var dispatcher = await GetById(id);

            if (dispatcher != null)
            {
                _context.Dispatcher.Remove(dispatcher);
                _context.User.Remove(dispatcher.User);

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
