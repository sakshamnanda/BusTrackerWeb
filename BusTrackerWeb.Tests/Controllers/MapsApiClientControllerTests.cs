using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusTrackerWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusTrackerWeb.Models;

namespace BusTrackerWeb.Controllers.Tests
{
    [TestClass()]
    public class MapsApiClientControllerTests
    {

        [TestMethod()]
        public void MakeRequestTest()
        {
            MapsApiClientController apiControl = new MapsApiClientController();

            apiControl.MakeRequest();
        }
    }
}