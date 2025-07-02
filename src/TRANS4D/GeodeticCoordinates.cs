using System;

namespace TRANS4D
{
    /// <summary>
    /// Represents a set of geodetic (ellipsoidal) coordinates: latitude, longitude, and height above the ellipsoid.
    /// </summary>
    public class GeodeticCoordinates
    {
        /// <summary>
        /// Gets an invalid set of geodetic coordinates (all values are NaN).
        /// </summary>
        public static GeodeticCoordinates Invalid => new GeodeticCoordinates(double.NaN, double.NaN, double.NaN);

        /// <summary>
        /// Creates a <see cref="GeodeticCoordinates"/> instance from latitude and longitude in decimal degrees and height.
        /// </summary>
        /// <param name="latitude">Latitude in decimal degrees.</param>
        /// <param name="longitude">Longitude in decimal degrees.</param>
        /// <param name="height">Height above the ellipsoid.</param>
        /// <returns>A new <see cref="GeodeticCoordinates"/> instance.</returns>
        public static GeodeticCoordinates FromDegrees(double latitude, double longitude, double height)
        {
            double latRad = latitude * Math.PI / 180.0;
            double lonRad = longitude * Math.PI / 180.0;
            return new GeodeticCoordinates(latRad, lonRad, height);
        }

        /// <summary>
        /// Creates a <see cref="GeodeticCoordinates"/> instance from latitude and longitude in radians and height.
        /// </summary>
        /// <param name="latitude">Latitude in radians.</param>
        /// <param name="longitude">Longitude in radians.</param>
        /// <param name="height">Height above the ellipsoid.</param>
        /// <returns>A new <see cref="GeodeticCoordinates"/> instance.</returns>
        public static GeodeticCoordinates FromRadians(double latitude, double longitude, double height)
        {
            return new GeodeticCoordinates(latitude, longitude, height);
        }

        /// <summary>
        /// Latitude in radians.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude in radians.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Height above the ellipsoid in meters.
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

        /// <summary>
        /// Returns a new GeodeticCoordinates with longitude normalized to [0, 2pi).
        /// </summary>
        public GeodeticCoordinates Normalize()
        {
            return new GeodeticCoordinates(
                this.Latitude,
                this.Longitude.NormalizeRadians(),
                this.Height
            );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeodeticCoordinates"/> class.
        /// Protected to enforce use of factory methods.
        /// </summary>
        protected GeodeticCoordinates() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeodeticCoordinates"/> class with the given latitude and longitude (in radians), and height.
        /// Protected to enforce use of factory methods.
        /// </summary>
        /// <param name="latitude">Latitude in radians.</param>
        /// <param name="longitude">Longitude in radians.</param>
        /// <param name="height">Height above the ellipsoid in meters.</param>
        protected GeodeticCoordinates(double latitude, double longitude, double height)
        {
            Latitude = latitude;
            Longitude = longitude;
            Height = height;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="GeodeticCoordinates"/> instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the specified object is equal to the current instance; otherwise, false.</returns>
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
