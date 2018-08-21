using System;

namespace Planetary_Generation
{
    /// <summary>
    /// Contains SimplePoint plus extra variables for calculating distances, sizes, and rotations.
    /// </summary>
    public struct BasePoint : IComparable, IPoint
    {
        /// <summary>
        /// Angular width and height of point in radians.
        /// </summary>
        private static double _angleSize;

        /// <summary>
        /// Number of points per height of map, twice this number for length of map.
        /// </summary>
        private static int _mapSize;

        /// <summary>
        /// Constant for converting between array index (YCoord) and phi.
        /// </summary>
        private static double _phiShift;

        /// <summary>
        /// Cosine of angular position of point. Phi ranges from -pi/2 to pi/2.
        /// </summary>
        private readonly double _cosPhi;

        /// <summary>
        /// Location of point.
        /// </summary>
        private readonly SimplePoint _position;

        /// <summary>
        /// Sine of angular position of point. Phi ranges from -pi/2 to pi/2.
        /// </summary>
        private readonly double _sinPhi;

        /// <summary>
        /// Angular position of point, ranges from 0 to 2 pi.
        /// </summary>
        private readonly double _theta;

        /// <summary>
        /// Constructor for the Base Point. Takes in X and Y coordinates.
        /// </summary>
        /// <param name="inX">X coordinate for point.</param>
        /// <param name="inY">Y coordinate for point.</param>
        public BasePoint(int inX, int inY)
        {
            _position = new SimplePoint(inX, inY);
            _theta = _position.X * _angleSize;
            _cosPhi = Math.Cos((_position.Y * _angleSize) + _phiShift);
            _sinPhi = Math.Sin((_position.Y * _angleSize) + _phiShift);
        }

        /// <summary>
        /// Constructor for the Base Point. Takes in a Simple Point structure.
        /// </summary>
        /// <param name="inPoint">Input Point.</param>
        public BasePoint(SimplePoint inPoint)
        {
            _position = inPoint;
            _theta = _position.X * _angleSize;
            _cosPhi = Math.Cos((_position.Y * _angleSize) + _phiShift);
            _sinPhi = Math.Sin((_position.Y * _angleSize) + _phiShift);
        }

        /// <summary>
        /// Opens <see cref="SimplePoint.X"/> to access through X.
        /// </summary>
        public int X
        {
            get
            {
                return _position.X;
            }
        }

        /// <summary>
        /// Opens <see cref="SimplePoint.Y"/> to access through Y.
        /// </summary>
        public int Y
        {
            get
            {
                return _position.Y;
            }
        }

        /// <summary>
        /// Rotates a three dimensional point about z axis.
        /// </summary>
        /// <param name="xIn">Effective X Axis for rotation.</param>
        /// <param name="yIn">Effective Y Axis for rotation.</param>
        /// <param name="zIn">Effective Z Axis for rotation.</param>
        /// <param name="cosAngle">Cosine of angle to rotate by.</param>
        /// <param name="sinAngle">Sine of angle to rotate by.</param>
        /// <returns>Position of rotated point.</returns>
        private static double[] AxisRotation(double xIn, double yIn, double zIn, double cosAngle, double sinAngle)
        {
            double[] rotatedPoint = new double[3];
            rotatedPoint[0] = xIn * cosAngle - yIn * sinAngle;
            rotatedPoint[1] = xIn * sinAngle + yIn * cosAngle;
            rotatedPoint[2] = zIn;
            return rotatedPoint;
        }

        /// <summary>
        /// Rotates a point about 3 axis in 3D Cartesian Coordinates.
        /// </summary>
        /// <param name="pointIn">Point to be rotated.</param>
        /// <param name="angle">Amount to rotate about axis.</param>
        /// <returns>Point rotated about 3 axis.</returns>
        private static double[] CartesianRotation(double[] pointIn, double[] angle)
        {
            double[] pointOut = new double[3];
            double cosAngleX = Math.Cos(angle[0]);
            double sinAngleX = Math.Sin(angle[0]);
            double cosAngleY = Math.Cos(angle[1]);
            double sinAngleY = Math.Sin(angle[1]);

            pointOut = AxisRotation(pointOut[1], pointOut[2], pointOut[0], cosAngleX, sinAngleX);
            pointOut = AxisRotation(pointOut[0], pointOut[2], pointOut[1], cosAngleY, sinAngleY);
            pointOut = AxisRotation(pointOut[0], pointOut[1], pointOut[2], Math.Cos(angle[2]), Math.Sin(angle[2]));
            pointOut = AxisRotation(pointOut[0], pointOut[2], pointOut[1], cosAngleY, -1 * sinAngleY);
            pointOut = AxisRotation(pointOut[1], pointOut[2], pointOut[0], cosAngleX, -1 * sinAngleX);

            return pointOut;
        }

        /// <summary>
        /// Rotates a point about 3 axis in 3D Spherical Coordinates.
        /// </summary>
        /// <param name="thetaIn">Input theta angle.</param>
        /// <param name="cosPhiIn">Cosine of input phi angle.</param>
        /// <param name="sinPhiIn">Sine of input phi angle.</param>
        /// <param name="angle">Angle vector to be rotated.</param>
        /// <returns>Theta and phi of rotated angle. Theta (0,2Pi], Phi (-Pi/2,Pi/2].</returns>
        private static double[] SphericalRotation(double thetaIn, double cosPhiIn, double sinPhiIn, double[] angle)
        {
            double[] pointCartesian = new double[3];

            pointCartesian[0] = Math.Cos(thetaIn) * cosPhiIn;
            pointCartesian[1] = Math.Sin(thetaIn) * cosPhiIn;
            pointCartesian[2] = sinPhiIn;

            pointCartesian = CartesianRotation(pointCartesian, angle);

            double[] pointSpherical = new double[2];

            pointSpherical[0] = (Math.Atan2(-1 * pointCartesian[1], -1 * pointCartesian[0]) + Math.PI);
            pointSpherical[1] = Math.Asin(pointCartesian[2]);
            return pointSpherical;
        }

        /// <summary>
        /// Sets static variables for Base Point and Simple Point.
        /// </summary>
        /// <param name="inMapSize">Map Size to set up points.</param>
        public static void MapSetup(int inMapSize)
        {
            SimplePoint.MapSize(inMapSize);
            _mapSize = inMapSize;
            _angleSize = Math.PI / (double)_mapSize;
            _phiShift = 0.5 * _angleSize - (Math.PI / 2);
        }

        /// <summary>
        /// Compares two BasePoints using the SimplePoint structure.
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
            if (obj is BasePoint otherPoint)
            {
                return this._position.CompareTo(otherPoint._position);
            }
            else
            {
                throw new ArgumentException("Object is not a BasePoint.");
            }
        }

        /// <summary>
        /// Calculates distance between this point and the input point. Uses approximation for cosine for small angles.
        /// </summary>
        /// <param name="iPoint">Second point used for calculating distance.</param>
        /// <returns>Distance between the two points.</returns>
        public double Distance(BasePoint iPoint)
        {
            int xDif = Math.Abs(iPoint.X - this.X);
            if (xDif > _mapSize)
            {
                xDif = 2 * _mapSize - xDif;
            }
            double cosDif;
            if (xDif < _mapSize / 6)
            {
                cosDif = 1 - (0.5 * xDif * xDif * _angleSize * _angleSize);
            }
            else
            {
                cosDif = Math.Cos((double)xDif * _angleSize);
            }
            return Math.Abs(2 * (1 - _sinPhi * iPoint._sinPhi - _cosPhi * iPoint._cosPhi * cosDif));
        }

        /// <summary>
        /// Determines the points above and below this point, including wrap-arounds.
        /// </summary>
        /// <param name="abovePoint">The point above this point.</param>
        /// <param name="belowPoint">The point below this point.</param>
        public void FindAboveBelowPoints(out SimplePoint abovePoint, out SimplePoint belowPoint)
        {
            _position.FindAboveBelowPoints(out abovePoint, out belowPoint);
        }

        /// <summary>
        /// Determines the points left and right of this point, including wrap-arounds.
        /// </summary>
        /// <param name="leftPoint">The point to the left of this point.</param>
        /// <param name="rightPoint">The point to the right of this point.</param>
        public void FindLeftRightPoints(out SimplePoint leftPoint, out SimplePoint rightPoint)
        {
            _position.FindLeftRightPoints(out leftPoint, out rightPoint);
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
            double zMin = ((_sinPhi - range) + 1) / 2;
            if (zMin < 0)
            {
                zMin = 0;
            }
            double zMax = ((_sinPhi + range) + 1) / 2;
            if (zMax > 1)
            {
                zMax = 1;
            }
            double rDif = range / _cosPhi;
            if (rDif > 1)
            {
                rDif = 1;
            }
            xMin = X - (int)(rDif * (double)_mapSize);
            xMax = X + (int)(rDif * (double)_mapSize);
            if (xMin <= 0)
            {
                xMin += 2 * _mapSize;
                xMax += 2 * _mapSize;
            }
            yMin = (int)Math.Floor(zMin * (double)_mapSize);
            yMax = (int)Math.Ceiling(zMax * (double)_mapSize);
        }

        /// <summary>
        /// Tests the momentum and returns true if the momentum exceeds the area corrected threshold.
        /// </summary>
        /// <param name="momentum">Momentum to test.</param>
        /// <param name="threshold">Uncorrected threshold to test for.</param>
        /// <returns>True if test succeeds, false otherwise.</returns>
        public bool TestMomentum(double momentum, double threshold)
        {
            if (momentum > Math.Pow(threshold, _cosPhi))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates new position of point for a given rotation.
        /// </summary>
        /// <param name="angle">Three dimensional angle for rotation, given in radians.</param>
        /// <returns>New position given as a Simple Point.</returns>
        public SimplePoint Transform(double[] angle)
        {
            double[] doubleOutput = SphericalRotation(_theta, _cosPhi, _sinPhi, angle);

            int xCoord = (int)(Math.Round(doubleOutput[0] / _angleSize));
            if (xCoord == 2 * _mapSize)
            {
                xCoord = 0;
            }
            int yCoord = (int)(Math.Round((doubleOutput[1] - _phiShift) / _angleSize));
            if (yCoord == _mapSize)
            {
                yCoord = 0;
            }
            return new SimplePoint(xCoord, yCoord);
        }
    }
}