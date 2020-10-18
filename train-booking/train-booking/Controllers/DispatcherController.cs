using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace train_booking.Controllers
{
    public class DispatcherController : Controller
    {
        // GET: DispatcherController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DispatcherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DispatcherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DispatcherController/Create
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

        // GET: DispatcherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DispatcherController/Edit/5
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

        // GET: DispatcherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DispatcherController/Delete/5
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
