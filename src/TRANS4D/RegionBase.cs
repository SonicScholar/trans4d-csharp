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


        public bool ContainsPoint(double latitude, double longitude)
        {
            return Boundary.ContainsPoint(longitude, latitude);
        }

        public abstract VelocityInfo GetVelocity(double latitude, double longitude);
    }
}