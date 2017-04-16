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
        /// <summary>
        /// The PTV Disruption Id.
        /// </summary>
        public int DisruptionId { get; set; }

        /// <summary>
        /// The PTV Title for this disruption.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The PTV link for more detail on this disruption.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// PTV Details of the disruptions.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// PTV status of the disruption.
        /// </summary>
        public string DisruptionStatus { get; set; }

        /// <summary>
        /// The PTV disruption type category.
        /// </summary>
        public string DisruptionType { get; set; }

        /// <summary>
        /// The disruption publication time.
        /// </summary>
        public string PublishedOn { get; set; }

        /// <summary>
        /// The disruption last update time.
        /// </summary>
        public string LastUpdated { get; set; }

        /// <summary>
        /// The time from which this disruption is in effect.
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// The time at which this disruption ends.
        /// </summary>
        public string ToDate { get; set; }
    }
}