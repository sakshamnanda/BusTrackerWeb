using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Departure Model.
    /// </summary>
    public class DepartureModel
    {
        public StopModel Stop { get; set; }

        public RouteModel Route { get; set; }

        public int RunId { get; set; }

        public DirectionModel Direction { get; set; }

        public List<int> Disruptions { get; set; }

        public DateTime ScheduledDeparture { get; set; }

        public DateTime EstimatedDeparture { get; set; }

        public bool AtPlatform { get; set; }

        public string PlatformNumber { get; set; }

        public string Flags { get; set; }
    }
}