using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
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
        /// Get all route runs from the PTV API.
        /// </summary>
        /// <returns>PTV API Runs Response.</returns>
        public async Task<PtvApiRunResponse> GetRouteRunsAsync(int routeId)
        {
            // Get all bus route runs.
            string getRunsRequest = string.Format("/v3/runs/route/{0}", routeId);

            PtvApiRunResponse runResponse = await GetPtvApiResponse<PtvApiRunResponse>(getRunsRequest);

            return runResponse;
        }
        
        /// <summary>
        /// Get the pattern for a route run from the PTV API.
        /// </summary>
        /// <returns>PTV API Stopping Pattern.</returns>
        public async Task<PtvApiStoppingPattern> GetRoutePatternAsync(int runId)
        {
            // Get all bus type routes.
            string getPatternRequest = string.Format("/v3/pattern/run/{0}/route_type/2", runId);

            PtvApiStoppingPattern patternResponse = 
                await GetPtvApiResponse<PtvApiStoppingPattern>(getPatternRequest);

            return patternResponse;
        }

        /// <summary>
        /// Get the stops for a route from the PTV API.
        /// </summary>
        /// <returns>PTV API Stopping Pattern.</returns>
        public async Task<PtvApiStopOnRouteResponse> GetRouteStopsAsync(int routeId)
        {
            // Get all bus type routes.
            string getStopsRequest = string.Format("/v3/stops/route/{0}/route_type/2", routeId);

            PtvApiStopOnRouteResponse stopsResponse =
                await GetPtvApiResponse<PtvApiStopOnRouteResponse>(getStopsRequest);

            return stopsResponse;
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