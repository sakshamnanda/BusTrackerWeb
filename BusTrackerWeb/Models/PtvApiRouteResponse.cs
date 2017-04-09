using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Route Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiRouteResponse
    {
        public List<PtvApiRoute> Routes { get; set; }

        public PtvApiStatus Status { get ; set; }
    }
}