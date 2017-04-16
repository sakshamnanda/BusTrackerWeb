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
    public class PtvApiStopOnRouteResponse
    {
        public List<PtvApiStopOnRoute> Stops { get; set; }

        public PtvApiStatus Status { get; set; }
    }
}