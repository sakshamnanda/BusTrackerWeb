using BusTrackerWeb.Models;
using BusTrackerWeb.Models.MapsApi;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
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
        /// Get a list of all PTV bus routes.
        /// </summary>
        /// <returns>An array of RouteModel objects.</returns>
        [ActionName("GetRoutes")]
        public async Task<List<RouteModel>> GetRoutes()
        {
            return await WebApiApplication.PtvApiControl.GetRoutesAsync();
        }

        /// <summary>
        /// Get a list of all PTV bus routes filtered by route name.
        /// </summary>
        /// <returns>An array of RouteModel objects.</returns>
        [ActionName("GetRoutesByName")]
        public async Task<List<RouteModel>> GetRoutesByName(string routeName)
        {
            return await WebApiApplication.PtvApiControl.GetRoutesByNameAsync(routeName);
        }


        /// <summary>
        /// Get a specific PTV bus route.
        /// </summary>
        /// <param name="routeId">PTV Bus Route Id.</param>
        /// <returns>A RouteModel object.</returns>
        [ActionName("GetRoute")]
        public async Task<RouteModel> GetRoute(int routeId)
        {
            RouteModel route = new RouteModel();

            try
            {
                route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);
            }
            catch(Exception e)
            {
                Trace.TraceError("GetRoute Exception: {0}", e.Message);
            }

            return route;
        }

        /// <summary>
        /// Get a list of PTV directions for a specific route.
        /// </summary>
        /// <param name="routeId">PTV Bus Route Id.</param>
        /// <returns>An array of DirectionModel objects.</returns>
        [ActionName("GetRouteDirections")]
        public async Task<List<DirectionModel>> GetRouteDirections(int routeId)
        {
            List<DirectionModel> routeDirections = new List<Models.DirectionModel>();

            try
            {
                // Create a new route.
                RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

                // Get all stops for a given route.
                routeDirections = await WebApiApplication.PtvApiControl.
                    GetRouteDirectionsAsync(route);
            }
            catch(Exception e)
            {
                Trace.TraceError("GetRouteDirections Exception: {0}", e.Message);
            }

            return routeDirections;
        }

        /// <summary>
        /// Get the next PTV run scheduled for specific route and direction.
        /// </summary>
        /// <param name="routeId">PTV Bus Route Id.</param>
        /// <param name="directionId">PTV Direction Id.</param>
        /// <returns>A RunModel object.</returns>
        [ActionName("GetRouteNextRun")]
        public async Task<RunModel> GetRouteNextRun(int routeId, int directionId)
        {
            RunModel nextRun = new RunModel();

            try
            {
                // Get the route.
                RouteModel route = await WebApiApplication.PtvApiControl.GetRouteAsync(routeId);

                // Get route direction.
                DirectionModel direction = await WebApiApplication.PtvApiControl.
                    GetDirectionAsync(directionId, route);

                // Get all runs for a route.
                List<RunModel> routeRuns = await WebApiApplication.PtvApiControl.
                    GetRouteRunsAsync(route);

                // Index through all runs to find those that have not expired.
                List<RunModel> currentRuns = new List<RunModel>();
                bool previousRunInPast = false;
                foreach (RunModel run in routeRuns)
                {
                    // Skip this run if the previous run was in the past.
                    // NB:  This condition optimises the algorithm i.e. avoids querying the PTV API for 
                    //      runs that have already expired.
                    if (!previousRunInPast)
                    {
                        run.StoppingPattern = await WebApiApplication.PtvApiControl.
                            GetStoppingPatternAsync(run);

                        // Check the current run for optimisation sentinals.
                        DateTime runLastStoptime = run.StoppingPattern.Departures.Last().
                            ScheduledDeparture;
                        int runDirectionId = run.StoppingPattern.Departures.Last().DirectionId;

                        if ((runDirectionId == directionId) && (runLastStoptime > DateTime.Now))
                        {
                            // This is a future run, update direction attribute and add to current list.
                            run.Direction = direction;
                            currentRuns.Add(run);
                        }
                        else
                        {
                            // This is the first run in the past, ignore this and all remaining runs.
                            previousRunInPast = true;
                        }
                    }
                }

                // Order the current run collection by Last Stop Scheduled Departure Time, the first 
                // run in the ordered collection will be the current run.
                currentRuns = currentRuns.
                    OrderBy(r => r.StoppingPattern.Departures.Last().ScheduledDeparture).ToList();
                nextRun = currentRuns.First();

                // Update the run with the Google Directions API ETA.
                // Build a collection of route geo locations.
                List<GeoCoordinate> routePoints = new List<GeoCoordinate>();
                foreach(DepartureModel departure in nextRun.StoppingPattern.Departures)
                {
                    GeoCoordinate geoLocation = 
                        new GeoCoordinate (Convert.ToDouble(departure.Stop.StopLatitude), 
                        Convert.ToDouble(departure.Stop.StopLongitude));

                    routePoints.Add(geoLocation);  
                }

                // Query Google Directions API.
                DirectionsResponse directionsResponse = WebApiApplication.MapsApiControl.GetDirections(routePoints.ToArray()); 

                // Update each stop on the run with Directions ETA.
                foreach(var leg in directionsResponse.routes.First().legs)
                {

                }
            }
            catch(Exception e)
            {
                Trace.TraceError("GetRouteNextRun Exception: {0}", e.Message);
            }

            return nextRun;
        }
    }
}