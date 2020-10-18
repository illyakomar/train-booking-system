using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Models;

namespace train_booking.Services.Interfaces
{
    public interface IDispatchersRepository
    {
        Task<Dispatcher> GetByUserId(string id);
    }
}
