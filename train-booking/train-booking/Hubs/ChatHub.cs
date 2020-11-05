using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;

namespace train_booking.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly TrainBookingContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public ChatHub(
            UserManager<User> userManager,
            TrainBookingContext context,
            IHttpContextAccessor httpContext
        )
        {
            _userManager = userManager;
            _context = context;
            _httpContext = httpContext;
        }

        public async Task SendMessage(string user, string message)
        {
            _context.Chat.Add(new Chat
            {
                UserId = user,
                Message = message
            });

            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", await _userManager.FindByIdAsync(user), message);
        }

    }
}
