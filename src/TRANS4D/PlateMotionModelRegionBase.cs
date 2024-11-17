using System;
using TRANS4D.Geometry;

namespace TRANS4D
{
    public class PlateMotionModelRegionBase : RegionBase
    {
        public PlateMotionModelRegionBase(Polygon polygon) 
            : base(polygon)
        {
        }

        public override VelocityInfo GetVelocity(double latitude, double longitude)
        {
            throw new NotImplementedException();
        }
    }
}