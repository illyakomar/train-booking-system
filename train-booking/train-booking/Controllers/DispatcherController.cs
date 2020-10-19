using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using train_booking.Data;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Dispatchers;
using train_booking.ViewModels.Account;
using train_booking.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace train_booking.Controllers
{
    public class DispatcherController : Controller
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IDispatchersRepository _dispatchersRepository;
        private readonly TrainBookingContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DispatcherController
        (
            IUsersRepository usersRepository,
            TrainBookingContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IDispatchersRepository dispatchersRepository
        )
        {
            _usersRepository = usersRepository;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _dispatchersRepository = dispatchersRepository;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index(string error = null, string message = null)
        {
            var dispatchers = _context.Dispatcher.Include(x => x.User).ToList();

            ViewBag.Error = error;
            ViewBag.Message = message;

            return View(dispatchers);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(DispatcherFormViewModel viewModel)
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
                        DispatcherViewModel trainDriverViewModel = new DispatcherViewModel
                        {
                            UserId = user.Id,
                            Address = viewModel.Address,
                            BirthDate = viewModel.BirthDate
                        };

                        await _userManager.AddToRoleAsync(user, "Dispatcher");
                        await _usersRepository.RegisterDispatcher(trainDriverViewModel);

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

            return RedirectToAction("Create", "Dispatcher", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            var dispatcher = await _dispatchersRepository.GetByUserId(id);

            if (dispatcher == null)
            {
                return RedirectToAction("Index", "Dispatcher", new { error = "Диспетчера не знайдено!" });
            }

            DispatcherFormViewModel model = new DispatcherFormViewModel
            {
                Id = dispatcher.User.Id,
                DispatcherId = dispatcher.DispatcherId,
                LastName = dispatcher.User.LastName,
                FirstName = dispatcher.User.FirstName,
                MiddleName = dispatcher.User.MiddleName,
                Passport = dispatcher.User.Passport,
                BirthDate = dispatcher.BirthDate,
                Address = dispatcher.Address,
                Email = dispatcher.User.Email
            };


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Dispatcher")]
        public async Task<IActionResult> Edit(DispatcherFormViewModel model)
        {
            if (ModelState.ErrorCount == 2)
            {
                var DispatcherU = await _usersRepository.Update(new UserViewModel
                {
                    Email = model.Email,
                    Passport = model.Passport,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                });
                var DispatchS = await _dispatchersRepository.Update(model.DispatcherId, model);

                if (DispatcherU && DispatchS)
                {
                    if (User.IsInRole("Dispatcher"))
                    {
                        return RedirectToAction("Dispatcher", "Profile");
                    }

                    return RedirectToAction("Index", "Dispatcher", new { message = "Диспетчер успішно відредагований!" });
                }
                else if (!DispatcherU && !DispatchS)
                {
                    return RedirectToAction("Index", "Dispatcher", new { error = "Сталася невідома помилка при редагуванні диспетчера!" });
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
                var res = await _dispatchersRepository.Remove(id);

                if (res)
                {
                    return RedirectToAction("Index", "Dispatcher", new { message = "Диспетчер успішно видалений!" });
                }

                return RedirectToAction("Index", "Dispatcher", new { error = "Сталася невідома помилка при видаленні диспетчера!" });
            }
            catch
            {
                return RedirectToAction("Index", "Dispatcher", new { error = "Диспетчер задіяний на маршруті, спершу видаліть диспетчера з маршруту!" });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> Setting(string id)
        {
            try
            {
                var dispatcher = await _dispatchersRepository.GetByUserId(id);
                if (dispatcher == null)
                {
                    return RedirectToAction("Index", "Dispatcher", new { error = "Диспетчера не знайдено!" });
                }
                DispatcherFormViewModel model = new DispatcherFormViewModel
                {
                    LastName = dispatcher.User.LastName,
                    FirstName = dispatcher.User.FirstName,
                    MiddleName = dispatcher.User.MiddleName,
                    Passport = dispatcher.User.Passport,
                    BirthDate = dispatcher.BirthDate,
                    Address = dispatcher.Address,
                    Email = dispatcher.User.Email,
                    DispatcherId = dispatcher.DispatcherId
                };

                return View(model);
            }
            catch
            {
                return RedirectToAction("Index", "Profile", new { error = "Диспетчера не знайдено!" });
            }
        }
    }
}
