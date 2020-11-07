using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using train_booking.Data;
using train_booking.Models;
using train_booking.Services.Interfaces;
using train_booking.ViewModels.Routes;
using train_booking.ViewModels.Tickets;

namespace train_booking.Controllers
{
    public class TicketController : Controller
    {
        private readonly IRoutesRepository _routesRepository;
        private readonly ITrainsRepository _trainsRepository;
        private readonly IWagonsRepository _wagonsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITrainDriversRepository _trainDriversRepository;
        private readonly UserManager<User> _userManager;
        private readonly TrainBookingContext _context;

        public TicketController(
            IRoutesRepository routesRepository,
            ITrainsRepository trainsRepository,
            IWagonsRepository wagonsRepository,
            IUsersRepository usersRepository,
            ITrainDriversRepository trainDriversRepository,
            UserManager<User> userManager,
            TrainBookingContext context
        )
        {
            _routesRepository = routesRepository;
            _trainsRepository = trainsRepository;
            _wagonsRepository = wagonsRepository;
            _usersRepository = usersRepository;
            _trainDriversRepository = trainDriversRepository;
            _userManager = userManager;
            _context = context;
        }


        [Route("{controller}")]
        [Authorize(Roles = "Passenger")]
        public IActionResult Index(string message = null, string error = null)
        {
            ViewBag.Message = message;
            ViewBag.Error = error;

            RoutesIndexViewModel routesIndexViewModel = new RoutesIndexViewModel()
            {
                Routes = _routesRepository.GetRoutes().ToList()
            };
            return View(routesIndexViewModel);
        }

        [Authorize(Roles = "Passenger")]
        [Route("{controller}/{action}/{trainId}")]
        public async Task<IActionResult> Details(int trainId)
        {
            Route route = await _routesRepository.GetRouteByTrainId(trainId);
            return View(route);
        }

        [HttpGet]
        [Authorize(Roles = "Passenger")]
        [Route("{controller}/{action}/{wagonId}")]
        public async Task<IActionResult> Reservation(int wagonId)
        {
            Wagon wagon = await _wagonsRepository.GetById(wagonId);
            List<Seat> seats = wagon.Seats.Where(seat => seat.SeatAvailability).ToList();
            return View(seats);
        }

        [HttpGet]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> ReservationConfirmation(int id)
        {
            try
            {
                Seat seat = await _context.Seat
                    .Where(seat => seat.Id == id && seat.SeatAvailability)
                    .Include(seat => seat.Wagon)
                    .ThenInclude(wagon => wagon.Train.Route)
                    .FirstOrDefaultAsync();
                User passenger = await _userManager.GetUserAsync(User);
                ReservationConfirmationViewModel reservationViewModel = new ReservationConfirmationViewModel
                {
                    UserId = passenger.Id,
                    FirstName = passenger.FirstName,
                    MiddleName = passenger.MiddleName,
                    LastName = passenger.LastName,
                    Passport = passenger.Passport,
                    SeatId = seat.Id,
                    DeparturePoint = seat.Wagon.Train.Route.DeparturePoint,
                    Destination = seat.Wagon.Train.Route.Destination,
                    DeparturePointDate = seat.Wagon.Train.Route.DeparturePointDate,
                    DestinationDate = seat.Wagon.Train.Route.DestinationDate,
                    WagonType = seat.Wagon.TypeWagon,
                    WagonId = seat.Wagon.WagonId,
                    SeatNumber = seat.SeatNumber,
                    PlacePrice = seat.Wagon.PlacePrice
                };
                return View(reservationViewModel);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> ReservationConfirmation(ReservationConfirmationViewModel reservationConfirmationViewModel)
        {
            try
            {
                Seat seat = await _context.Seat
                    .Where(seat => seat.Id == reservationConfirmationViewModel.SeatId && seat.SeatAvailability)
                    .FirstOrDefaultAsync();

                seat.UserId = reservationConfirmationViewModel.UserId;
                seat.SeatAvailability = false;

                _context.Seat.Update(seat);
                await _context.SaveChangesAsync();
                return RedirectToAction("History");
            }
            catch
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Authorize(Roles = "Passenger")]
        public async Task<IActionResult> History()
        {
            string userId = (await _userManager.GetUserAsync(User)).Id;
            List<Seat> seats = await _context.Seat
                .Where(seat => seat.UserId == userId)
                .Include(seat => seat.Wagon)
                .ThenInclude(wagon => wagon.Train.Route)
                .ToListAsync();
            return View(seats);
        }

    }
}
