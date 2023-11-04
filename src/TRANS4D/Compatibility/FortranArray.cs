using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D.Compatibility
{
    /// <summary>
    /// This class is a wrapper around a standard array for use
    /// with code ported from Fortran.  It provides a 1-based
    /// indexing scheme.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FortranArray<T>// : IEnumerable<T>
    {
        private readonly T[] _array;

        public FortranArray(int size)
        {
            _array = new T[size];
        }

        public T this[int index]
        {
            get
            {
                if (index < 1 || index > _array.Length)
                    throw new IndexOutOfRangeException();
                return _array[index - 1];
            }
            set
            {
                _array[index - 1] = value;
            }
        }
    }
}
