using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.Wagons;

namespace train_booking.Services.Interfaces
{
    public interface IWagonsRepository
    {
        Task Insert(WagonViewModel model);
        WagonViewModel GetById(int id);
        Task Update(WagonViewModel model);
        IEnumerable<WagonViewModel> GetWagonByTrainId(int id);
        Task Delete(int id);
        //Task<List<RouteWagon>> GetAllNextRouteWagons();
        //Task<List<RouteWagon>> GetAllNextCourseModulesAvaliableForStudent(string channelId);
        //Wagon GetWagone(int routeId, int wagonId);
        //Route GetRouteById(int id);
    }
}
