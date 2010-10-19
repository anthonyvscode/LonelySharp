using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LonelySharp.Tests
{
    [TestClass]
    public class ServiceQueryTests
    {
        public const double latitude = -33.920940998916;
        public const double longitude = 151.256557703018;

        [TestMethod]
        public void Can_Query_For_Points_Of_Interest_By_Lat_Lng()
        {
            var lsharp = new LonelySharp();

            var poilist = lsharp.GetPOIList(latitude, longitude, 20);
        }
    }
}
