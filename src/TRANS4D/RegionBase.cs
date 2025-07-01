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


        public bool ContainsPoint(GeodeticCoordinates coordinates) => 
            Boundary.ContainsPoint(coordinates.Longitude, coordinates.Latitude);

        public abstract VelocityInfo GetVelocity(GeodeticCoordinates coordinates);
    }
}