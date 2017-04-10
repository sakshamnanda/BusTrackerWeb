using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusTrackerWeb.Controllers
{
    /// <summary>
    /// Controls all Bus Tracker API Route action requests.
    /// </summary>
    public class RouteController : ApiController
    {
        /// <summary>
        /// Get an array of bus routes.
        /// </summary>
        /// <returns>An array of bus route objects.</returns>
        [ActionName("GetRoutes")]
        public List<RouteModel> GetRoutes()
        {
            List<RouteModel> routes = new List<RouteModel>();

            // Get all routes from the PTV API.

            // Convert PTV schema objects Bus Tracker schema objects.



            return routes;
        }
    }
}