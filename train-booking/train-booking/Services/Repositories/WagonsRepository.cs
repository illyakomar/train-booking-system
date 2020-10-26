using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Wagons;

namespace train_booking.Services.Repositories
{
    public class WagonsRepository : IWagonsRepository
    {
        private TrainBookingContext _context;

        public WagonsRepository(
            TrainBookingContext context
        )
        {
            _context = context;
        }
        public async Task Insert(WagonViewModel model)
        {
            var wag = new Wagon
            {
                TypeWagon = model.TypeWagon,
                PlaceCount = model.PlaceCount,
                PlacePrice = model.PlacePrice,
                TrainId = model.TrainId
            };

            _context.Wagon.Add(wag);

            await _context.SaveChangesAsync();

        }

        public WagonViewModel GetById(int id)
        {
            Wagon wagonFromDB = _context.Wagon.Include(x => x.Train).Where(x => x.WagonId == id).FirstOrDefault();
            return new WagonViewModel()
            {
                WagonId = wagonFromDB.WagonId,
                TypeWagon = wagonFromDB.TypeWagon,
                PlaceCount = wagonFromDB.PlaceCount,
                PlacePrice = wagonFromDB.PlacePrice,
                Train = wagonFromDB.Train
            };
        }
        public async Task Update(WagonViewModel model)
        {
            var wagon = _context.Wagon.Where(m => m.WagonId == model.WagonId).FirstOrDefault();

            if (wagon != null)
            {
                wagon.TypeWagon = model.TypeWagon;
                wagon.PlaceCount = model.PlaceCount;
                wagon.PlacePrice = model.PlacePrice;
                
                _context.Wagon.Update(wagon);

                await _context.SaveChangesAsync();
            }

        }
        public IEnumerable<WagonViewModel> GetWagonByTrainId(int id)
        {
            List<WagonViewModel> wagons = _context.Wagon
                .Include(x => x.Train)
                .Where(x => x.TrainId == id)
                .Select(model => new WagonViewModel
                {
                    WagonId = model.WagonId,
                    TypeWagon = model.TypeWagon,
                    PlaceCount = model.PlaceCount,
                    PlacePrice = model.PlacePrice,
                    Train = model.Train,
                    TrainId = model.TrainId
                })
                .ToList();

            return wagons;
        }
        public async Task Delete(int id)
        {
            Wagon wagonFromDB = _context.Wagon.Where(x => x.WagonId == id).FirstOrDefault();

            _context.Wagon.Remove(wagonFromDB);
            await _context.SaveChangesAsync();
        }

      
    }
}
