using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTrackerWeb.Models
{
    public class PtvApiGatewayModel
    {
        const string PTV_API_BASE_URL = @"http://timetableapi.ptv.vic.gov.au";

        public PtvApiSignerModel ApiSigner { get; set; }

        public PtvApiGatewayModel(PtvApiSignerModel signer)
        {
            ApiSigner = signer;
        }

        public List<RouteModel> GetRoutes()
        {
            const string GET_ROUTES_REQUEST = @"/v3/routes";

            List<RouteModel> ptvRoutes = new List<Models.RouteModel>();

            string httpRequest = ApiSigner.SignApiUrl(PTV_API_BASE_URL, GET_ROUTES_REQUEST);
                                   

            return ptvRoutes;
        }
    }
}