using TRANS4D.Geometry;

namespace TRANS4D
{
    public interface IRegion
    {
        Polygon Boundary { get; }
        
        bool ContainsPoint(double latitude, double longitude);

        VelocityInfo GetVelocity(double latitude, double longitude);
    }
}