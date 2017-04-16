using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Stopping Pattern Model.
    /// </summary>
    public class StoppingPatternModel
    {
        /// <summary>
        /// The departures associated with this stopping pattern.
        /// </summary>
        public List<DepartureModel> Departures { get; set; }

        /// <summary>
        /// The disruptions associated with this stopping pattern.
        /// </summary>
        public List<DisruptionModel> Disruptions { get; set; }
    }
}