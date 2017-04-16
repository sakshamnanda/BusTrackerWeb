using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTrackerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTrackerWeb.Models.Models.Tests
{
    [TestClass()]
    public class PtvApiSignerModelTests
    {
        [TestMethod()]
        public void SignApiUrlTest()
        {
            string key = "7c8fd0c8-c6b6-475e-b6fe-ba396da3e254";
            string developerId = "3000207";
            string baseUrl = "http://timetableapi.ptv.vic.gov.au";
            string requestUrl = "/v3/routes";

            PtvApiSignitureModel apiSigniture = new PtvApiSignitureModel(key, developerId);

            string signedUrl = apiSigniture.SignApiUrl(baseUrl, requestUrl);

            Assert.IsTrue(signedUrl == "http://timetableapi.ptv.vic.gov.au/v3/routes?devid=3000207&signature=40F449FB1406DA0F171C13236B4A183B87419C5B");
        }
    }
}