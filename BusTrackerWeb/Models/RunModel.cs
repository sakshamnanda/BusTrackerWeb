using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Run Model
    /// </summary>
    public class RunModel
    {
        public int RunId { get; set; }

        public RouteModel Route { get; set; }

        public StopModel FinalStop { get; set; }

        public StoppingPatternModel StoppingPattern { get; set; }
    }
}