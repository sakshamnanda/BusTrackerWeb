using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Status Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiStatus
    {
        public string Version { get; set; }

        public int Health { get; set; }
    }
}