using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.Routes;

namespace train_booking.Services.Interfaces
{
    public interface IRoutesRepository
    {
        IEnumerable<Route> GetRoutes();
        Task Insert(Route route);
        Task<Route> GetByIdAsync(int id);
        Task Update(Route route);
        Task<bool> Delete(int id);
        Task<Route> GetRouteByTrainId(int trainId);
        Task<RoutesIndexViewModel> GetTrainDriverRouteByIdAsync(int trainDriverId);
    }
}
