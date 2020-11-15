using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Account;
using train_booking.ViewModels.TrainDrivers;

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

        public async Task<bool> Update(int id, TrainDriverFormViewModel model)
        {
            var trainDriver = await GetById(id);

            if (trainDriver != null)
            {
                trainDriver.BirthDate = model.BirthDate;
                trainDriver.HealthStatus = model.HealthStatus;
                trainDriver.CertificateNumber = model.CertificateNumber;

                _context.TrainDriver.Update(trainDriver);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<TrainDriver> GetById(int id)
        {
            return await _context.TrainDriver.Include(x => x.User).FirstOrDefaultAsync(x => x.TrainDriverId == id);
        }

        public async Task<bool> Remove(int id)
        {
            var trainDriver = await GetById(id);

            if (trainDriver != null)
            {
                _context.TrainDriver.Remove(trainDriver);
                _context.User.Remove(trainDriver.User);

                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<IEnumerable<TrainDriverViewModel>> GetTrainDrivers()
        {
            List<TrainDriverViewModel> trainDrivers = new List<TrainDriverViewModel>();
            foreach (TrainDriver trainDriver in await _context.TrainDriver.Include(trainDriver => trainDriver.User).ToListAsync())
            {
                trainDrivers.Add(new TrainDriverViewModel()
                {
                    TrainDriverId = trainDriver.TrainDriverId,
                    User = new UserViewModel(trainDriver.User)
                });
            }
            return trainDrivers;
        }

        public IEnumerable<TrainDriver> GetTrainDriversForStatic()
        {
            return _context.TrainDriver
                .ToList<TrainDriver>();
        }
    }
}
