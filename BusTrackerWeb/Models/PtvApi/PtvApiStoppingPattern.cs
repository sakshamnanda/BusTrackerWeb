using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Stopping Pattern Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiStoppingPattern
    {
        public List<PtvApiDeparture> Departures { get; set; }

        public List<PtvApiDisruption> Disruptions { get; set; }

        public PtvApiStatus Status { get; set; }
    }
}