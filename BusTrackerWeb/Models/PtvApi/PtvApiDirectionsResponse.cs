using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Directions Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDirectionsResponse
    {
        public List<PtvApiDirection> Directions { get; set; }

        public PtvApiStatus Status { get ; set; }
    }
}