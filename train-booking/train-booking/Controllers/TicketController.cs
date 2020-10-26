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
    public class TicketController : Controller
    {
        private readonly IRoutesRepository _routesRepository;
        private readonly ITrainsRepository _trainsRepository;
        private readonly IWagonsRepository _wagonsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly UserManager<User> _userManager;

        public TicketController(
            IRoutesRepository routesRepository,
            ITrainsRepository trainsRepository,
            IWagonsRepository wagonsRepository,
            IUsersRepository usersRepository,
            ITrainDriversRepository trainDriversRepository,
            UserManager<User> userManager
        )
        {
            _routesRepository = routesRepository;
            _trainsRepository = trainsRepository;
            _wagonsRepository = wagonsRepository;
            _usersRepository = usersRepository;
            _trainDriversRepository = trainDriversRepository;
            _userManager = userManager;
        }

        [Route("{controller}")]
        [Authorize(Roles = "Administrator, Dispatcher, TrainDriver, Passenger")]
        public IActionResult Index(string message = null, string error = null)
        {
            ViewBag.Message = message;
            ViewBag.Error = error;

            RoutesIndexViewModel routesIndexViewModel = new RoutesIndexViewModel()
            {
                Routes = _routesRepository.GetRoutes().ToList()
            };
            return View(routesIndexViewModel);
        }
    }
}
