using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Disruption Route Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDisruptionRoute
    {
        public int route_type { get; set; }

        public int route_id { get; set; }

        public string route_name { get; set; }

        public string route_number { get; set; }

        public PtvApiDisruptionDirection direction { get; set; }
    }
}