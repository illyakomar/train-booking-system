using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using train_booking.Data;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Trains;
using train_booking.ViewModels.Wagons;

namespace train_booking.Controllers
{
    public class StorageTrainController : Controller
    {
        private readonly ITrainsRepository _trainRepository;
        private readonly IWagonsRepository _wagonRepository;
        private readonly TrainBookingContext _context;

        public StorageTrainController(
            ITrainsRepository trainRepository,
            IWagonsRepository wagonRepository,
            TrainBookingContext context
        )
        {
            _trainRepository = trainRepository;
            _wagonRepository = wagonRepository;
            _context = context;
        }
        [Authorize(Roles = "Administrator,Dispatcher")]
        public IActionResult Index(string error = null)
        {
            ViewBag.Error = error;
            return View(_trainRepository.GetTrains());
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Dispatcher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        public async Task<IActionResult> Create(TrainViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _trainRepository.Insert(model);
                return RedirectToAction("Index", "StorageTrain");
            }
            return View(model);
        }
        [Authorize(Roles = "Administrator,Dispatcher")]
        public IActionResult Edit(int id)
        {
            TrainViewModel trainViewModel = _trainRepository.GetById(id);
            return View("Edit", _trainRepository.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        public async Task<IActionResult> Edit(TrainViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _trainRepository.Update(model);
                return RedirectToAction("Index", "StorageTrain");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _trainRepository.Delete(id);
                return RedirectToAction("Index", "StorageTrain");
            }
            catch
            {
                return RedirectToAction("Index", "StorageTrain", new { error = "Даний потяг уже пов'язаний із маршрутом, спершу видаліть маршрут!" });
            }

        }

        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{trainId}/Wagon")]
        public IActionResult Wagon(int trainId, string error)
        {
            ViewBag.Error = error;
            ViewBag.TrainId = trainId;
            List<WagonViewModel> wagons = _wagonRepository.GetWagonByTrainId(trainId).ToList();
            return View(wagons);
        }


        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{trainId}")]
        public IActionResult CreateWagon(int trainId)
        {
            TrainViewModel train = _trainRepository.GetById(trainId);

            if (train != null)
            {
                ViewBag.TrainId = trainId;
                return View();
            }

            return RedirectToAction("Index", "StorageTrain", new { error = "Потяг не знайдено" });
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{trainId}")]
        public async Task<IActionResult> CreateWagon(WagonViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _wagonRepository.Insert(model);
                return RedirectToAction("Wagon", "StorageTrain", new { trainId = model.TrainId });
            }
            return View(model);
        }

        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{wagonId}")]
        public IActionResult EditWagon(int wagonId)
        {
            WagonViewModel wagon = _wagonRepository.GetById(wagonId);
            return View(wagon);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{wagonId}")]
        public async Task<IActionResult> EditWagon(WagonViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _wagonRepository.Update(model);
                return RedirectToAction("Wagon", "StorageTrain", new { trainId = model.Train.TrainId });
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Dispatcher")]
        [Route("{controller}/{action}/{wagonId}")]
        public async Task<IActionResult> DeleteWagon(int wagonId, int trainId)
        {
            try
            {
                await _wagonRepository.Delete(wagonId);
                return RedirectToAction("Wagon", "StorageTrain", new { trainId });
            }
            catch
            {
                return RedirectToAction("Wagon", "StorageTrain", new { trainId, error = "Ви не можете видалити цей вагон, оскільки він задіяний у маршруті!" });
            }
        }
    }
}
