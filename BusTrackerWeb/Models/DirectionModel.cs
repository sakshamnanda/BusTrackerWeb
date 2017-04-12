using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Direction Model.
    /// </summary>
    public class DirectionModel
    {
        public int DirectionId { get; set; }

        public string DirectionName { get; set; }

        public RouteModel Route { get; set; }

        public int RouteType { get; set; }
    }
}