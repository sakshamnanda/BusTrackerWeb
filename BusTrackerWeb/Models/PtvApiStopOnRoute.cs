using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Stop on Route Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiStopOnRoute
    {
        public string stop_name { get; set; }

        public int stop_id { get; set; }

        public int route_type { get; set; }

        public decimal stop_latitude { get; set; }

        public decimal stop_longitude { get; set; }
    }
}