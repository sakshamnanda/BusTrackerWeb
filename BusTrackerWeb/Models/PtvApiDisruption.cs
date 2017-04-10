using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Disruption Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDisruption
    {
        public int disruption_id { get; set; }

        public string title { get; set; }

        public string url { get; set; }

        public string description { get; set; }

        public string disruption_status { get; set; }

        public string disruption_type { get; set; }

        public string published_on { get; set; }

        public string last_updated { get; set; }

        public string from_date { get; set; }

        public string to_date { get; set; }

        public List<PtvApiDisruptionRoute> routes { get; set; }

    }
}