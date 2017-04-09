using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTrackerWeb.Controllers
{
    /// <summary>
    /// Controls all Bus Tracker Home View actions.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Open the Home View.
        /// </summary>
        /// <returns>Index View.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
