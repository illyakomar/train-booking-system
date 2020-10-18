using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using train_booking.Models;

namespace train_booking.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("CheckerRole", "Home");
            }

            return View();
        }

        public IActionResult CheckerRole()
        {
            if (User.IsInRole("Passenger"))
            {
                return RedirectToAction("Passenger", "Profile");
            }

            if (User.IsInRole("Dispatcher"))
            {
                return RedirectToAction("Dispatcher", "Profile");
            }

            if (User.IsInRole("TrainDriver"))
            {
                return RedirectToAction("TrainDriver", "Profile");
            }

            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Admin", "Profile");
            }

            return RedirectToAction("Passenger", "Profile");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
