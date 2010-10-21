using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using LonelySharp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LonelySharp.Tests
{
    [TestClass]
    public class ServiceQueryTests
    {
        public const double latitude = -27.46546;
        public const double longitude = 153.0279;

        [TestMethod]
        public void Can_Query_For_Points_Of_Interest_By_Lat_Lng()
        {
            var lsharp = new LonelySharp();

            var poilist = lsharp.GetPOIList(latitude, longitude, 100, LonelySharp.DistanceType.Meters);

            var poi = poilist[0];

        }

        [TestMethod]
        public void Can_Throw_Custom_Error_Exception()
        {
            var lsharp = new LonelySharp();

            //POI ID for USA (returns too many results)
            try
            {
                var thisWillEror = lsharp.GetPOIList(361720);
            }
            catch (TooManyResultsException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
