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
    public class PtvApiClientController
    {
        HttpClient Client { get; set; }

        PtvApiSignerModel ApiSigner { get; set; }

        public PtvApiClientController()
        {
            Client = new HttpClient();
            //Client.BaseAddress = new Uri("http://timetableapi.ptv.vic.gov.au");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ApiSigner = new Models.PtvApiSignerModel(Properties.Settings.Default.PtvApiDeveloperKey,
                Properties.Settings.Default.PtvApiDeveloperId);
        }


        public async Task<PtvApiRouteResponse> GetRoutesAsync()
        {
            PtvApiRouteResponse routeResponse = new PtvApiRouteResponse();

            const string GET_ROUTES_REQUEST = @"/v3/routes?route_types=2";

            string clientRequest = ApiSigner.SignApiUrl("http://timetableapi.ptv.vic.gov.au", GET_ROUTES_REQUEST);

            HttpResponseMessage response = await Client.GetAsync(clientRequest);

            if (response.IsSuccessStatusCode)
            {
                routeResponse = await response.Content.ReadAsAsync<PtvApiRouteResponse>();
            }

            return routeResponse;
        }
    }
}