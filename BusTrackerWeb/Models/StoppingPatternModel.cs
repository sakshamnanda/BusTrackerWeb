using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Stopping Pattern Model, Refer:
    /// </summary>
    public class StoppingPatternModel
    {
        public List<DepartureModel> Departures { get; set; }

        public List<DisruptionModel> Disruptions { get; set; }
    }
}