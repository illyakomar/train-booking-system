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

        public async Task Insert(Wagon wagon)
        {
            _context.Wagon.Add(wagon);
            await _context.SaveChangesAsync();
        }

        public async Task<Wagon> GetById(int id)
        {
            return await _context.Wagon
                .Include(wagon => wagon.Train)
                .Include(wagon => wagon.Seats)
                .Where(wagon => wagon.WagonId == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateSeat(int wagonId, int placeNumber)
        {
            Seat seat = new Seat
            {
                SeatNumber = placeNumber,
                WagonId = wagonId,
                SeatAvailability = true
            };
            await _context.Seat.AddAsync(seat);
            await _context.SaveChangesAsync();
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
