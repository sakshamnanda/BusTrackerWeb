using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BusTrackerWeb.Controllers
{
    /// <summary>
    /// Controls all Bus Tracker API Route action requests.
    /// </summary>
    public class RouteController : ApiController
    {
        /// <summary>
        /// Get a collection of bus routes.
        /// </summary>
        /// <returns>An array of bus route objects.</returns>
        [ActionName("GetRoutes")]
        public async Task<List<RouteModel>> GetRoutes()
        {
            return await WebApiApplication.PtvApiControl.GetRoutesAsync();
        }

        [ActionName("GetRoute")]
        public async Task<RouteModel> GetRoute(int routId)
        {
            return await WebApiApplication.PtvApiControl.GetRouteAsync(routId);
        }


        [ActionName("GetRouteDirections")]
        public async Task<List<DirectionModel>> GetRouteDirections(int routeId)
        {
            // Create a new route.
            RouteModel route = new RouteModel { RouteId = routeId };

            // Get all stops for a given route.
            List<DirectionModel> directions = await WebApiApplication.PtvApiControl.GetRouteDirectionsAsync(route);

            return directions;
        }

        [ActionName("GetRouteNextDeparture")]
        public async Task<List<DepartureModel>> GetRouteNextDeparture(int routeId, int directionId)
        {
            // Get the route.
            RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

            // Get route directions.
            

            // Get all stops for a given route.
            List<StopModel> stops = await WebApiApplication.PtvApiControl.GetRouteStopsAsync(route);

            // Get next departure for each stop along route.
            List<DepartureModel> departures = new List<DepartureModel>();
            foreach(StopModel stop in stops)
            {
                departures.Add(await WebApiApplication.PtvApiControl.GetStopDepartureAsync(stop, route));
            }

            // Order departures by time.
            departures = departures.OrderBy(d => d.ScheduledDeparture).ToList();

            return departures;
        }

    }
}