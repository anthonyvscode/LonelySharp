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
        /// <summary>
        /// Returns the full POI data set.
        /// </summary>
        /// <param name="poiID">Point of Interest ID</param>
        /// <returns></returns>
        public POI GetPOI(int poiID)
        {
            var request = new RestRequest { Resource = "pois/{poi-id}" };
            request.AddParameter("poi-id", poiID, ParameterType.UrlSegment);
            return Execute<POI>(request);
        }

        public POICollection GetPOIList(double north, double south, double east, double west)
        {
            var request = new RestRequest { Resource = "bounding_boxes/{north},{south},{east},{west}/pois" };
            request.AddParameter("north", north, ParameterType.UrlSegment);
            request.AddParameter("south", south, ParameterType.UrlSegment);
            request.AddParameter("east", east, ParameterType.UrlSegment);
            request.AddParameter("west", west, ParameterType.UrlSegment);
            return Execute<POICollection>(request);
        }

        public POICollection GetPOIList(double latitude, double longitude, int distance)
        {
            return GetPOIList(latitude, longitude, distance, DistanceType.Kilometers);
        }

        public POICollection GetPOIList(double latitude, double longitude, int distance, DistanceType measurementType)
        {
            double calculatedDistance;

            switch (measurementType)
            {
                case DistanceType.Meters:
                    calculatedDistance = Convert.ToDouble((double)distance / (double)1000);
                    break;
                case DistanceType.Miles:
                    calculatedDistance = distance / 0.621371192;
                    break;
                default:
                    calculatedDistance = distance;
                    break;
            }

            GeoLocation myLocation = GeoLocation.FromDegrees(latitude, longitude);

            GeoLocation[] coordinates = myLocation.BoundingCoordinates(calculatedDistance);

            double north = coordinates[1].getLatitudeInDegrees();
            double south = coordinates[0].getLatitudeInDegrees();
            double east = coordinates[1].getLongitudeInDegrees();
            double west = coordinates[0].getLongitudeInDegrees();

            return GetPOIList(north, south, east, west);
        }

        public POICollection GetPOIList(Place place)
        {
            return GetPOIList(place, null);
        }

        public POICollection GetPOIList(Place place, POIType? poiType)
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

        public POICollection GetPOIList(int placeID)
        {
            return GetPOIList(placeID, null);
        }

        public POICollection GetPOIList(int placeID, POIType? poiType)
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

        public enum DistanceType
        {
            Kilometers,
            Meters,
            Miles
        }
    }
}
