using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace train_booking.Controllers
{
    public class TrainDriverController : Controller
    {
        // GET: TrainDriverController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrainDriverController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainDriverController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainDriverController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainDriverController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainDriverController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainDriverController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainDriverController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
