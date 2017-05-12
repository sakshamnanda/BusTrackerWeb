using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Stops by Distance Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiStopsByDistanceResponse
    {
        public List<PtvApiStopGeosearch> Stops { get; set; }

        public PtvApiStatus Status { get; set; }
    }
}