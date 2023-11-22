using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TRANS4D.Geometry
{
    public class Polygon
    {
        public List<(double X, double Y)> Vertices { get; private set; }
        public double MinX { get; private set; }
        public double MaxX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }
        // Future: BoundingBox for R-Tree

        public Polygon(IEnumerable<double> X, IEnumerable<double> Y)
        {
            double[] x = X.ToArray();
            double[] y = Y.ToArray();
            if (x.Length != y.Length)
                throw new ArgumentException("X and Y must have the same number of elements");
            Vertices = Enumerable.Range(0, x.Length)
                .Select(i =>
                {
                    //update bounds as we go
                    if (i == 0)
                    {
                        MinX = x[i];
                        MaxX = x[i];
                        MinY = y[i];
                        MaxY = y[i];
                    }
                    else
                    {
                        MinX = Math.Min(MinX, x[i]);
                        MaxX = Math.Max(MaxX, x[i]);
                        MinY = Math.Min(MinY, y[i]);
                        MaxY = Math.Max(MaxY, y[i]);
                    }
                    return (x[i], y[i]);
                }).ToList();
        }


        public bool ContainsPoint(double x, double y, bool includeEdge = false)
        {
            bool inside = false;
            int j = Vertices.Count - 1;

            for (int i = 0; i < Vertices.Count; i++)
            {
                var (xi, yi) = Vertices[i];
                var (xj, yj) = Vertices[j];

                //check if point is on the edge (the segment between two vertices)
                // given a point p1 and p2, chec
                if(includeEdge && (x,y).IsPointOnLineSegment(xi, yi, xj, yj))
                    return true;

                if ((yi > y) != (yj > y) &&
                    (x < (xj - xi) * (y - yi) / (yj - yi) + xi))
                {
                    inside = !inside;
                }

                j = i;
            }

            return inside;
        }

    }

    internal static class TupleExtension
    {
        public static bool IsPointOnLineSegment(this (double x, double y) point,
            double p1x, double p1y, double p2x, double p2y)
        {
            var (px, py) = point;
            
            //two approaches here if P1 and P2 are coincident
            //always return false, since it's not really a segment?
            //or say we can allow a segment of zero length, in which case
            //we check if the test point is also equal to p1 & p2
            //I opted for the latter since the goal of this function
            //is to be used for checking if a test point is on an
            //edge or corner of a polygon. This check may happen on
            //a polygon whose last point == first point so that it's properly
            //closed off.
            if (p1x == p2x && p1y == p2y)
                return  px == p1x && py == p1y;

            // Check if p is collinear with p1 and p2
            double crossProduct = (px - p1x) * (p2y - p1y) - (py - p1y) * (p2x - p1x);
            if (Math.Abs(crossProduct) > float.Epsilon) return false;  // Not collinear

            // Check if p is within the bounds of the segment
            double dotProduct = (px - p1x) * (p2x - p1x) + (py - p1y) * (p2y - p1y);
            if (dotProduct < 0) return false; // p is beyond p1

            double squaredLengthP1P2 = (p2x - p1x) * (p2x - p1x) + (p2y - p1y) * (p2y - p1y);
            if (dotProduct > squaredLengthP1P2) return false; // p is beyond p2

            return true; // p is on the segment
        }
    }
}