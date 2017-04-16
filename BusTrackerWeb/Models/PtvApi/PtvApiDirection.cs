using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Direction Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDirection
    {
        public int direction_id { get; set; }

        public string direction_name { get; set; }

        public int route_id { get; set; }

        public int route_type { get; set; }
    }
}