using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendDefaultEmailAsync(string email, string subject, string message, string buttonLink, string buttonText);
    }
}
