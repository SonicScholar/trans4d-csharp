using System;

namespace TRANS4D
{

    public class EulerPoleRegion
    {
        public string Name { get; set; }
        public double Wx { get; set; }
        public double Wy { get; set; }
        public double Wz { get; set; }

        private EulerPoleRegion(double wx, double wy, double wz)
            : this(wx, wy, wz, string.Empty)
        {
        }

        public EulerPoleRegion(double wx, double wy, double wz, string name)
        {
            Name = name;
            Wx = wx;
            Wy = wy;
            Wz = wz;
        }

        // Static instances representing named plates
        /// <summary>
        /// North America Plate (Ding et al. 2019, JGR-Solid Earth)
        /// </summary>
        public static EulerPoleRegion NorthAmerica = new EulerPoleRegion(
            0.2668e-9, -3.3677e-9, -0.2956e-9, nameof(NorthAmerica));

        /// <summary>
        /// Caribbean Plate (Snay & Saleh, in preparation)
        /// </summary>
        public static EulerPoleRegion Caribbean = new EulerPoleRegion(
            -0.188e-9, -4.73e-9, 2.963e-9, nameof(Caribbean));

        /// <summary>
        /// Pacific Plate (Altamimi et al. 2017, JGR-Solid Earth)
        /// </summary>
        public static EulerPoleRegion Pacific = new EulerPoleRegion(
            -1.983e-9, 5.076e-9, -10.516e-9, nameof(Pacific));

        /// <summary>
        /// Juan de Fuca Plate (DeMets et al. 2010, Geophys. J. Intl, vol 181)
        /// </summary>
        public static EulerPoleRegion JuanDeFuca = new EulerPoleRegion(
            6.636e-9, 11.761e-9, -10.63e-9, nameof(JuanDeFuca));

        /// <summary>
        /// Cocos Plate (DeMets et al. 2010, Geophys. J. Intl, vol. 181)
        /// </summary>
        public static EulerPoleRegion Cocos = new EulerPoleRegion(
            -10.38e-9, -14.901e-9, 9.133e-9, nameof(Cocos));

        /// <summary>
        /// Mariana Plate (Snay, 2003, SALIS, Vol 63)
        /// </summary>
        /// todo: re-calculate values so we don't need special values for this?
        /// todo: maybe a derived class is needed to handle this case
        public static EulerPoleRegion Mariana = new EulerPoleRegion(
            -0.097e-9, 0.509e-9, -1.682e-9, nameof(Mariana));

        /// <summary>
        /// Philippine Sea Plate (Kreemer et al. 2014, Geochem. Geophys. Geosyst., vol. 15)
        /// </summary>
        public static EulerPoleRegion PhilippineSea = new EulerPoleRegion(
            9.221e-9, -4.963e-9, -11.554e-9, nameof(PhilippineSea));

        /// <summary>
        /// South America Plate (Altamimi et al. 2017, JGR-Solid Earth)
        /// </summary>
        public static EulerPoleRegion SouthAmerica = new EulerPoleRegion(
            -1.309e-9, -1.459e-9, -0.679e-9, nameof(SouthAmerica));

        /// <summary>
        /// Nazca Plate (Altamimi et al. 2017, JGR-Solid Earth)
        /// </summary>
        public static EulerPoleRegion Nazca = new EulerPoleRegion(
            -1.614e-9, -7.486e-9, 7.868e-9, nameof(Nazca));

        /// <summary>
        /// Panama Plate (Kreemer et al. 2014, Geochem. Geophys. Geosyst., vol 15)
        /// </summary>
        public static EulerPoleRegion Panama = new EulerPoleRegion(
            2.088e-9, -23.037e-9, 6.729e-9, nameof(Panama));

        /// <summary>
        /// North Andes Plate (Mora-Paez, 2018, J. South Amer. Earth Science)
        /// </summary>
        public static EulerPoleRegion NorthAndes = new EulerPoleRegion(
            -1.964e-9, -1.518e-9, 0.4e-9, nameof(NorthAndes));

        public (double vx, double vy, double vz) ComputeVelocity(double x, double y, double z)
        {
            // Compute velocities
            double vx = -Wz * y + Wy * z;
            double vy = Wz * x - Wx * z;
            double vz = -Wy * x + Wx * y;

            // The parameters--WX, WY, and WZ--refer to ITRF2000
            // for the Mariana Plate(Snay, 2003).Hence,
            // for this plate, VX, VY, and VZ, correspond to ITRF2000.
            // The following code converts these to ITRF2014 velocities for
            //  this plate.
            //if (this == EulerPoleRegion.Mariana)
            //{
            //    vx *= 1000.0;
            //    vy *= 1000.0;
            //    vz *= 1000.0;

                        //    // Call to VTRANF would go here - simulated with a placeholder for now
                        //    VTRANF(x, y, z, ref vx, ref vy, ref vz);

                        //    vx /= 1000.0;
                        //    vy /= 1000.0;
                        //    vz /= 1000.0;
                        //}

            return (vx, vy, vz);
        }

        private static void VTRANF(double x, double y, double z, ref double vx, ref double vy, ref double vz)
        {
            // todo: Placeholder for the VTRANF operation
            // In actual implementation, this function would perform some transformation
            throw new NotImplementedException();
        }
    }
}