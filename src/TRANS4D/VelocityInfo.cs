namespace TRANS4D
{
    public class VelocityInfo
    {
        public int? DeformationRegion { get; set; }
        public double NorthVelocity { get; set; }
        public double EastVelocity { get; set; }
        public double UpwardVelocity { get; set; }

        public VelocityInfo(int? deformationRegion, double northVelocity, double eastVelocity, double upwardVelocity)
        {
            DeformationRegion = deformationRegion;
            NorthVelocity = northVelocity;
            EastVelocity = eastVelocity;
            UpwardVelocity = upwardVelocity;
        }
    }
}
