using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    public class PtvApiRoute
    {
        public int route_type { get; set; }

        public int route_id { get; set; }

        public string route_name { get; set; }

        public string route_number { get; set; }
    }
}