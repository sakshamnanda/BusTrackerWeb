using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Runs Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiRunResponse
    {
        public List<PtvApiRun> Runs { get; set; }

        public PtvApiStatus Status { get ; set; }
    }
}