using TRANS4D.Geometry;

namespace TRANS4D
{
    public abstract class RegionBase: IRegion
    {
        protected RegionBase(Polygon polygon)
        {
            Polygon = polygon;
        }

        public Polygon Polygon { get; }

        public bool ContainsPoint(double latitude, double longitude)
        {
            Polygon.ContainsPoint(longitude, latitude);
        }

        public abstract VelocityInfo GetVelocity(double latitude, double longitude);
    }
}