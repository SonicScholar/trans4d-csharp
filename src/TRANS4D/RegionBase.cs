using TRANS4D.Geometry;

namespace TRANS4D
{
    public abstract class RegionBase: IRegion
    {
        protected RegionBase(Polygon boundary)
        {
            Boundary = boundary;
        }

        public Polygon Boundary { get; }


        public bool ContainsPoint(GeodeticCoordinates coordinates)
        {
            var normalizedCoordinates = coordinates.Normalize();
            return Boundary.ContainsPoint(normalizedCoordinates.LongitudeDegrees, normalizedCoordinates.LatitudeDegrees);
        }

        public abstract VelocityInfo GetVelocity(GeodeticCoordinates coordinates);
    }
}