using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using train_booking.Data;
using train_booking.Models;

namespace train_booking.Controllers
{
    public class MyHistoryController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly TrainBookingContext _context;

        public MyHistoryController(
            UserManager<User> userManager,
            TrainBookingContext context
        )
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> History()
        {
            string userId = (await _userManager.GetUserAsync(User)).Id;
            List<Seat> seats = await _context.Seat
                .Where(seat => seat.UserId == userId)
                .Include(seat => seat.Wagon)
                .ThenInclude(wagon => wagon.Train.Route)
                .ToListAsync();
            return View(seats);
        }

    }
}
