using TRANS4D.Geometry;

namespace TRANS4D
{
    public class GridBasedRegion : RegionBase
    {
        public GridBasedRegion(Polygon boundary): base(boundary)
        {
            
        }

        public override VelocityInfo GetVelocity(double latitude, double longitude)
        {
            throw new System.NotImplementedException();
        }
    }
}
