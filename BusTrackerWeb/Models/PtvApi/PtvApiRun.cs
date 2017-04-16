using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Run Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiRun
    {
        public int run_id { get; set; }

        public int route_id { get; set; }

        public int route_type { get; set; }

        public int final_stop_id { get; set; }

        public string destination_name { get; set; }

        public string status { get; set; } 
    }
}