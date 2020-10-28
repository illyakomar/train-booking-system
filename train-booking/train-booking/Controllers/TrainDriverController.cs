using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Account;
using train_booking.ViewModels.TrainDrivers;

namespace train_booking.Controllers
{
    public class TrainDriverController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly TrainBookingContext _context;

        public TrainDriverController
        (
            IUsersRepository usersRepository,
            ITrainDriversRepository trainDriversRepository,
            TrainBookingContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _usersRepository = usersRepository;
            _trainDriversRepository = trainDriversRepository;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index(string error = null, string message = null)
        {
            var trainDrivers = _context.TrainDriver.Include(x => x.User).ToList();

            ViewBag.Error = error;
            ViewBag.Message = message;

            return View(trainDrivers);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(TrainDriverFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _usersRepository.IsEmailUnique(viewModel.Email))
                {
                    User user = new User
                    {
                        Email = viewModel.Email,
                        UserName = viewModel.Email,
                        LastName = viewModel.LastName,
                        FirstName = viewModel.FirstName,
                        MiddleName = viewModel.MiddleName,
                        Passport = viewModel.Passport
                    };

                    var createUserResult = await _userManager.CreateAsync(user, viewModel.Password);

                    if (createUserResult.Succeeded)
                    {
                        TrainDriverViewModel trainDriverViewModel = new TrainDriverViewModel
                        {
                            BirthDate = viewModel.BirthDate,
                            CertificateNumber = viewModel.CertificateNumber,
                            HealthStatus = viewModel.HealthStatus,
                            UserId = user.Id
                        };

                        await _userManager.AddToRoleAsync(user, "TrainDriver");
                        await _usersRepository.RegisterTrainDriver(trainDriverViewModel);

                        if (!string.IsNullOrWhiteSpace(user.Email))
                        {
                            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in createUserResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ця пошта вже зареєстрована");
                }
            }

            return RedirectToAction("Create", "TrainDriver", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            var trainDriver = await _trainDriversRepository.GetByUserId(id);

            if (trainDriver == null)
            {
                return RedirectToAction("Index", "TrainDriver", new { error = "Машиніста не знайдено!" });
            }

            TrainDriverFormViewModel model = new TrainDriverFormViewModel
            {
                Id = trainDriver.User.Id,
                TrainDriverId = trainDriver.TrainDriverId,
                LastName = trainDriver.User.LastName,
                FirstName = trainDriver.User.FirstName,
                MiddleName = trainDriver.User.MiddleName,
                Passport = trainDriver.User.Passport,
                BirthDate = trainDriver.BirthDate,
                HealthStatus = trainDriver.HealthStatus,
                CertificateNumber = trainDriver.CertificateNumber,
                Email = trainDriver.User.Email
            };


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, TrainDriver")]
        public async Task<IActionResult> Edit(TrainDriverFormViewModel model, int TrainDriverId = -1)
        {
            if (ModelState.IsValid || ModelState.ErrorCount == 2)
            {
                var trainU = await _usersRepository.Update(new UserViewModel
                {
                    Email = model.Email,
                    Passport = model.Passport,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                });
                var trainS = await _trainDriversRepository.Update(TrainDriverId, model);

                if (trainU && trainS)
                {
                    if (User.IsInRole("TrainDriver"))
                    {
                        return RedirectToAction("TrainDriver", "Profile");
                    }

                    return RedirectToAction("Index", "TrainDriver", new { message = "Машиніст успішно відредагований!" });
                }
                else if (!trainU && !trainS)
                {
                    return RedirectToAction("Index", "TrainDriver", new { error = "Сталася невідома помилка при редагуванні машиніста!" });
                }
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var res = await _trainDriversRepository.Remove(id);

                if (res)
                {
                    return RedirectToAction("Index", "TrainDriver", new { message = "Машиніст успішно видалений!" });
                }

                return RedirectToAction("Index", "TrainDriver", new { error = "Сталася невідома помилка при видаленні машиніста!" });
            }
            catch
            {
                return RedirectToAction("Index", "TrainDriver", new { error = "Машиніст задіяний на маршруті, спершу видаліть машиніста з маршруту!" });
            }
        }

        [HttpGet]
        [Authorize(Roles = "TrainDriver")]
        public async Task<IActionResult> Setting(string id)
        {
            try
            {
                var trainDriver = await _trainDriversRepository.GetByUserId(id);
                if (trainDriver == null)
                {
                    return RedirectToAction("Index", "TrainDriver", new { error = "Машиніста не знайдено!" });
                }
                TrainDriverFormViewModel model = new TrainDriverFormViewModel
                {
                    LastName = trainDriver.User.LastName,
                    FirstName = trainDriver.User.FirstName,
                    MiddleName = trainDriver.User.MiddleName,
                    Passport = trainDriver.User.Passport,
                    BirthDate = trainDriver.BirthDate,
                    CertificateNumber = trainDriver.CertificateNumber,
                    HealthStatus = trainDriver.HealthStatus,
                    Email = trainDriver.User.Email,
                    TrainDriverId = trainDriver.TrainDriverId
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Profile", new { error = "Машиніста не знайдено!" });
            }
        }

    }
}
