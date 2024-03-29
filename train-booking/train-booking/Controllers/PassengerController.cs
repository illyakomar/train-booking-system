﻿using System;
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
using train_booking.ViewModels.Passengers;

namespace train_booking.Controllers
{
    public class PassengerController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPassengersRepository _passengersRepository;


        public PassengerController
        (
            IPassengersRepository passengersRepository,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _passengersRepository = passengersRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Passenger")]
        [Route("{controller}/{action}/{id}")]
        public async Task<IActionResult> Setting(string id)
        {
            User passenger = await _passengersRepository.GetById(id);

            PassengerFormViewModel model = new PassengerFormViewModel
            {
                Id = passenger.Id,
                LastName = passenger.LastName,
                FirstName = passenger.FirstName,
                MiddleName = passenger.MiddleName,
                Passport = passenger.Passport,
                Email = passenger.Email
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> Setting(PassengerFormViewModel model)
        {
            var res = await _passengersRepository.Update(model);
            if (res && (ModelState.IsValid || ModelState.ErrorCount == 1))
            {
                return RedirectToAction("Passenger", "Profile");
            }
            else
            {
                return RedirectToAction("Passenger", "Profile", new { error = "Сталася невідома помилка при редагуванні викладача!" });
            }
        }

    }
}
