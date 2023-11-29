using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    // this will be for reading the grid files containing the velocity data
    // (also contains standard deviations, I think. Have to double check with
    // Rich or check the original FORTRAN code)
    public class Grid : IGrid<double>
    {
        //todo: grid boundaries
        //todo: grid spacing

        public double GetGridValue(double x, double y)
        {
            throw new NotImplementedException();
        }

        object IGrid.GetGridValue(double x, double y) => GetGridValue(x, y);
    }
}
