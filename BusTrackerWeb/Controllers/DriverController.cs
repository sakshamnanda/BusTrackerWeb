using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTrackerWeb.Controllers
{
    /// <summary>
    /// Controls all Bus Tracker Driver View actions.
    /// </summary>
    public class DriverController : Controller
    {
        /// <summary>
        /// Open the Passenger Landing View.
        /// </summary>
        /// <returns>Passenger View.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Driver Landing Page";

            return View();
        }
    }
}
