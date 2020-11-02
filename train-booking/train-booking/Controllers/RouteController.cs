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
using train_booking.ViewModels.Account;
using train_booking.ViewModels.Routes;
using train_booking.ViewModels.TrainDrivers;
using train_booking.ViewModels.Trains;

namespace train_booking.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRoutesRepository _routesRepository;
        private readonly ITrainsRepository _trainsRepository;
        private readonly IWagonsRepository _wagonsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly UserManager<User> _userManager;

        public RouteController(
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
        [Authorize(Roles = "Administrator,Dispatcher,TrainDriver")]
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

        [Authorize(Roles = "Administrator,Dispatcher")]
        public async Task<IActionResult> Create()
        {
            IEnumerable<TrainViewModel> trains = _trainsRepository.GetTrains();
            IEnumerable<TrainDriverViewModel> trainDriver = await _trainDriversRepository.GetTrainDrivers();

            RoutesViewModel route = new RoutesViewModel
            {
                DeparturePointDate = DateTime.Now.Date,
                DestinationDate = DateTime.Now.Date
            };


            ViewData["Train"] = trains;
            ViewData["TrainDriver"] = trainDriver;

            return View(route);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        public async Task<IActionResult> Create(RoutesViewModel routes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Route route = new Route()
                    {
                        Id = routes.Id,
                        TrainId = routes.TrainId,
                        Distance = routes.Distance,
                        TrainDriverId = routes.TrainDriverId,
                        Destination = routes.Destination,
                        DeparturePoint = routes.DeparturePoint,
                        DestinationDate = routes.DestinationDate,
                        DeparturePointDate = routes.DeparturePointDate
                    };
                    await _routesRepository.Insert(route);
                    return RedirectToAction("Index", "Route", new { message = "Маршрут успішно додано!" });
                }
            }
            catch
            {
                return RedirectToAction("Index", "Route", new { error = "Не можливо створити маршрут, спочатку видаліть маршрут з потрібним потягом!" });
            }
            return RedirectToAction("Index", "Route", new { error = "При створенні маршруту виникла невідома помилка!" });
        }

        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{routeId}")]
        public async Task<IActionResult> Edit(int routeId)
        {
            Route route = await _routesRepository.GetByIdAsync(routeId);
            ViewData["Train"] = _trainsRepository.GetTrains();
            ViewData["TrainDriver"] = await _trainDriversRepository.GetTrainDrivers();

            if (User.IsInRole("Dispatcher") || User.IsInRole("Administrator"))
            {
                RoutesViewModel model = new RoutesViewModel
                {
                    Id = route.Id,
                    Distance = route.Distance,
                    TrainDriverId = route.TrainDriverId,
                    TrainId = route.TrainId,
                    Destination = route.Destination,
                    DeparturePoint = route.DeparturePoint,
                    DestinationDate = route.DestinationDate,
                    DeparturePointDate = route.DeparturePointDate
                };
                return View(model);
            }

            return RedirectToAction("Index", "Route", new { error = "Ви успішно відредагували маршрут!" });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{routeId}")]
        public async Task<IActionResult> Edit(RoutesViewModel route)
        {
            if (ModelState.IsValid)
            {
                var oldRoute = await _routesRepository.GetByIdAsync(route.Id);

                oldRoute.TrainId = route.TrainId;
                oldRoute.Distance = route.Distance;
                oldRoute.TrainDriverId = route.TrainDriverId;
                oldRoute.Destination = route.Destination;
                oldRoute.DeparturePoint = route.DeparturePoint;
                oldRoute.DestinationDate = route.DestinationDate;
                oldRoute.DeparturePointDate = route.DeparturePointDate;

                await _routesRepository.Update(oldRoute);

                return RedirectToAction("Index", "Route", new { message = "Ви успішно відредагували маршрут!" });
            }
            return View(route);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{routeId}")]
        public async Task<IActionResult> Delete(int routeId)
        {
            bool res = await _routesRepository.Delete(routeId);
            if (res)
            {
                return RedirectToAction("Index", "Route", new { message = "Маршрут успішно видалений!" });
            }
            else
            {
                return RedirectToAction("Index", "Route", new { error = "Сталася невідома помилка при видаленні маршруту!" });
            }

        }

        [Authorize(Roles = "Administrator,Dispatcher,TrainDriver")]
        [Route("{controller}/{action}/{trainId}")]
        public async Task<IActionResult> Details(int trainId)
        {
            Route route = await _routesRepository.GetRouteByTrainId(trainId);
            return View(route);
        }
    }
}
