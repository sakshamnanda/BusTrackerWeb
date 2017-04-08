using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusTrackerWeb.Controllers
{
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
            routes.Add(new Models.RouteModel { RouteId = 1, RouteName = "Geelong Station to Deakin", RouteNumber = 41 });
            routes.Add(new Models.RouteModel { RouteId = 2, RouteName = "Deakin to Geelong Station", RouteNumber = 42 });

            return routes;
        }
    }
}