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
    public class TrainDriversRepository : ITrainDriversRepository
    {
        private TrainBookingContext _context;

        public TrainDriversRepository(TrainBookingContext context)
        {
            _context = context;
        }

        public async Task<TrainDriver> GetByUserId(string id)
        {
            return await _context.TrainDriver.Include(x => x.User).FirstAsync(x => x.UserId == id);
        }
    }
}
