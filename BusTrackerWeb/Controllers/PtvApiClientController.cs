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
        /// <returns>PTV API Route Response.</returns>
        public async Task<PtvApiRouteResponse> GetRoutesAsync()
        {
            // Get all bus type routes.
            const string GET_ROUTES_REQUEST = @"/v3/routes?route_types=2";

            PtvApiRouteResponse routeResponse = new PtvApiRouteResponse();

            try
            {
                // Sign the API request with developer ID and key.
                string clientRequest = ApiSigner.SignApiUrl(PTV_API_BASE_URL, GET_ROUTES_REQUEST);

                // Send a request to the PTV API.
                HttpResponseMessage response = await Client.GetAsync(clientRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialise the JSON API response into strongly typed objects.
                    routeResponse = await response.Content.ReadAsAsync<PtvApiRouteResponse>();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("GetRoutesAsync Exception: {0}", e.Message);
            }

            return routeResponse;
        }

        /// <summary>
        /// Get all route runs from the PTV API.
        /// </summary>
        /// <returns>PTV API Runs Response.</returns>
        public async Task<PtvApiRunResponse> GetRouteRunsAsync(int routeId)
        {
            // Get all bus type routes.
            const string GET_ROUTES_REQUEST = @"/v3/runs/route/{0}";

            PtvApiRunResponse runResponse = new PtvApiRunResponse();

            try
            {
                // Add request parameters.
                string parameterisedRequest = string.Format(GET_ROUTES_REQUEST, routeId);

                // Sign the API request with developer ID and key.
                string clientRequest = ApiSigner.SignApiUrl(PTV_API_BASE_URL, parameterisedRequest);

                // Send a request to the PTV API.
                HttpResponseMessage response = await Client.GetAsync(clientRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialise the JSON API response into strongly typed objects.
                    runResponse = await response.Content.ReadAsAsync<PtvApiRunResponse>();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("GetRoutesAsync Exception: {0}", e.Message);
            }

            return runResponse;
        }


        /// <summary>
        /// Get the pattern for a route run from the PTV API.
        /// </summary>
        /// <returns>PTV API Stopping Pattern.</returns>
        public async Task<PtvApiStoppingPattern> GetRoutePatternAsync(int runId)
        {
            // Get all bus type routes.
            const string GET_ROUTES_REQUEST = @"/v3/pattern/run/{0}/route_type/2";

            PtvApiStoppingPattern patternResponse = new PtvApiStoppingPattern();

            try
            {
                // Add request parameters.
                string parameterisedRequest = string.Format(GET_ROUTES_REQUEST, runId);

                // Sign the API request with developer ID and key.
                string clientRequest = ApiSigner.SignApiUrl(PTV_API_BASE_URL, parameterisedRequest);

                // Send a request to the PTV API.
                HttpResponseMessage response = await Client.GetAsync(clientRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialise the JSON API response into strongly typed objects.
                    patternResponse = await response.Content.ReadAsAsync<PtvApiStoppingPattern>();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("GetRoutesAsync Exception: {0}", e.Message);
            }

            return patternResponse;
        }

    }
}