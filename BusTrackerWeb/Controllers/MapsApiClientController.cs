using BusTrackerWeb.Models;
using BusTrackerWeb.Models.MapsApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;

namespace BusTrackerWeb.Controllers
{
    /// <summary>
    /// Controls all Google Maps API requests.
    /// </summary>
    public class MapsApiClientController
    {
        const string PTV_API_BASE_URL = "https://maps.googleapis.com/maps/api/directions/json?origin=-38.145,144.355&destination=-38.198,144.300&waypoints=-38.157,144.356|-38.189,144.343&departure_time=now&traffic_model=best_guess&key=AIzaSyAyEgv4_85K8azHU2fYz78xxGAT3ne3egU";

        HttpClient Client { get; set; }

        PtvApiSignitureModel ApiSigner { get; set; }

        /// <summary>
        /// Initialise the clent controller.
        /// </summary>
        public MapsApiClientController()
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //ApiSigner = new Models.PtvApiSignitureModel(Properties.Settings.Default.PtvApiDeveloperKey,
            //    Properties.Settings.Default.PtvApiDeveloperId);
        }


        public DirectionsResponse MakeRequest()
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(PTV_API_BASE_URL) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DirectionsResponse));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    DirectionsResponse jsonResponse = objResponse as DirectionsResponse;

                    return jsonResponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
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
                //string clientRequest = ApiSigner.SignApiUrl(PTV_API_BASE_URL, request);

                // Send a request to the PTV API.
                //HttpResponseMessage httpResponse = await Client.GetAsync(clientRequest);
                HttpResponseMessage httpResponse = await Client.GetAsync(PTV_API_BASE_URL);


                if (httpResponse.IsSuccessStatusCode)
                {
                    // Deserialise the JSON API response into strongly typed objects.
                    response = await httpResponse.Content.ReadAsAsync<T>();
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("GetPtvApiResponse Exception: {0}", e.Message);
            }

            return response;
        }
    }
}