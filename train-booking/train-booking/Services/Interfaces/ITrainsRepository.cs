using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.ViewModels.Trains;

namespace train_booking.Services.Interfaces
{
    public interface ITrainsRepository
    {
        Task Insert(TrainViewModel model);
        TrainViewModel GetById(int id);
        Task Update(TrainViewModel model);
        IEnumerable<TrainViewModel> GetTrains();
        Task Delete(int id);
    }
}
