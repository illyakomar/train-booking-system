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

namespace train_booking.Controllers
{
   [Authorize(Roles = "Administrator, Dispatcher, TrainDriver, Passenger")]
    public class ProfileController : Controller
    {
        private readonly IDispatchersRepository _dispatchersRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly TrainBookingContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileController
        (
            IUsersRepository usersRepository,
            IDispatchersRepository dispatchersRepository,
            ITrainDriversRepository trainDriversRepository,
            TrainBookingContext context,
            UserManager<User> userManager
        )
        {
            _usersRepository = usersRepository;
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

            return View(dispatcher);
        }

        [Authorize(Roles = "TrainDriver")]
        public async Task<IActionResult> TrainDriver(bool isModalWindowShowRequired = false)
        {
            string Id = _userManager.GetUserId(User);
            TrainDriver trainDriver = await _trainDriversRepository.GetByUserId(Id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            return View(trainDriver);
        }

        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> Passenger(bool isModalWindowShowRequired = false)
        {
            string id = _userManager.GetUserId(User);
            User passenger = await _usersRepository.GetUserById(id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            return View(passenger);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Admin(bool isModalWindowShowRequired = false)
        {
            string id = _userManager.GetUserId(User);
            User admin = await _usersRepository.GetUserById(id);

            ViewBag.IsModalWindowShowRequired = isModalWindowShowRequired;

            return View(admin);
        }

    }
}
