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

        //public async Task<List<RouteWagon>> GetAllNextRouteWagons()
        //{
        //    var nextCourseModules = await _context.CourseModule.Include(cm => cm.Module).Include(cm => cm.Course).ThenInclude(c => c.CourseStudent).Where(cm => cm.Date >= DateTime.Now || cm.StartTime >= DateTime.Now.TimeOfDay.Add(new TimeSpan(-2, 0, 0))).ToListAsync();
        //    nextCourseModules = nextCourseModules.OrderBy(cm => cm.Date).ToList();
        //    return nextCourseModules;
        //}

        //public async Task<List<RouteWagon>> GetAllNextCourseModulesAvaliableForStudent(string channelId)
        //{
        //    var userId = _context.BotAuthorizedUsers.Where(u => u.ChannelUserId == channelId).SingleOrDefault().UserId;
        //    var student = await _context.Student.Include(x => x.User).FirstAsync(x => x.UserId == userId);
        //    var nextCourseModules = await GetAllNextCourseModules();
        //    return nextCourseModules.Where(cm => cm.Course.CourseStudent.Any(cs => cs.StudentId == student.StudentId)).ToList();
        //}

        //public Wagon GetWagone(int routeId, int wagonId)
        //{
        //    return _context.RouteWagon
        //        .Include(x => x.Module)
        //        .Where(x => x.RouteId == routeId)
        //        .Select(x => x.Module)
        //        .Where(x => x.WagonId == wagonId)
        //        .FirstOrDefault();
        //}
        //public Route GetRouteById(int id)
        //{
        //    return _context.Route
        //        .Include(x => x.Teacher)
        //        .Include(x => x.CourseModule)
        //        .Include(x => x.CourseStudent)
        //        .Include(x => x.Subject)
        //        .Where(x => x.CourseId == id)
        //        .FirstOrDefault();
        //}

    }
}
