using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Bus Stop Model.
    /// </summary>
    public class StopModel
    {
        public int StopId { get; set; }

        public string StopName { get; set; }

        public decimal StopLatitude { get; set; }

        public decimal StopLongitude { get; set; }
    }
}