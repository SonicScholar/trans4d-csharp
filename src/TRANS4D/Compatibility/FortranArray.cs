﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRANS4D.Compatibility
{
    /// <summary>
    /// This class is a wrapper around a standard array for use
    /// with code ported from Fortran.  It provides a 1-based
    /// indexing scheme.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FortranArray<T>// : IEnumerable<T>
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
            set => _array[index - 1] = value;
        }

        public int Length => _array.Length;
    }

    public static class FortranArrayHelper
    {
        public static FortranArray<T> ToFortranArray<T>(this IEnumerable<T> source)
        {
            var sourceList = source.ToList();
            var array = new FortranArray<T>(sourceList.Count);
            var i = 1;
            foreach (var item in source)
            {
                array[i] = item;
                i++;
            }
            return array;
        }
    }
}
