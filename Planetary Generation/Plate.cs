using System.Collections.Generic;

namespace Planetary_Generation
{
    /// <summary>
    /// Contains data and functions for plates.
    /// </summary>
    public class Plate
    {
        /// <summary>
        /// Two dimensional vector for rotation direction, in radians.
        /// </summary>
        public double[] Direction;

        /// <summary>
        /// Collection of points within this plate.
        /// </summary>
        public List<PlatePoint> PlatePoints;

        /// <summary>
        /// Magnitude of rotation per time, in radians per time unit.
        /// </summary>
        public double Speed;

        /// <summary>
        /// Allocates space for <see cref="Direction"/>
        /// </summary>
        public Plate()
        {
            Direction = new double[2];
            PlatePoints = new List<PlatePoint>();
        }

        /// <summary>
        /// Copies input plate points to this plate.
        /// </summary>
        /// <param name="cPlate">Input plate.</param>
        public void CopyPoints(List<PlatePoint> newPoints)
        {
            PlatePoints.Clear();
            for (int i = 0; i < newPoints.Count; i++)
            {
                PlatePoints.Add(new PlatePoint(newPoints[i].X, newPoints[i].Y, newPoints[i].Height));
            }
        }

        /// <summary>
        /// Transforms each point in plate, see <see cref="Point.Transform"/>.
        /// </summary>
        /// <param name="timeStep">Scaling factor for how much to rotate.</param>
        public void Slide(double timeStep)
        {
            for (int i = 0; i < PlatePoints.Count; i++)
            {
                double[] angle = new double[3] { Direction[0], Direction[1], timeStep * Speed };
                PlatePoints[i] = new PlatePoint(PlatePoints[i].Transform(angle), PlatePoints[i].Height);
            }
        }
    }
}