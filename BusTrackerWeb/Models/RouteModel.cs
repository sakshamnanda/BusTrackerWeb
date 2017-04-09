using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Route Model.
    /// </summary>
    public class RouteModel
    {
        public int RouteId { get; set; }

        public string RouteName { get; set; }

        public int RouteNumber { get; set; }
    }
}