using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// Bus Tracker Bus Model.
    /// </summary>
    public class BusModel
    {
        /// <summary>
        /// The Bus Tracker ID allocated to the bus.
        /// </summary>
        public int BusId { get; set; }

        /// <summary>
        /// The bus road registration number.
        /// </summary>
        public string BusRegoNumber { get; set; }

        /// <summary>
        /// The current GPS longitude of the bus.
        /// </summary>
        public decimal BusLongitude { get; set; }


        /// <summary>
        /// The current GPS latitude of the bus.
        /// </summary>
        public decimal BusLatitude { get; set; }

        /// <summary>
        /// The bus's previous stop.
        /// </summary>
        public int BusPreviousStopId { get; set; }
    }
}