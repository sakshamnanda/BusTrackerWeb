using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// PTV API Direction Response Model, Refer:
    /// http://timetableapi.ptv.vic.gov.au/swagger/ui/index
    /// </summary>
    public class PtvApiDirectionResponse
    {
        public PtvApiDirection Direction { get; set; }

        public PtvApiStatus Status { get ; set; }
    }
}