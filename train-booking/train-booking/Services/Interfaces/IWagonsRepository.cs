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
        Task Insert(Wagon wagon);
        Task<Wagon> GetById(int id);
        Task Update(WagonViewModel model);
        IEnumerable<WagonViewModel> GetWagonByTrainId(int id);
        Task Delete(int id);
        Task CreateSeat(int wagonId, int placeNumber);
    }
}
