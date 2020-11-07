using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Routes;

namespace train_booking.Controllers
{
    public class ScheduleController : Controller
    {

        private readonly IRoutesRepository _routesRepository;

        public ScheduleController(
            IRoutesRepository routesRepository
        )
        {
            _routesRepository = routesRepository;
        }

        [Route("{controller}")]
        [Authorize(Roles = "Administrator,Dispatcher,TrainDriver, Passenger")]
        public IActionResult Index()
        {
            List<Route> routes = _routesRepository.GetRoutes().ToList();
            List<Route> departureRoutes = new List<Route>();

            DateTime currentTime = DateTime.Now;
                
            foreach (Route route in routes)
            {
                TimeSpan span = route.DeparturePointDate - currentTime;

                if (span.TotalMilliseconds >= 0)
                {
                    departureRoutes.Add(route);
                }
            }

            RoutesIndexViewModel routesIndexViewModel = new RoutesIndexViewModel()
            {
                Routes = departureRoutes
            };


            return View(routesIndexViewModel);
        }

    }
}
