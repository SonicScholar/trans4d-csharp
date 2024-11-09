using System;

namespace TRANS4D
{
    public class GeodeticCoordinates
    {
        public static GeodeticCoordinates Invalid => new GeodeticCoordinates(double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Latitude in radians
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude in radians
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Height above the ellipsoid in meters
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets the latitude in decimal degrees.
        /// </summary>
        public double LatitudeDegrees => Latitude * 180.0 / Math.PI;

        /// <summary>
        /// Gets the longitude in decimal degrees.
        /// </summary>
        public double LongitudeDegrees => Longitude * 180.0 / Math.PI;

        public GeodeticCoordinates(){ }

        /// <summary>
        /// Create a set of ellipsoidal coordinates with the given latitude, longitude, and height
        /// above the ellipsoid.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="height"></param>
        public GeodeticCoordinates(double latitude, double longitude, double height)
        {
            Latitude = latitude;
            Longitude = longitude;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            if (obj is GeodeticCoordinates other)
            {
                return Latitude == other.Latitude &&
                       Longitude == other.Longitude &&
                       Height == other.Height;
            }

            return false;
        }

    }

}
