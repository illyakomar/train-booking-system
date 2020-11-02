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
using train_booking.ViewModels.Passengers;

namespace train_booking.Controllers
{
    public class AdminController : Controller
    {
   
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPassengersRepository _passengersRepository;


        public AdminController
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
        [Authorize(Roles = "Administrator")]
        [Route("{controller}/{action}/{id}")]
        public async Task<IActionResult> Setting(string id)
        {
            User admin = await _passengersRepository.GetById(id);

            AdminFormViewModel model = new AdminFormViewModel
            {
                Id = admin.Id,
                LastName = admin.LastName,
                FirstName = admin.FirstName,
                MiddleName = admin.MiddleName,
                Email = admin.Email
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Setting(AdminFormViewModel model)
        {
            var res = await _passengersRepository.UpdateAdmin(model);
            if (res && (ModelState.IsValid || ModelState.ErrorCount == 1))
            {
                return RedirectToAction("Admin", "Profile");
            }
            else
            {
                return RedirectToAction("Admin", "Profile", new { error = "Сталася невідома помилка при редагуванні викладача!" });
            }
        }
    }
}
