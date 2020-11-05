using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using train_booking.Data;
using train_booking.Models;
using train_booking.ViewModels.Chats;

namespace train_booking.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TrainBookingContext _context;

        public ChatController
        (
            UserManager<User> userManager,
            TrainBookingContext context
        )
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Administrator,Dispatcher,TrainDriver")]
        public IActionResult Index()
        {
            var model = new ChatViewModel
            {
                UserId = _userManager.GetUserId(User),

                Messages = _context.Chat.Select(x => new MessageViewModel
                {
                    user = _userManager.FindByIdAsync(x.UserId).Result,
                    message = x.Message,
                    date = x.ChatDate
                }).OrderBy(x => x.date).ToList()
            };

            return View(model);
        }
    }
}
