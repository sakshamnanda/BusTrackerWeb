using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTrackerWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerWeb.Models;

namespace BusTrackerWeb.Controllers.Tests
{
    [TestClass()]
    public class PtvApiClientControllerTests
    {
        [TestMethod()]
        public async Task GetRoutesAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();

            List<RouteModel> routes = await apiControl.GetRoutesAsync();

            RouteModel testRoute =
                routes.Find(r => r.RouteName.Contains("Geelong Station") &&
                r.RouteName.Contains("Deakin University") &&
                r.RouteNumber == "41");

            Assert.IsTrue(testRoute.RouteId == 10846);
        }

        [TestMethod()]
        public async Task GetRouteRunsAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();
            RouteModel route = new RouteModel { RouteId = 10846 };

            List<RunModel> runs = await apiControl.GetRouteRunsAsync(route);

            Assert.IsNotNull(runs.Count != 0);
        }

        [TestMethod()]
        public async Task GetRoutePatternAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();
            RouteModel route = new RouteModel { RouteId = 10846 };
            List<RunModel> runs = await apiControl.GetRouteRunsAsync(route);


            List<RunDeparture> departures = await apiControl.GetRunDeparturesAsync(runs.Last());

            Assert.IsTrue(departures.Count == 34);
        }

        [TestMethod()]
        public async Task GetRouteStopsAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();
            RouteModel route = new RouteModel { RouteId = 10846 };

            List<StopModel> stops = await apiControl.GetRouteStopsAsync(route);

            Assert.IsTrue(stops.Count == 52);
        }


        [TestMethod()]
        public async Task GetStopDepartureAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();
            RouteModel route = new RouteModel { RouteId = 10846 };
            StopModel stop = new StopModel { StopId = 27930 };

            DepartureModel nextDeparture = await apiControl.GetStopDepartureAsync(stop, route);

            Assert.IsNotNull(nextDeparture);
            Assert.IsTrue(nextDeparture.ScheduledDeparture >= DateTime.Now);
        }

        [TestMethod()]
        public async Task GetRouteDirectionsAsyncTest()
        {
            PtvApiClientController apiControl = new PtvApiClientController();
            RouteModel route = new RouteModel { RouteId = 10846 };

            List<DirectionModel> directions = await apiControl.GetRouteDirectionsAsync(route);

            Assert.IsNotNull(directions.Count == 2);
        }

    }
}