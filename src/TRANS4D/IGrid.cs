using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    internal interface IGrid
    {
        object GetGridValue(double x, double y);
    }

    internal interface IGrid<T> : IGrid 
    {
        T GetGridValue(double x, double y);
    }
}
