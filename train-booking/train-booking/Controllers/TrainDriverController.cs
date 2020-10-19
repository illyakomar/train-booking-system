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
using train_booking.ViewModels.Account;
using train_booking.ViewModels.TrainDrivers;

namespace train_booking.Controllers
{
    public class TrainDriverController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly TrainBookingContext _context;

        public TrainDriverController(IUsersRepository usersRepository, ITrainDriversRepository trainDriversRepository, TrainBookingContext context)
        {
            _usersRepository = usersRepository;
            _trainDriversRepository = trainDriversRepository;
            _context = context;
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
        public async Task<IActionResult> Create(TrainDriverFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _usersRepository.RegisterTrainDriver(
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
                          new TrainDriverViewModel
                          {
                              BirthDate = model.BirthDate,
                              HealthStatus = model.HealthStatus,
                              CertificateNumber = model.CertificateNumber
                          }
                      );

                }
                catch
                {
                    ModelState.AddModelError("Email", "Ця електронна адреса вже зайнята!");
                    return View(model);
                }


                return RedirectToAction("Index", "TrainDriver", new { message = "Машиніст успішно доданий!" });
            }

            return View(model);
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
                Passport = trainDriver.User.PhoneNumber,
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
                var resU = await _usersRepository.Update(new UserViewModel
                {
                    Email = model.Email,
                    Passport = model.Passport,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName
                });
                var resS = await _trainDriversRepository.Update(TrainDriverId, model);

                if (resU && resS)
                {
                    if (User.IsInRole("TrainDriver"))
                    {
                        return RedirectToAction("TrainDriver", "Profile");
                    }

                    return RedirectToAction("Index", "TrainDriver", new { message = "Машиніст успішно відредагований!" });
                }
                else if (!resU && !resS)
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

        //[HttpGet("/Teachers/GetTrainDriverRoutes/channelId={channelId}")]
        //public async Task<IActionResult> GetTrainDriverRoutes(string channelId)
        //{
        //    try
        //    {
        //        var userId = _userRepository.GetAuthorizedUserId(channelId);
        //        var teacher = await _userRepository.GetById(userId);
        //        var courses = await _coursesRepository.GetTeacherCourses(teacher.Id);

        //        // Remove self-loop
        //        for (int i = 0; i < courses.Count; i++)
        //        {
        //            for (int j = 0; j < courses[i].CourseModule.Count; j++)
        //            {
        //                courses[i].CourseModule.ToList()[j].Course = null;
        //                courses[i].CourseModule.ToList()[j].Module.CourseModule = null;
        //            }
        //            courses[i].CourseStudent = null;
        //        }

        //        return Json(courses);
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound("No courses have been found");
        //    }
        //}
    }
}
