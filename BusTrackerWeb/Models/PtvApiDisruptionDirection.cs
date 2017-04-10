using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Disruption Direction Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDisruptionDirection
    {
        public int route_direction_id { get; set; }

        public int direction_id { get; set; }

        public string direction_name { get; set; }

        public string service_time { get; set; }
    }
}