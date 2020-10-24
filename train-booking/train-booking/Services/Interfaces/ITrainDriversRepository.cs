using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.TrainDrivers;

namespace train_booking.Services.Interfaces
{
    public interface ITrainDriversRepository
    {
        Task<TrainDriver> GetByUserId(string id);
        Task<bool> Update(int id, TrainDriverFormViewModel model);
        Task<TrainDriver> GetById(int id);
        Task<bool> Remove(int id);
        Task<IEnumerable<TrainDriverViewModel>> GetTrainDrivers();


    }
}
