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
        /// <summary>
        /// The PTV Run Id.
        /// </summary>
        public int RunId { get; set; }

        /// <summary>
        /// The route associated with this run.
        /// </summary>
        public RouteModel Route { get; set; }

        /// <summary>
        /// The final bus stop associated with this run.
        /// </summary>
        public StopModel FinalStop { get; set; }

        /// <summary>
        /// The direction associated with this run.
        /// </summary>
        public DirectionModel Direction { get; set; }

        /// <summary>
        /// The stopping pattern associated with this run.
        /// </summary>
        public StoppingPatternModel StoppingPattern { get; set; }
    }
}