using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTrackerWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerWeb.Models;
using System.Device.Location;
using BusTrackerWeb.Models.MapsApi;

namespace BusTrackerWeb.Controllers.Tests
{
    [TestClass()]
    public class MapsApiClientControllerTests
    {
        [TestMethod()]
        public void BuildDirectionsQueryTest()
        {
            MapsApiClientController controller = new MapsApiClientController();

            List<GeoCoordinate> routePoints = new List<GeoCoordinate>();

            routePoints.Add(new GeoCoordinate(-38.145, 144.355));
            routePoints.Add(new GeoCoordinate(-38.198, 144.300));
            routePoints.Add(new GeoCoordinate(-38.157, 144.356));
            routePoints.Add(new GeoCoordinate(-38.189, 144.343));

            string apiQuery = controller.BuildDirectionsQuery(routePoints.ToArray());

            Assert.IsTrue(apiQuery == "https://maps.googleapis.com/maps/api/directions/json?origin=-38.145,144.355&destination=-38.189,144.343&waypoints=-38.198,144.3|-38.157,144.356&departure_time=now&traffic_model=best_guess&key=AIzaSyAyEgv4_85K8azHU2fYz78xxGAT3ne3egU");
        }

        [TestMethod()]
        public void GetDirectionsTest()
        {
            MapsApiClientController controller = new MapsApiClientController();

            List<GeoCoordinate> routePoints = new List<GeoCoordinate>();

            routePoints.Add(new GeoCoordinate(-38.145, 144.355));
            routePoints.Add(new GeoCoordinate(-38.198, 144.300));
            routePoints.Add(new GeoCoordinate(-38.157, 144.356));
            routePoints.Add(new GeoCoordinate(-38.189, 144.343));

            DirectionsResponse response = controller.GetDirections(routePoints.ToArray());

            Assert.IsTrue(response.routes.First().legs.Count() == 3);

        }
    }
}