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
        /// <summary>
        /// The PTV route Id.
        /// </summary>
        public int RouteId { get; set; }

        /// <summary>
        /// The name of this route.
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// The public reference number of this route.
        /// </summary>
        public string RouteNumber { get; set; }

        /// <summary>
        /// The PTV route type i.e. all bus services = 2.
        /// </summary>
        public int RouteType { get; set; }
    }
}