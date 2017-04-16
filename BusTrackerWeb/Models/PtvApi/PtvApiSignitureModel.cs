using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    /// <summary>
    /// This class models a PTV API URL Request signer with a HMAC-SHA1 hash.  As per document:
    /// PTV 2016,PTV Timetable API – API Key and Signature information', 
    /// https://static.ptv.vic.gov.au/PTV/PTV%20docs/API/1475462320/PTV-Timetable-API-key-and-signature-document.RTF.
    /// </summary>
    public class PtvApiSignitureModel
    {
        /// <summary>
        /// The unique API Key provided by PTV.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The Developer Id provided by PTV.
        /// </summary>
        public string ApiDeveloperId { get; set; }

        /// <summary>
        /// Custom constuctor to initilise class properties.
        /// </summary>
        /// <param name="key">The unique API Key provided by PTV.</param>
        /// <param name="developerId">The Developer Id provided by PTV.</param>
        public PtvApiSignitureModel(string key, string developerId)
        {
            ApiKey = key;
            ApiDeveloperId = developerId;
        }

        /// <summary>
        /// This function concatenates the request Url with the Developer ID before using a HMACSHA1
        /// hash function to generate a request signiture. The base Url, request Url and signture 
        /// are then formated into a single signed Url.
        /// </summary>
        /// <param name="baseUrl">PTV API base Url without API request.</param>
        /// <param name="requestUrl">API request Url</param>
        /// <returns>PTV API Url.</returns>
        public string SignApiUrl(string baseUrl, string requestUrl)
        {
            string signedUrl = string.Empty;

            // Append the Developer Id to the request.
            string hashUrl = string.Format("{0}{1}devid={2}", requestUrl, 
                requestUrl.Contains("?") ? "&" : "?", ApiDeveloperId);
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            // Encode the API Key.
            byte[] keyBytes = encoding.GetBytes(ApiKey);

            // Hash the Url including requst Url and Developer Id to create signiture.
            byte[] urlBytes = encoding.GetBytes(hashUrl);
            byte[] tokenBytes = new System.Security.Cryptography.HMACSHA1(keyBytes).
                ComputeHash(urlBytes);
            var sb = new System.Text.StringBuilder();

            // Convert signature array to string.
            Array.ForEach<byte>(tokenBytes, x => sb.Append(x.ToString("X2")));

            // Append signiture to base Url and request.
            signedUrl = string.Format("{0}{1}&signature={2}", baseUrl, hashUrl, sb.ToString());

            return signedUrl;
        }
    }
}