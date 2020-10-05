using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace train_booking.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string username, string message);
        //Task SendMarkChatEmailAsync(string email, string subject, string message, string userFromFullName, string emailFrom, string courseName, string moduleName, int? testMark, int? labMark, string buttonLink);
    }
}
