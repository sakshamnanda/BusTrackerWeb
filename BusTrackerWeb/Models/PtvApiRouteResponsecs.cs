using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    public class PtvApiRouteResponse
    {
        public List<PtvApiRoute> Routes { get; set; }

        public PtvApiStatus Status { get ; set; }
    }
}