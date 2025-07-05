namespace TRANS4D
{
    public class VelocityInfo
    {
        public double VelocityNorth { get; set; }
        public double VelocityEast { get; set; }
        public double VelocityUp { get; set; }

        public double SigmaVelocityNorth { get; set; }
        public double SigmaVelocityEast { get; set; }
        public double SigmaVelocityUp { get; set; }

        public VelocityInfo() { }

        public VelocityInfo(double velocityNorth, double velocityEast, double velocityUp)
        {
            VelocityNorth = velocityNorth;
            VelocityEast = velocityEast;
            VelocityUp = velocityUp;
        }

        public VelocityInfo(double velocityNorth, double velocityEast, double velocityUp,
            double sigmaVelocityNorth, double sigmaVelocityEast, double sigmaVelocityUp) : this(velocityNorth,
            velocityEast, velocityUp)
        {
            SigmaVelocityNorth = sigmaVelocityNorth;
            SigmaVelocityEast = sigmaVelocityEast;
            SigmaVelocityUp = sigmaVelocityUp;
        }
    }
}
