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
    public class PtvApiGatewayModelTests
    {
        string DeveloperId = "3000207";
        string DeveloperKey = "7c8fd0c8-c6b6-475e-b6fe-ba396da3e254";

        [TestMethod()]
        public void GetRoutesTest()
        {

            PtvApiSignerModel signer = new PtvApiSignerModel(DeveloperKey, DeveloperId);
            PtvApiGatewayModel gateway = new PtvApiGatewayModel(signer);

            Assert.Fail();
        }
    }
}