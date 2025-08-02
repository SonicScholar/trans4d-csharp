using TRANS4D.Geometry;

namespace TRANS4D
{
    public interface IVelocityModelRegion
    {
        Polygon Boundary { get; }
        
        bool ContainsPoint(GeodeticCoordinates coordinates);

        VelocityInfo GetVelocity(GeodeticCoordinates coordinates);
    }
}