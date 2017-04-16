using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Departures Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDeparturesResponse
    {
        public List<PtvApiDeparture> Departures { get; set; }

        public PtvApiStatus Status { get ; set; }
    }
}