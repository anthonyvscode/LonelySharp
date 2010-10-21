using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LonelySharp.Utilities;

namespace LonelySharp
{
    /**
     * Represents a point on the surface of a sphere. (The Earth is almost
     * spherical.)
     *
     * This code was originally published at
     * http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates#Java
     * 
     * @author Jan Philip Matuschek
     * @version 22 September 2010
     * @converted to C# by Anthony Zigenbine on 19th October 2010
     */
    public class GeoLocation
    {
        private double radLat;  // latitude in radians
        private double radLon;  // longitude in radians

        private double degLat;  // latitude in degrees
        private double degLon;  // longitude in degrees

        private static double MIN_LAT = Helpers.ConvertDegreesToRadians(-90d);  // -PI/2
        private static double MAX_LAT = Helpers.ConvertDegreesToRadians(90d);   //  PI/2
        private static double MIN_LON = Helpers.ConvertDegreesToRadians(-180d); // -PI
        private static double MAX_LON = Helpers.ConvertDegreesToRadians(180d);  //  PI

        private const double earthRadius = 6371.01;

        private GeoLocation()
        {
        }

        /// <summary>
        /// Return GeoLocation from Degrees
        /// </summary>
        /// <param name="latitude">The latitude, in degrees.</param>
        /// <param name="longitude">The longitude, in degrees.</param>
        /// <returns>GeoLocation in Degrees</returns>
        public static GeoLocation FromDegrees(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
                                     {
                                         radLat = Helpers.ConvertDegreesToRadians(latitude),
                                         radLon = Helpers.ConvertDegreesToRadians(longitude),
                                         degLat = latitude,
                                         degLon = longitude
                                     };
            result.CheckBounds();
            return result;
        }

        /// <summary>
        /// Return GeoLocation from Radians
        /// </summary>
        /// <param name="latitude">The latitude, in radians.</param>
        /// <param name="longitude">The longitude, in radians.</param>
        /// <returns>GeoLocation in Radians</returns>
        public static GeoLocation FromRadians(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
                                     {
                                         radLat = latitude,
                                         radLon = longitude,
                                         degLat = Helpers.ConvertRadiansToDegrees(latitude),
                                         degLon = Helpers.ConvertRadiansToDegrees(longitude)
                                     };
            result.CheckBounds();
            return result;
        }

        private void CheckBounds()
        {
            if (radLat < MIN_LAT || radLat > MAX_LAT ||
                    radLon < MIN_LON || radLon > MAX_LON)
                throw new Exception("Arguments are out of bounds");
        }

        /**
         * @return the latitude, in degrees.
         */
        public double getLatitudeInDegrees()
        {
            return degLat;
        }

        /**
         * @return the longitude, in degrees.
         */
        public double getLongitudeInDegrees()
        {
            return degLon;
        }

        /**
         * @return the latitude, in radians.
         */
        public double getLatitudeInRadians()
        {
            return radLat;
        }

        /**
         * @return the longitude, in radians.
         */
        public double getLongitudeInRadians()
        {
            return radLon;
        }

        public override string ToString()
        {
            return "(" + degLat + "\u00B0, " + degLon + "\u00B0) = (" +
                     radLat + " rad, " + radLon + " rad)";
        }

        /// <summary>
        /// Computes the great circle distance between this GeoLocation instance and the location argument.
        /// </summary>
        /// <param name="location">Location to act as the centre point</param>
        /// <returns>the distance, measured in the same unit as the radius argument.</returns>
        public double DistanceTo(GeoLocation location)
        {
            return Math.Acos(Math.Sin(radLat) * Math.Sin(location.radLat) +
                    Math.Cos(radLat) * Math.Cos(location.radLat) *
                    Math.Cos(radLon - location.radLon)) * earthRadius;
        }

        /// <summary>
        /// Computes the bounding coordinates of all points on the surface
        /// of a sphere that have a great circle distance to the point represented
        /// by this GeoLocation instance that is less or equal to the distance
        /// argument.
        /// For more information about the formulae used in this method visit
        /// http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates
        /// </summary>
        /// <param name="distance">The distance from the point represented by this 
        /// GeoLocation instance. Must me measured in the same unit as the radius argument.
        /// </param>
        /// <returns>An array of two GeoLocation objects such that:
        /// 
        /// a) The latitude of any point within the specified distance is greater
        /// or equal to the latitude of the first array element and smaller or
        /// equal to the latitude of the second array element.
        /// If the longitude of the first array element is smaller or equal to
        /// the longitude of the second element, then
        /// the longitude of any point within the specified distance is greater
        /// or equal to the longitude of the first array element and smaller or
        /// equal to the longitude of the second array element.
        /// 
        /// b) If the longitude of the first array element is greater than the
        /// longitude of the second element (this is the case if the 180th
        /// meridian is within the distance), then
        /// the longitude of any point within the specified distance is greater
        /// or equal to the longitude of the first array element
        /// or smaller or equal to the longitude of the second
        /// array element.</returns>
        public GeoLocation[] BoundingCoordinates(double distance)
        {

            if (distance < 0d)
                throw new Exception("Distance cannot be less than 0");

            // angular distance in radians on a great circle
            double radDist = distance / earthRadius;

            double minLat = radLat - radDist;
            double maxLat = radLat + radDist;

            double minLon, maxLon;
            if (minLat > MIN_LAT && maxLat < MAX_LAT)
            {
                double deltaLon = Math.Asin(Math.Sin(radDist) /
                    Math.Cos(radLat));
                minLon = radLon - deltaLon;
                if (minLon < MIN_LON) minLon += 2d * Math.PI;
                maxLon = radLon + deltaLon;
                if (maxLon > MAX_LON) maxLon -= 2d * Math.PI;
            }
            else
            {
                // a pole is within the distance
                minLat = Math.Max(minLat, MIN_LAT);
                maxLat = Math.Min(maxLat, MAX_LAT);
                minLon = MIN_LON;
                maxLon = MAX_LON;
            }

            return new GeoLocation[]
            {
                FromRadians(minLat, minLon),
                FromRadians(maxLat, maxLon)
            };
        }
    }
}
