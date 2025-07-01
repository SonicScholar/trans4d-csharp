using TRANS4D.Geometry;

namespace TRANS4D
{
    public interface IRegion
    {
        Polygon Boundary { get; }
        
        bool ContainsPoint(GeodeticCoordinates coordinates);

        VelocityInfo GetVelocity(GeodeticCoordinates coordinates);
    }
}