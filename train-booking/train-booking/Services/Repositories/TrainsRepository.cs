using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Trains;

namespace train_booking.Services.Repositories
{
    public class TrainsRepository : ITrainsRepository
    {
        private TrainBookingContext _context;
        public TrainsRepository(TrainBookingContext context)
        {
            _context = context;
        }
        public async Task Insert(TrainViewModel model)
        {
            _context.Train.Add(new Train
            {
                NumberTrain = model.NumberTrain,
                TypeTrain = model.TypeTrain
            });
            await _context.SaveChangesAsync();
        }

        public TrainViewModel GetById(int id)
        {
            Train trainFromDB = _context.Train.Include(x => x.Wagon).Where(x => x.TrainId == id).FirstOrDefault();

            if (trainFromDB == null) return null;

            return new TrainViewModel()
            {
                TrainId = trainFromDB.TrainId,
                NumberTrain = trainFromDB.NumberTrain,
                TypeTrain = trainFromDB.TypeTrain
            };
        }
        public async Task Update(TrainViewModel model)
        {
            _context.Train.Update(new Train()
            {
                TrainId = model.TrainId,
                NumberTrain = model.NumberTrain,
                TypeTrain = model.TypeTrain
            });
            await _context.SaveChangesAsync();
        }
        public IEnumerable<TrainViewModel> GetTrains()
        {
            List<TrainViewModel> trains = new List<TrainViewModel>();
            foreach (Train train in _context.Train.Include(x => x.Wagon).ToList<Train>())
            {
                trains.Add(new TrainViewModel()
                {
                    TrainId = train.TrainId,
                    NumberTrain = train.NumberTrain,
                    TypeTrain = train.TypeTrain,
                    Wagon = train.Wagon
                });
            }
            return trains;
        }
        public async Task Delete(int id)
        {
            Train trainFromDB = _context.Train.Where(x => x.TrainId == id).FirstOrDefault();
            _context.Train.Remove(trainFromDB);
            await _context.SaveChangesAsync();
        }
    }
}
