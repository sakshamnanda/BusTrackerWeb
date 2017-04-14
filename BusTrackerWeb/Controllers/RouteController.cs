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
            RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

            // Get all stops for a given route.
            List<DirectionModel> directions = await WebApiApplication.PtvApiControl.GetRouteDirectionsAsync(route);

            return directions;
        }

        [ActionName("GetRouteDirection")]
        public async Task<DirectionModel> GetRouteDirection(int directionId, int routeId)
        {
            // Get the route.
            RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

            // Get all stops for a given route.
            DirectionModel direction = await WebApiApplication.PtvApiControl.GetDirectionAsync(directionId, route);

            return direction;
        }


        [ActionName("GetRouteNextDeparture")]
        public async Task<List<DepartureModel>> GetRouteNextDeparture(int routeId, int directionId)
        {
            // Get the route.
            RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

            // Get route direction.
            DirectionModel direction = await WebApiApplication.PtvApiControl.GetDirectionAsync(directionId, route);

            // Get all runs for a route.
            List<RunModel> runs = await WebApiApplication.PtvApiControl.GetRouteRunsAsync(route);

            // Get the stopping pattern for each run.
            foreach (RunModel run in runs)
            {
                run.StoppingPattern = await WebApiApplication.PtvApiControl.GetStoppingPatternAsync(run);
            }
            
            // Filter runs by direction.
            runs = runs.Where(r => r.StoppingPattern.Departures.First().Direction.DirectionId == directionId).ToList();

            // Find the run that has the next departure from last stop, this is the current run.
            RunModel nextRun = runs.OrderBy(r => r.StoppingPattern.Departures.Last().ScheduledDeparture).First();

            // Return the departures for the current run.

            return nextRun.StoppingPattern.Departures;
        }

    }
}