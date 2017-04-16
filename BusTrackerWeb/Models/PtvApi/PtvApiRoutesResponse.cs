using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Routes Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiRoutesResponse
    {
        public List<PtvApiRoute> Routes { get; set; }

        public PtvApiStatus Status { get ; set; }

        public int route_type { get; set; }
    }
}