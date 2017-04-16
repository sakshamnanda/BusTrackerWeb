using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Departure Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDeparture
    {
        public int stop_id { get; set; }

        public int route_id { get; set; }

        public int run_id { get; set; }

        public int direction_id { get; set; }

        public List<int> disruption_ids { get; set; }

        public string scheduled_departure_utc { get; set; }

        public string estimated_departure_utc { get; set; }

        public bool at_platform { get; set; }

        public string platform_number { get; set; }

        public string flags { get; set; }
    }
}