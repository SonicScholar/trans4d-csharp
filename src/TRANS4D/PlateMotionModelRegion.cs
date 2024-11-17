using System;
using TRANS4D.Geometry;

namespace TRANS4D
{
    public class PlateMotionModelRegion : RegionBase
    {
        public PlateMotionModelRegion(Polygon boundary) 
            : base(boundary)
        {
        }

        public override VelocityInfo GetVelocity(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}