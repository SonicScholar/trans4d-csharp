namespace TRANS4D
{
    public class VelocityInfo
    {
        public double NorthVelocity { get; set; }
        public double EastVelocity { get; set; }
        public double UpwardVelocity { get; set; }

        public double SigmaNorthVelocity { get; set; }
        public double SigmaEastVelocity { get; set; }
        public double SigmaUpwardVelocity { get; set; }

        public VelocityInfo(double northVelocity, double eastVelocity, double upwardVelocity)
        {
            NorthVelocity = northVelocity;
            EastVelocity = eastVelocity;
            UpwardVelocity = upwardVelocity;
        }
    }
}
