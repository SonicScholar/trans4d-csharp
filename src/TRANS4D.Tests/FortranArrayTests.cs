using TRANS4D.Compatibility;

namespace TRANS4D.Tests
{
    public class FortranArrayTests
    {
        [Fact]
        public void CreateEmptyArray_Succeeds()
        {
            FortranArray<int> array = new FortranArray<int>(0);
            Assert.Equal(0, array.Length);
        }

        [Fact]
        public void CreateArray_Succeeds()
        {
            FortranArray<int> array = new FortranArray<int>(10);
            Assert.Equal(10, array.Length);
        }

        [Fact]
        public void AccessArray_Succeeds()
        {
            FortranArray<int> array = new FortranArray<int>(10);
            array[1] = 1;
            array[2] = 2;
            array[3] = 3;
            Assert.Equal(1, array[1]);
            Assert.Equal(2, array[2]);
            Assert.Equal(3, array[3]);
        }

        [Fact]
        public void AccessArray_OutOfRange_Throws()
        {
            FortranArray<int> array = new FortranArray<int>(10);
            Assert.Throws<IndexOutOfRangeException>(() => array[0]);
            Assert.Throws<IndexOutOfRangeException>(() => array[11]);
        }
    }
}