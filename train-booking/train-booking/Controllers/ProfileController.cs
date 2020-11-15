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
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Statistics;

namespace train_booking.Controllers
{
   [Authorize(Roles = "Administrator, Dispatcher, TrainDriver, Passenger")]
    public class ProfileController : Controller
    {
        private readonly IDispatchersRepository _dispatchersRepository;
        private readonly IRoutesRepository _routesRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly TrainBookingContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileController
        (
            IUsersRepository usersRepository,
            IRoutesRepository routesRepository,
            IDispatchersRepository dispatchersRepository,
            ITrainDriversRepository trainDriversRepository,
            TrainBookingContext context,
            UserManager<User> userManager
        )
        {
            _usersRepository = usersRepository;
            _routesRepository = routesRepository;
            _dispatchersRepository = dispatchersRepository;
            _trainDriversRepository = trainDriversRepository;
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> Dispatcher(bool isModalWindowShowRequired = false)
        {
            string Id = _userManager.GetUserId(User);
            Dispatcher dispatcher = await _dispatchersRepository.GetByUserId(Id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            StatisticssViewModel statistics = new StatisticssViewModel()
            {
                Id = dispatcher.User.Id,
                FirstName = dispatcher.User.FirstName,
                LastName = dispatcher.User.LastName,
                MiddleName = dispatcher.User.MiddleName,
                Email = dispatcher.User.Email,
                Address = dispatcher.Address,
                Routes = _routesRepository.GetRoutes().ToList(),
                Users = _usersRepository.GetUsers().ToList(),
                TrainDrivers = _trainDriversRepository.GetTrainDriversForStatic().ToList()
            };

            return View(statistics);
        }

        [Authorize(Roles = "TrainDriver")]
        public async Task<IActionResult> TrainDriver(bool isModalWindowShowRequired = false)
        {
            string Id = _userManager.GetUserId(User);
            TrainDriver trainDriver = await _trainDriversRepository.GetByUserId(Id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            StatisticssViewModel statistics = new StatisticssViewModel()
            {
                Id = trainDriver.User.Id,
                FirstName = trainDriver.User.FirstName,
                LastName = trainDriver.User.LastName,
                MiddleName = trainDriver.User.MiddleName,
                Email = trainDriver.User.Email,
                HealthStatus = trainDriver.HealthStatus,
                CertificateNumber = trainDriver.CertificateNumber,
                Routes = _routesRepository.GetRoutes().ToList(),
                Users = _usersRepository.GetUsers().ToList()
            };

            return View(statistics);
        }

        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> Passenger(bool isModalWindowShowRequired = false)
        {
            string id = _userManager.GetUserId(User);
            User passenger = await _usersRepository.GetUserById(id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            StatisticssViewModel statistics = new StatisticssViewModel()
            {
                Id = passenger.Id,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                MiddleName = passenger.MiddleName,
                Email = passenger.Email,
                Passport = passenger.Passport,
                Routes = _routesRepository.GetRoutes().ToList(),
                Users = _usersRepository.GetUsers().ToList()
            };

            return View(statistics);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Admin(bool isModalWindowShowRequired = false)
        {
            string id = _userManager.GetUserId(User);
            User admin = await _usersRepository.GetUserById(id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            StatisticssViewModel statistics = new StatisticssViewModel()
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                MiddleName = admin.MiddleName,
                Email = admin.Email,
                Routes = _routesRepository.GetRoutes().ToList(),
                Users = _usersRepository.GetUsers().ToList(),
                TrainDrivers = _trainDriversRepository.GetTrainDriversForStatic().ToList(),
                Dispatchers = _dispatchersRepository.GetDispatcherForStatic().ToList()
            };

            return View(statistics);
        }

    }
}
