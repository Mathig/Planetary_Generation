using System;

namespace Planetary_Generation
{
    /// <summary>
    /// Contains data for points to be used to represent tectonic plates.
    /// </summary>
    public class PlatePoint : IComparable, IPoint
    {
        /// <summary>
        /// Contains the position and can handle most positional data.
        /// </summary>
        private BasePoint _point;

        /// <summary>
        /// Represents the height of the plate.
        /// </summary>
        public double Height;

        /// <summary>
        /// Constructor for Plate Point.
        /// </summary>
        /// <param name="inX">X coordinate for point.</param>
        /// <param name="inY">Y coordinate for point.</param>
        /// <param name="inHeight">Height of Plate Point, defaults to 0.</param>
        public PlatePoint(int inX, int inY, double inHeight = 0)
        {
            _point = new BasePoint(inX, inY);
            Height = inHeight;
        }

        /// <summary>
        /// Constructor for Plate Point.
        /// </summary>
        /// <param name="inPoint"> Input coordinates for new point.</param>
        /// <param name="inHeight">Height of Plate Point, defaults to 0.</param>
        public PlatePoint(SimplePoint inPoint, double inHeight = 0)
        {
            _point = new BasePoint(inPoint);
            Height = inHeight;
        }

        /// <summary>
        /// Constructor for Plate Point.
        /// </summary>
        /// <param name="inPoint"> Input coordinates for new point.</param>
        /// <param name="inHeight">Height of Plate Point, defaults to 0.</param>
        public PlatePoint(BasePoint inPoint, double inHeight = 0)
        {
            _point = inPoint;
            Height = inHeight;
        }

        /// <summary>
        /// Opens <see cref="BasePoint.X"/> to access through X.
        /// </summary>
        public int X
        {
            get
            {
                return _point.X;
            }
        }

        /// <summary>
        /// Opens <see cref="BasePoint.Y"/> to access through Y.
        /// </summary>
        public int Y
        {
            get
            {
                return _point.Y;
            }
        }

        /// <summary>
        /// Compares two PlatePoints using the SimplePoint structure.
        /// </summary>
        /// <param name="obj">Points to be compared.</param>
        /// <returns>-1 if input is after this object, 0 if the positions are equal,
        /// and 1 if input is before this object.</returns>
        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (obj is PlatePoint otherPoint)
            {
                return this._point.CompareTo(otherPoint._point);
            }
            else
            {
                throw new ArgumentException("Object is not a PlatePoint.");
            }
        }

        /// <summary>
        /// Calculates distance between this point and the input point. Uses approximation for cosine for small angles.
        /// </summary>
        /// <param name="iPoint">Second point used for calculating distance.</param>
        /// <returns>Distance between the two points.</returns>
        public double Distance(PlatePoint inPlatePoint)
        {
            return _point.Distance(inPlatePoint._point);
        }

        /// <summary>
        /// Determines the points above and below this point, including wrap-arounds.
        /// </summary>
        /// <param name="abovePoint">The point above this point.</param>
        /// <param name="belowPoint">The point below this point.</param>
        public void FindAboveBelowPoints(out SimplePoint abovePoint, out SimplePoint belowPoint)
        {
            _point.FindAboveBelowPoints(out abovePoint, out belowPoint);
        }

        /// <summary>
        /// Determines the points left and right of this point, including wrap-arounds.
        /// </summary>
        /// <param name="leftPoint">The point to the left of this point.</param>
        /// <param name="rightPoint">The point to the right of this point.</param>
        public void FindLeftRightPoints(out SimplePoint leftPoint, out SimplePoint rightPoint)
        {
            _point.FindLeftRightPoints(out leftPoint, out rightPoint);
        }

        /// <summary>
        /// Calculates boundaries for rectangular approximation of points in range of this point.
        /// </summary>
        /// <param name="range">Distance from center point.</param>
        /// <param name="xMin">Minimum X boundary, adding 2*<see cref="_mapSize"/> if range wraps around border.</param>
        /// <param name="xMax">Maximum X boundary, adding 2*<see cref="_mapSize"/> if range wraps around border.</param>
        /// <param name="yMin">Minimum Y boundary.</param>
        /// <param name="yMax">Maximum Y boundary.</param>
        public void Range(double range, out int xMin, out int xMax, out int yMin, out int yMax)
        {
            _point.Range(range, out xMin, out xMax, out yMin, out yMax);
        }

        /// <summary>
        /// Tests the momentum and returns true if the momentum exceeds the area corrected threshold.
        /// </summary>
        /// <param name="momentum">Momentum to test.</param>
        /// <param name="threshold">Uncorrected threshold to test for.</param>
        /// <returns>True if test succeeds, false otherwise.</returns>
        public bool TestMomentum(double momentum, double threshold)
        {
            return _point.TestMomentum(momentum, threshold);
        }

        /// <summary>
        /// Calculates new position of point for a given rotation.
        /// </summary>
        /// <param name="angle">Three dimensional angle for rotation, given in radians.</param>
        /// <returns>New position given as a Simple Point.</returns>
        public SimplePoint Transform(double[] angle)
        {
            return _point.Transform(angle);
        }
    }
}