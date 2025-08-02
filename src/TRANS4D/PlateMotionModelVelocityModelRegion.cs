using System;
using TRANS4D.Geometry;

namespace TRANS4D
{
    public class PlateMotionModelVelocityModelRegion : VelocityModelRegionBase
    {
        public PlateMotionModelVelocityModelRegion(Polygon boundary) 
            : base(boundary)
        {
        }

        public override VelocityInfo GetVelocity(GeodeticCoordinates coordinates)
        {
            throw new NotImplementedException();
        }
    }
}