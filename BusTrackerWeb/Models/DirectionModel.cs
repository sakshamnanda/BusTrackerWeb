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
        /// <summary>
        /// The PTV Direction Id.
        /// </summary>
        public int DirectionId { get; set; }

        /// <summary>
        /// The Bus's direction of travel i.e. the last stop.
        /// </summary>
        public string DirectionName { get; set; }
    }
}