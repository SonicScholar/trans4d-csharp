using System.IO;

namespace TRANS4D
{
    public struct GridRecord
    {
        public int padding1;
        public double VN;
        public double SN;
        public double VE;
        public double SE;
        public double VU;
        public double SU;
        public int padding2;

        // Reads a GridRecord from a FileStream (FORTRAN unformatted sequential binary)
        // Returns the total number of bytes read
        public uint ReadRecordFromFile(FileStream file)
        {
            int bytesRead = 0;
            using (var reader = new BinaryReader(file, System.Text.Encoding.Default, leaveOpen: true))
            {
                padding1 = reader.ReadInt32(); bytesRead += sizeof(int);
                VN = reader.ReadDouble(); bytesRead += sizeof(double);
                SN = reader.ReadDouble(); bytesRead += sizeof(double);
                VE = reader.ReadDouble(); bytesRead += sizeof(double);
                SE = reader.ReadDouble(); bytesRead += sizeof(double);
                VU = reader.ReadDouble(); bytesRead += sizeof(double);
                SU = reader.ReadDouble(); bytesRead += sizeof(double);
                padding2 = reader.ReadInt32(); bytesRead += sizeof(int);
            }
            return (uint)bytesRead;
        }
    }

}