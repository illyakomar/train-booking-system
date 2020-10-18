using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace train_booking.Controllers
{
    public class TrainController : Controller
    {
        // GET: TrainController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrainController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainController/Create
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

        // GET: TrainController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainController/Edit/5
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

        // GET: TrainController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainController/Delete/5
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
