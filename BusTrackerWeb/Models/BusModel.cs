using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace BusTrackerWeb.Models
{
    public class BusModel
    {
        public int BusId { get; set; }

        public string BusRegoNumber { get; set; }

        public decimal BusLongitude { get; set; }

        public decimal BusLatitude { get; set; }

        public int BusPreviousStopId { get; set; }
    }
}