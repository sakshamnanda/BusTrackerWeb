using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Disruption Model, Refer:
    /// </summary>
    public class DisruptionModel
    {
        public int DisruptionId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string DisruptionStatus { get; set; }

        public string DisruptionType { get; set; }

        public string PublishedOn { get; set; }

        public string LastUpdated { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }
    }
}