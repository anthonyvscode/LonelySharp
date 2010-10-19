using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LonelySharp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Deserializers;

namespace LonelySharp.Tests
{
    /// <summary>
    /// Summary description for Places
    /// </summary>
    [TestClass]
    public class Places
    {
        public Places()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_Deserialize_Place_Listing()
        {
            var xmlpath = Environment.CurrentDirectory + @"\..\..\..\LonelySharp.Tests\Responses\Places_List.xml";
            var doc = XDocument.Load(xmlpath);
            var response = new RestResponse { Content = doc.ToString() };

            var d = new XmlAttributeDeserializer();
            var output = d.Deserialize<PlaceCollection>(response);

            Assert.AreEqual(output[0].Fullname, "Australia & Pacific -> Australia -> Queensland -> Brisbane");
            Assert.AreEqual(output[0].Northlatitude, -27.434999);
        }

        [TestMethod]
        public void Can_Deserialize_POI_List()
        {
            var xmlpath = Environment.CurrentDirectory + @"\..\..\..\LonelySharp.Tests\Responses\POI_List.xml";
            var doc = XDocument.Load(xmlpath);
            var response = new RestResponse { Content = doc.ToString() };

            var d = new XmlAttributeDeserializer();
            var output = d.Deserialize<POICollection>(response);

            Assert.AreEqual(output[0].ID, 1077533);
            Assert.AreEqual(output[0].POIType.ToString(), "Eat");
            Assert.AreEqual(output.Count, 222);
        }

        [TestMethod]
        public void Can_Deserialize_POI()
        {
            var xmlpath = Environment.CurrentDirectory + @"\..\..\..\LonelySharp.Tests\Responses\POI_Details.xml";
            var doc = XDocument.Load(xmlpath);
            var response = new RestResponse { Content = doc.ToString() };

            var d = new XmlAttributeDeserializer();
            var output = d.Deserialize<POI>(response);

            Assert.AreEqual(output.Name, "Alhambra Lounge");
            Assert.AreEqual(output.Urls[0].Value, "www.alhambralounge.com");
        }
    }
}
