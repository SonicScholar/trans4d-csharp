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

        public override VelocityInfo GetVelocity(GeodeticCoordinates coordinates)
        {
            throw new NotImplementedException();
        }
    }
}