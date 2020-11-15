using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;
using train_booking.ViewModels.Dispatchers;

namespace train_booking.Services.Interfaces
{
    public interface IDispatchersRepository
    {
        Task<Dispatcher> GetByUserId(string id);
        Task<bool> Update(int id, DispatcherFormViewModel model);
        Task<bool> Remove(int id);
        IEnumerable<Dispatcher> GetDispatcherForStatic();
    }
}
