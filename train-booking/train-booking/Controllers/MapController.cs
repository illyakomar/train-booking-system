using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace train_booking.Controllers
{
    public class MapController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
