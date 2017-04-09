using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTrackerWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerWeb.Models;

namespace BusTrackerWeb.Controllers.Models.Tests
{
    [TestClass()]
    public class PtvApiClientControllerTests
    {
        [TestMethod()]
        public async Task GetRoutesAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();

            PtvApiRouteResponse response = await apiControl.GetRoutesAsync();

            PtvApiRoute testRoute = 
                response.Routes.Find(r => r.route_name.Contains("Geelong Station") && 
                r.route_name.Contains("Deakin University") && 
                r.route_number == "41");

            Assert.IsTrue(testRoute.route_id == 10846);
        }
    }
}