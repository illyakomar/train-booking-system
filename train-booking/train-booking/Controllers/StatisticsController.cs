using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using train_booking.Data;

namespace train_booking.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly TrainBookingContext _context;

        public StatisticsController(
            TrainBookingContext context 
        )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Json(new int[0]);
        }

        [HttpGet]
        public async Task<IActionResult> GetWagonOnRouteBarData(int count = 10)
        {
            var json = await _context.Route
                .Include(x => x.Train)
                .Select(x => new
                {
                    x.DeparturePoint,
                    x.Destination,
                    Wagons = x.Train.Wagon.Count
                })
                .ToListAsync();

            return Json(json.Take(count));
        }

    }
}
