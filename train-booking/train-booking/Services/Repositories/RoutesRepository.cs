using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Routes;

namespace train_booking.Services.Repositories
{
    public class RoutesRepository : IRoutesRepository
    {
        private TrainBookingContext _context;

        public RoutesRepository(TrainBookingContext context)
        {
            _context = context;
        }

        public IEnumerable<Route> GetRoutes()
        {
            return _context.Route
                .Include(x => x.TrainDriver)
                .ThenInclude(x => x.User)
                .ToList<Route>();
        }

        public async Task<Route> GetByIdAsync(int id)
        {
            return await _context.Route.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<RoutesIndexViewModel> GetTrainDriverRouteByIdAsync(int trainDriverId)
        {
            return new RoutesIndexViewModel
            {
                Routes = await _context.Route
               .Where(x => x.TrainDriverId == trainDriverId)
               .Include(x => x.Train)
               .Include(x => x.TrainDriver)
               .ThenInclude(x => x.User)
               .ToListAsync()
            };
        }

        public async Task Insert(Route route)
        {
            _context.Route.Add(route);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Route route)
        {
            _context.Route.Update(route);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            Route routeFromDB = _context.Route.Where(x => x.Id == id).FirstOrDefault();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Route.Remove(routeFromDB);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        public async Task<Route> GetRouteByTrainId(int trainId)
        {
            return await _context.Route
                .Where(route => route.TrainId == trainId)
                .Include(route => route.Train)
                .ThenInclude(train => train.Wagon)
                .Include(route => route.TrainDriver)
                .ThenInclude(trainDriver => trainDriver.User)
                .FirstOrDefaultAsync();
        }
    }
}
