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

namespace train_booking.Controllers
{
    public class DispatcherController : Controller
    {

        private readonly IUsersRepository _usersRepository;
        private readonly IDispatchersRepository _dispatchersRepository;
        private readonly TrainBookingContext _context;

        public DispatcherController(IUsersRepository usersRepository, IDispatchersRepository dispatchersRepository, TrainBookingContext context)
        {
            _usersRepository = usersRepository;
            _dispatchersRepository = dispatchersRepository;
            _context = context;
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
        public async Task<IActionResult> Create(DispatcherFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _usersRepository.RegisterDispatcher(
                          new UserViewModel
                          {
                              Email = model.Email,
                              FirstName = model.FirstName,
                              LastName = model.LastName,
                              MiddleName = model.MiddleName,
                              Password = model.Password,
                              Passport = model.Passport,
                              UserName = model.Email
                          },
                          new DispatcherViewModel
                          {
                              BirthDate = model.BirthDate,
                              Address = model.Address
                          }
                      );

                }
                catch
                {
                    ModelState.AddModelError("Email", "Ця електронна адреса вже зайнята!");
                    return View(model);
                }


                return RedirectToAction("Index", "Dispatcher", new { message = "Диспетчер успішно доданий!" });
            }

            return View(model);
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
                Passport = dispatcher.User.PhoneNumber,
                BirthDate = dispatcher.BirthDate,
                Address = dispatcher.Address,
                Email = dispatcher.User.Email
            };


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Dispatcher")]
        public async Task<IActionResult> Edit(DispatcherFormViewModel model, int DispatcherId = -1)
        {
            if (ModelState.IsValid || ModelState.ErrorCount == 2)
            {
                var resU = await _usersRepository.Update(new UserViewModel
                {
                    Email = model.Email,
                    Passport = model.Passport,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                });
                var resS = await _dispatchersRepository.Update(DispatcherId, model);

                if (resU && resS)
                {
                    if (User.IsInRole("Dispatcher"))
                    {
                        return RedirectToAction("Dispatcher", "Profile");
                    }

                    return RedirectToAction("Index", "Dispatcher", new { message = "Диспетчер успішно відредагований!" });
                }
                else if (!resU && !resS)
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
