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
        
        [ActionName("GetRouteNextRun")]
        public async Task<RunModel> GetRouteNextRun(int routeId, int directionId)
        {
            RunModel nextRun = new Models.RunModel();

            // Get the route.
            RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

            // Get route direction.
            DirectionModel direction = await WebApiApplication.PtvApiControl.GetDirectionAsync(directionId, route);

            // Get all runs for a route.
            List<RunModel> routeRuns = await WebApiApplication.PtvApiControl.GetRouteRunsAsync(route);

            // Get the stopping pattern for each run and find the current runs for this direction.
            List<RunModel> currentRuns = new List<RunModel>();

            // Initialise the sentinal with the last stop time of the last run.
            bool previousRunInPast = false;
            foreach (RunModel run in routeRuns)
            {
                // Skip this run if the previous run was in the past.
                if (!previousRunInPast)
                {
                    run.StoppingPattern = await WebApiApplication.PtvApiControl.GetStoppingPatternAsync(run);

                    // Check the current run for sentinals.
                    DateTime runLastStoptime = run.StoppingPattern.Departures.Last().ScheduledDeparture;
                    int runDirectionId = run.StoppingPattern.Departures.Last().DirectionId;

                    // Update direction property for relevant runs.
                    if ((runDirectionId == directionId) && (runLastStoptime > DateTime.Now))
                    {
                        run.Direction = direction;
                        currentRuns.Add(run);
                    }
                    else
                    {
                        previousRunInPast = true;
                    }
                }
            }

            // Find the run that has the next departure from last stop, this is the current run.
            currentRuns = currentRuns.OrderBy(r => r.StoppingPattern.Departures.Last().ScheduledDeparture).ToList();
             
            nextRun = currentRuns.First();

            return nextRun;
        }
    }
}