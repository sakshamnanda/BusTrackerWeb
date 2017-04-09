using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    public class PtvApiStatus
    {
        public string Version { get; set; }

        public int Health { get; set; }
    }
}