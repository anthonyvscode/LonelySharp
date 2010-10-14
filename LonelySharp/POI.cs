using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LonelySharp.Model;
using RestSharp;

namespace LonelySharp
{
    public partial class LonelySharp
    {
        public POICollection GetPOIListByID(double north, double south, double east, double west)
        {
            var request = new RestRequest { Resource = "bounding_boxes/{north},{south},{east},{west}/pois" };
            request.AddParameter("north", north, ParameterType.UrlSegment);
            request.AddParameter("south", south, ParameterType.UrlSegment);
            request.AddParameter("east", east, ParameterType.UrlSegment);
            request.AddParameter("west", west, ParameterType.UrlSegment);
            return Execute<POICollection>(request);
        }

        /// <summary>
        /// Returns the full POI data set.
        /// </summary>
        /// <param name="poiID">Point of Interest ID</param>
        /// <returns></returns>
        public POI GetPOIByID(int poiID)
        {
            var request = new RestRequest { Resource = "pois/{poi-id}" };
            request.AddParameter("poi-id", poiID, ParameterType.UrlSegment);
            return Execute<POI>(request);
        }

        public POICollection GetPOIListByPlace(Place place)
        {
            return GetPOIListByPlace(place, null);
        }

        public POICollection GetPOIListByPlace(Place place, POIType? poiType)
        {
            var request = new RestRequest { Resource = "bounding_boxes/{north},{south},{east},{west}/pois" };
            request.AddParameter("north", place.Northlatitude, ParameterType.UrlSegment);
            request.AddParameter("south", place.Southlatitude, ParameterType.UrlSegment);
            request.AddParameter("east", place.Eastlongitude, ParameterType.UrlSegment);
            request.AddParameter("west", place.Westlongitude, ParameterType.UrlSegment);

            if (poiType != null)
                request.AddParameter("poi_type", poiType);

            return Execute<POICollection>(request);
        }

        public POICollection GetPOIListByPlaceID(int placeID)
        {
            return GetPOIListByPlaceID(placeID, null);
        }

        public POICollection GetPOIListByPlaceID(int placeID, POIType? poiType)
        {
            var request = new RestRequest { Resource = "places/{placeID}/pois" };
            request.AddParameter("placeID", placeID, ParameterType.UrlSegment);
            request.AddParameter("type-name", poiType, ParameterType.UrlSegment);

            if (poiType != null)
                request.AddParameter("poi_type", poiType);

            return Execute<POICollection>(request);
        }

        public enum POIType
        {
            Eat,
            Sleep,
            See,
            Shop,
            Night,
            Do
        }
    }
}
