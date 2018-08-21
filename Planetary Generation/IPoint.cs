namespace Planetary_Generation
{
    /// <summary>
    /// Interface to allow points to access the X and Y position and find left, right, above, and below points.
    /// </summary>
    internal interface IPoint
    {
        /// <summary>
        /// X value of point, for array index.
        /// </summary>
        int X { get; }

        /// <summary>
        /// Y value of point, for array index.
        /// </summary>
        int Y { get; }

        /// <summary>
        /// Determines the points above and below this point, including wrap-arounds.
        /// </summary>
        /// <param name="abovePoint">The point above this point.</param>
        /// <param name="belowPoint">The point below this point.</param>
        void FindAboveBelowPoints(out SimplePoint abovePoint, out SimplePoint belowPoint);

        /// <summary>
        /// Determines the points left and right of this point, including wrap-arounds.
        /// </summary>
        /// <param name="leftPoint">The point to the left of this point.</param>
        /// <param name="rightPoint">The point to the right of this point.</param>
        void FindLeftRightPoints(out SimplePoint leftPoint, out SimplePoint rightPoint);
    }
}