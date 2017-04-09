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
        /// <returns></returns>
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
    }
}