using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    public class RunDeparture
    {
        public RunModel Run { get; set; }

        public StopModel Stop { get; set; }

        public DateTime RunScheduledDeparture { get; set; }
    }
}