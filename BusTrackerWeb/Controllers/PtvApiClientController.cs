using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace BusTrackerWeb.Controllers
{
    /// <summary>
    /// Controls all Bus Tracker PTV API requests.
    /// </summary>
    public class PtvApiClientController
    {
        const string PTV_API_BASE_URL = "http://timetableapi.ptv.vic.gov.au";

        HttpClient Client { get; set; }

        PtvApiSignerModel ApiSigner { get; set; }

        /// <summary>
        /// Initialise the clent controller.
        /// </summary>
        public PtvApiClientController()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ApiSigner = new Models.PtvApiSignerModel(Properties.Settings.Default.PtvApiDeveloperKey,
                Properties.Settings.Default.PtvApiDeveloperId);
        }

        /// <summary>
        /// Get all bus routes from the PTV API.
        /// </summary>
        /// <returns>Bus Route collection.</returns>
        public async Task<List<RouteModel>> GetRoutesAsync()
        {
            List<RouteModel> routes = new List<RouteModel>();

            // Get all bus type routes.
            string getRoutesRequest = @"/v3/routes?route_types=2";
            PtvApiRouteResponse routeResponse = 
                await GetPtvApiResponse<PtvApiRouteResponse>(getRoutesRequest);

            // If the response is healthy try to convert the API response to a route collection.
            if(routeResponse.Status.Health == 1)
            {
                foreach(PtvApiRoute apiRoute in routeResponse.Routes)
                {
                    try
                    {
                        routes.Add(new RouteModel
                        {
                            RouteId = apiRoute.route_id,
                            RouteName = apiRoute.route_name,
                            RouteNumber = apiRoute.route_number
                        });
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError("GetRoutesAsync Exception: {0}", e.Message);
                    }
                }
            }

            // Order by route name.
            routes = routes.OrderBy(r => r.RouteName).ToList();
                
            return routes;
        }

        /// <summary>
        /// Get all bus routes from the PTV API.
        /// </summary>
        /// <returns>Bus Route collection.</returns>
        public async Task<List<DirectionModel>> GetRouteDirectionsAsync(RouteModel route)
        {
            List<DirectionModel> directions = new List<DirectionModel>();

            // Get all route directions.
            string getDirectionsRequest = string.Format("/v3/directions/route/{0}", route.RouteId);
            PtvApiDirectionsResponse directionsResponse =
                await GetPtvApiResponse<PtvApiDirectionsResponse>(getDirectionsRequest);

            // If the response is healthy try to convert the API response to a direction collection.
            if (directionsResponse.Status.Health == 1)
            {
                foreach (PtvApiDirection apiDirection in directionsResponse.Directions)
                {
                    try
                    {
                        directions.Add(new DirectionModel
                        {
                            DirectionId = apiDirection.direction_id,
                            DirectionName = apiDirection.direction_name,
                            Route = route,
                            RouteType = apiDirection.route_type
                        });
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError("GetRouteDirectionsAsync Exception: {0}", e.Message);
                    }
                }
            }

            // Order by route name.
            directions = directions.OrderBy(r => r.DirectionName).ToList();

            return directions;
        }


        /// <summary>
        /// Get the stops for a route from the PTV API.
        /// </summary>
        /// <returns>PTV API Stopping Pattern.</returns>
        public async Task<List<StopModel>> GetRouteStopsAsync(RouteModel route)
        {
            // Get all bus type routes.
            string getStopsRequest = string.Format("/v3/stops/route/{0}/route_type/2", route.RouteId);

            PtvApiStopOnRouteResponse stopsResponse =
                await GetPtvApiResponse<PtvApiStopOnRouteResponse>(getStopsRequest);

            // If the response is healthy try to convert the API response to a route collection.
            List<StopModel> stops = new List<StopModel>();
            if (stopsResponse.Status.Health == 1)
            {
                foreach (PtvApiStopOnRoute apiStop in stopsResponse.Stops)
                {
                    try
                    {
                        stops.Add(new StopModel
                        {
                            StopId = apiStop.stop_id,
                            StopName = apiStop.stop_name,
                            StopLatitude = apiStop.stop_latitude,
                            StopLongitude = apiStop.stop_longitude
                        });
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError("GetRouteStopsAsync Exception: {0}", e.Message);
                    }
                }
            }

            return stops;
        }

        /// <summary>
        /// Get all route runs from the PTV API.
        /// </summary>
        /// <returns>PTV API Runs Response.</returns>
        public async Task<List<RunModel>> GetRouteRunsAsync(RouteModel route)
        {
            const int MAX_RUNS_TAKEN = 48; 

            // Get all stops on a run.
            List<StopModel> stops = await GetRouteStopsAsync(route);
            
            // Get all route runs.
            string getRunsRequest = string.Format("/v3/runs/route/{0}", route.RouteId);
            PtvApiRunResponse runResponse = await GetPtvApiResponse<PtvApiRunResponse>(getRunsRequest);

            // If the response is healthy try to convert the API response to a run collection.
            List<RunModel> runs = new List<RunModel>();
            if (runResponse.Status.Health == 1)
            {
                foreach (PtvApiRun apiRun in runResponse.Runs)
                {
                    try
                    {
                        runs.Add(new RunModel
                        {
                            RunId = apiRun.run_id,
                            Route = route,
                            FinalStop = stops.First(s => s.StopId == apiRun.final_stop_id)
                        });
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError("GetRouteRunsAsync Exception: {0}", e.Message);
                    }
                }

                runs = runs.OrderByDescending(r => r.RunId).Take(MAX_RUNS_TAKEN).ToList();
            }

            return runs;
        }
        
        /// <summary>
        /// Get the departures for a specific run from the PTV API.
        /// </summary>
        /// <returns>PTV API Stopping Pattern.</returns>
        public async Task<List<RunDeparture>> GetRunDeparturesAsync(RunModel run)
        {
            // Get all stops on a run.
            List<StopModel> stops = await GetRouteStopsAsync(run.Route);

            // Get all bus type routes.
            string getPatternRequest = string.Format("/v3/pattern/run/{0}/route_type/2", run.RunId);

            PtvApiStoppingPattern patternResponse = 
                await GetPtvApiResponse<PtvApiStoppingPattern>(getPatternRequest);

            // If the response is healthy try to convert the API response to a pattern collection.
            List<RunDeparture> runDepartures = new List<RunDeparture>();
            if (patternResponse.Status.Health == 1)
            {
                foreach (PtvApiDeparture apiDeparture in patternResponse.Departures)
                {
                    try
                    {
                        runDepartures.Add(new RunDeparture
                        {
                            Run = run,
                            Stop = stops.First(s => s.StopId == apiDeparture.stop_id),
                            RunScheduledDeparture = Convert.ToDateTime(apiDeparture.scheduled_departure_utc),
                        });

                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError("GetRoutePatternAsync Exception: {0}", e.Message);
                    }
                }

                runDepartures = runDepartures.OrderBy(r => r.RunScheduledDeparture).ToList();
            }

            return runDepartures;
        }

        /// <summary>
        /// Get all bus routes from the PTV API.
        /// </summary>
        /// <returns>Bus Route collection.</returns>
        public async Task<DepartureModel> GetStopDepartureAsync(StopModel stop, RouteModel route)
        {
            List<DepartureModel> departures = new List<DepartureModel>();
            DepartureModel nextDeparture = new DepartureModel();

            // Get all departures for a route stop.
            string getDeparturesRequest =  
                string.Format("/v3/departures/route_type/2/stop/{0}/route/{1}", stop.StopId, 
                route.RouteId);

            PtvApiDeparturesResponse departuresResponse =
                await GetPtvApiResponse<PtvApiDeparturesResponse>(getDeparturesRequest);


            // If the response is healthy try to convert the API response to a route collection.
            if (departuresResponse.Status.Health == 1)
            {
                foreach (PtvApiDeparture apiDeparture in departuresResponse.Departures)
                {
                    try
                    {
                        departures.Add(new DepartureModel
                        {
                            Stop = stop,
                            Route = route,
                            Run = new RunModel { RunId = apiDeparture.run_id },
                            Direction = new DirectionModel { DirectionId = apiDeparture.direction_id },
                            Disruptions = apiDeparture.disruption_ids,
                            ScheduledDeparture = DateTime.Parse(apiDeparture.scheduled_departure_utc, null, DateTimeStyles.AssumeLocal),
                            AtPlatform = apiDeparture.at_platform,
                            PlatformNumber = apiDeparture.platform_number,
                            Flags = apiDeparture.flags
                        });
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError("GetDepartureAsync Exception: {0}", e.Message);
                    }
                }
            }

            // Order by route name.
            departures = departures.OrderBy(d => d.ScheduledDeparture).ToList();

            // Filter out past departures.
            departures = departures.Where(d => d.ScheduledDeparture >= DateTime.Now).ToList();

            // Return the next scheduled departure.
            nextDeparture = departures.First();

            return nextDeparture;
        }


        /// <summary>
        /// Generic PTV API Get Request function.
        /// </summary>
        /// <typeparam name="T">PTV API object type to be retrieved.</typeparam>
        /// <param name="request">API request string.</param>
        /// <returns>The API response.</returns>
        private async Task<T> GetPtvApiResponse<T>(string request)
        {
            T response = default(T);

            try
            {
                // Sign the API request with developer ID and key.
                string clientRequest = ApiSigner.SignApiUrl(PTV_API_BASE_URL, request);

                // Send a request to the PTV API.
                HttpResponseMessage httpResponse = await Client.GetAsync(clientRequest);

                if (httpResponse.IsSuccessStatusCode)
                {
                    // Deserialise the JSON API response into strongly typed objects.
                    response = await httpResponse.Content.ReadAsAsync<T>();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("GetPtvApiResponse Exception: {0}", e.Message);
            }

            return response;
        }
    }
}