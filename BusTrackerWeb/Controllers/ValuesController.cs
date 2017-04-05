using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusTrackerWeb.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    
        /// <summary>
        /// This function updates the current GPS location of a bus running a specific route.
        /// </summary>
        /// <param name="busId">Identifier of the bus running the route.</param>
        /// <param name="runId">Identifier of the run associated with a specific route.</param>
        /// <param name="longitude">Current GPS longitude of the bus.</param>
        /// <param name="latitude">Current GPS latitude of the bus.</param>
        /// <returns>HTTP Response Code</returns>
        public int PutBusLocation(int busId, int runId, decimal longitude, decimal latitude)
        {
            return 0;
        }
    }
}
