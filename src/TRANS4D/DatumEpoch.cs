using System;

namespace TRANS4D
{
    /// <summary>
    /// Represents a geodetic reference frame (datum) at a specific epoch (date).
    /// </summary>
    public class DatumEpoch
    {
        public Datum Datum { get; }
        public DateTime Epoch { get; }

        public DatumEpoch(Datum datum, DateTime epoch)
        {
            Datum = datum;
            Epoch = epoch;
        }

        public override bool Equals(object obj)
        {
            if (obj is DatumEpoch other)
            {
                return Datum == other.Datum && Epoch == other.Epoch;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Datum.GetHashCode() ^ Epoch.GetHashCode();
        }
    }
}
