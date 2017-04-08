using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BusTrackerWeb.Controllers
{
    public class BusController : ApiController
    {
        /// <summary>
        /// Update the longitude and latitude of a bus.
        /// </summary>
        /// <param name="bus">The bus to be updated.</param>
        /// <returns>200 if updated.</returns>
        [ActionName("PutBusLocation")]
        public int PutBusLocation([FromBody]BusModel bus)
        {
            Debug.WriteLine("PUT api/BusController: BusId={0}, BusRegoNumber={1}, BusLongitude={2}, BusLongitutde={3}",
                bus.BusId, bus.BusRegoNumber, bus.BusLongitude, bus.BusLatitude);

            return 200;
        }
    }
}