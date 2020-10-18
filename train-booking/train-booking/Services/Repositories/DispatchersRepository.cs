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
    }
}
