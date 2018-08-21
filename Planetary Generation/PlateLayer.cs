using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planetary_Generation
{
    /// <summary>
    /// Contains data and methods for managing the entire map.
    /// </summary>
    public class PlateLayer
    {
        /// <summary>
        /// Number of points per height of map, twice this number for length of map.
        /// </summary>
        private static int _layerSize;

        /// <summary>
        /// List of active points, used for processing points.
        /// </summary>
        private readonly bool[,] ActivePoints;

        /// <summary>
        /// Contains all the plates on the map, independently from <see cref="Map"/>.
        /// </summary>
        private readonly Plate[] Plates;

        /// <summary>
        /// Contains magnitudes of points, for the purposes of generating new plates.
        /// </summary>
        private readonly double[,] PointMagnitudes;

        /// <summary>
        /// Contains all points in an x-y grid for use in calculations.
        /// </summary>
        private readonly BasePoint[,] PointMap;

        /// <summary>
        /// Extra plate to add potential new plates.
        /// </summary>
        private readonly Plate temporaryPlate;

        /// <summary>
        /// Contains heights of points before movement.
        /// </summary>
        public readonly double[,] PastHeights;

        /// <summary>
        /// Contains which points were previously part of which plates.
        /// </summary>
        public readonly int[,] PastPlates;

        /// <summary>
        /// Allocates space for arrays and sets up the point class.
        /// </summary>
        /// <param name="iSize">Size of the map, see <see cref="_layerSize"/></param>
        /// <param name="iNumberPlates">Number of plates in <see cref="Plates"/></param>
        public PlateLayer(int iSize, int iNumberPlates)
        {
            _layerSize = iSize;
            BasePoint.MapSetup(iSize);
            Plates = new Plate[iNumberPlates];
            for (int i = 0; i < Plates.Length; i++)
            {
                Plates[i] = new Plate();
            }
            temporaryPlate = new Plate();
            ActivePoints = new bool[2 * _layerSize, _layerSize];
            PastPlates = new int[2 * _layerSize, _layerSize];
            PastHeights = new double[2 * _layerSize, _layerSize];
            PointMagnitudes = new double[2 * _layerSize, _layerSize];
            PointMap = new BasePoint[2 * _layerSize, _layerSize];
            for (int x = 0; x < 2 * _layerSize; x++)
            {
                for (int y = 0; y < _layerSize; y++)
                {
                    PointMap[x, y] = new BasePoint(x, y);
                }
            }
        }

        /// <summary>
        /// Calculates the average height of adjacent points in <see cref="PastHeights"/>.
        /// </summary>
        /// <param name="iPlate">Index of plate to scan for with <see cref="PastPlates"/>.</param>
        /// <param name="inPoint">Input point.</param>
        /// <returns>Average of nearby points from <see cref="PastHeights"/>.</returns>
        private double AdjacentHeightAverage(int inPlate, SimplePoint inPoint, bool moveHorizontal)
        {
            double average = 0;
            int averageCount = 0;
            if (moveHorizontal)
            {
                inPoint.FindLeftRightPoints(out SimplePoint leftPoint, out SimplePoint rightPoint);
                if (PastPlates[leftPoint.X, leftPoint.Y] == inPlate)
                {
                    if (PastHeights[leftPoint.X, leftPoint.Y] != 0)
                    {
                        average += PastHeights[leftPoint.X, leftPoint.Y];
                        averageCount++;
                    }
                }
                if (PastPlates[rightPoint.X, rightPoint.Y] == inPlate)
                {
                    if (PastHeights[rightPoint.X, rightPoint.Y] != 0)
                    {
                        average += PastHeights[rightPoint.X, rightPoint.Y];
                        averageCount++;
                    }
                }
                if (averageCount == 0)
                {
                    inPoint.FindAboveBelowPoints(out SimplePoint abovePoint, out SimplePoint belowPoint);
                    if (PastPlates[abovePoint.X, abovePoint.Y] == inPlate)
                    {
                        if (PastHeights[abovePoint.X, abovePoint.Y] != 0)
                        {
                            average += PastHeights[abovePoint.X, abovePoint.Y];
                            averageCount++;
                        }
                    }
                    if (PastPlates[belowPoint.X, belowPoint.Y] == inPlate)
                    {
                        if (PastHeights[belowPoint.X, belowPoint.Y] != 0)
                        {
                            average += PastHeights[belowPoint.X, belowPoint.Y];
                            averageCount++;
                        }
                    }
                }
            }
            else
            {
                inPoint.FindAboveBelowPoints(out SimplePoint abovePoint, out SimplePoint belowPoint);
                if (PastPlates[abovePoint.X, abovePoint.Y] == inPlate)
                {
                    if (PastHeights[abovePoint.X, abovePoint.Y] != 0)
                    {
                        average += PastHeights[abovePoint.X, abovePoint.Y];
                        averageCount++;
                    }
                }
                if (PastPlates[belowPoint.X, belowPoint.Y] == inPlate)
                {
                    if (PastHeights[belowPoint.X, belowPoint.Y] != 0)
                    {
                        average += PastHeights[belowPoint.X, belowPoint.Y];
                        averageCount++;
                    }
                }
                if (averageCount == 0)
                {
                    inPoint.FindLeftRightPoints(out SimplePoint leftPoint, out SimplePoint rightPoint);
                    if (PastPlates[leftPoint.X, leftPoint.Y] == inPlate)
                    {
                        if (PastHeights[leftPoint.X, leftPoint.Y] != 0)
                        {
                            average += PastHeights[leftPoint.X, leftPoint.Y];
                            averageCount++;
                        }
                    }
                    if (PastPlates[rightPoint.X, rightPoint.Y] == inPlate)
                    {
                        if (PastHeights[rightPoint.X, rightPoint.Y] != 0)
                        {
                            average += PastHeights[rightPoint.X, rightPoint.Y];
                            averageCount++;
                        }
                    }
                }
            }
            if (averageCount > 1)
            {
                average = average / (double)averageCount;
            }
            return average;
        }

        /// <summary>
        /// Scans for all points in <see cref="ActivePoints"/> that are set to true and are
        /// contiguously adjacent to the given point and adds them to <see cref="temporaryPlate"/>,
        /// setting the <see cref="ActivePoints"/> to false in the process.
        /// </summary>
        /// <param name="startingPoint">Starting point to check neighbors.</param>
        private void CheckNeighbor(SimplePoint startingPoint)
        {
            Stack<SimplePoint> pointStack = new Stack<SimplePoint>();
            ActivePoints[startingPoint.X, startingPoint.Y] = false;
            pointStack.Push(startingPoint);
            while (pointStack.Count != 0)
            {
                SimplePoint point = pointStack.Pop();
                temporaryPlate.PlatePoints.Add(new PlatePoint(point));
                SimplePoint[] newPoints = new SimplePoint[4];
                point.FindLeftRightPoints(out newPoints[0], out newPoints[1]);
                point.FindAboveBelowPoints(out newPoints[2], out newPoints[3]);
                for (int i = 0; i < 4; i++)
                {
                    if (ActivePoints[newPoints[i].X, newPoints[i].Y])
                    {
                        ActivePoints[newPoints[i].X, newPoints[i].Y] = false;
                        pointStack.Push(newPoints[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Expands each plate one pixel in every direction until no point outside of all plates exists.
        /// </summary>
        private void ExpandPlates()
        {
            Queue<OverlapPoint> borderPoints = new Queue<OverlapPoint>();
            for (int i = 0; i < Plates.Length; i++)
            {
                for (int j = 0; j < Plates[i].PlatePoints.Count; j++)
                {
                    SimplePoint[] newPoints = new SimplePoint[4];
                    Plates[i].PlatePoints[j].FindLeftRightPoints(out newPoints[0], out newPoints[1]);
                    Plates[i].PlatePoints[j].FindAboveBelowPoints(out newPoints[2], out newPoints[3]);
                    for (int k = 0; k < 4; k++)
                    {
                        if (!ActivePoints[newPoints[k].X, newPoints[k].Y])
                        {
                            borderPoints.Enqueue(new OverlapPoint(newPoints[k], i));
                        }
                    }
                }
            }
            while (borderPoints.Count != 0)
            {
                OverlapPoint borderPoint = borderPoints.Dequeue();
                if (!ActivePoints[borderPoint.X, borderPoint.Y])
                {
                    ActivePoints[borderPoint.X, borderPoint.Y] = true;
                    SimplePoint oldPoint = new SimplePoint(borderPoint.X, borderPoint.Y);
                    Plates[borderPoint.plateIndex[0]].PlatePoints.Add(new PlatePoint(oldPoint));
                    SimplePoint[] newPointsP = new SimplePoint[4];
                    oldPoint.FindLeftRightPoints(out newPointsP[0], out newPointsP[1]);
                    oldPoint.FindAboveBelowPoints(out newPointsP[2], out newPointsP[3]);
                    for (int k = 0; k < 4; k++)
                    {
                        if (!ActivePoints[newPointsP[k].X, newPointsP[k].Y])
                        {
                            OverlapPoint newPoint = new OverlapPoint(newPointsP[k], borderPoint.plateIndex[0]);
                            borderPoints.Enqueue(newPoint);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Finds all points with two or more entries in multiple plates and outputs them in a list of <see cref="OverlapPoint"/>.
        /// </summary>
        /// <returns>List of <see cref="OverlapPoint"/>.</returns>
        private List<OverlapPoint> FindOverlap()
        {
            List<OverlapPoint> rawOutput = new List<OverlapPoint>();
            List<OverlapPoint> output = new List<OverlapPoint>();
            bool[,] ActivePointsTwo = new bool[2 * _layerSize, _layerSize];
            Parallel.For(0, Plates.Length, (i) =>
            {
                foreach (PlatePoint iPoint in Plates[i].PlatePoints)
                {
                    if (ActivePoints[iPoint.X, iPoint.Y])
                    {
                        ActivePoints[iPoint.X, iPoint.Y] = false;
                    }
                    else
                    {
                        ActivePointsTwo[iPoint.X, iPoint.Y] = true;
                    }
                }
            });
            for (int i = 0; i < Plates.Length; i++)
            {
                for (int j = 0; j < Plates[i].PlatePoints.Count; j++)
                {
                    if (ActivePointsTwo[Plates[i].PlatePoints[j].X, Plates[i].PlatePoints[j].Y])
                    {
                        rawOutput.Add(new OverlapPoint(Plates[i].PlatePoints[j].X, Plates[i].PlatePoints[j].Y, i, j));
                    }
                }
            }
            rawOutput.Sort();
            int index = 0;
            for (int i = 1; i < rawOutput.Count; i++)
            {
                if (rawOutput[i - 1].CompareTo(rawOutput[i]) == 0)
                {
                    rawOutput[index].plateIndex.Add(rawOutput[i].plateIndex[0]);
                    rawOutput[index].pointIndex.Add(rawOutput[i].pointIndex[0]);
                }
                else
                {
                    output.Add(rawOutput[index]);
                    index = i;
                }
            }
            return output;
        }

        /// <summary>
        /// Increases the Height of all points within circles centered at the given list of points.
        /// </summary>
        /// <param name="radius">Radius of circles.</param>
        /// <param name="magnitude">Magnitude of height to be added per point per circle.</param>
        /// <param name="points">List of points where the circles are centered.</param>
        private void FlowStep(double radius, double magnitude, List<BasePoint> points)
        {
            double radiusSquared = radius * radius;
            Parallel.For(0, (points.Count), (i) =>
            {
                points[i].Range(radius, out int xMin, out int xMax, out int yMin, out int yMax);
                for (int xP = xMin; xP < xMax; xP++)
                {
                    int x = xP;
                    if (x >= 2 * _layerSize)
                    {
                        x -= 2 * _layerSize;
                    }
                    for (int y = yMin; y < yMax; y++)
                    {
                        if (points[i].Distance(PointMap[x, y]) < radiusSquared)
                        {
                            PointMagnitudes[x, y] += magnitude;
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Stores heights of plates in in <see cref="PastPlates"/>.
        /// </summary>
        private void HeightTracker()
        {
            Parallel.For(0, (Plates.Length), (i) =>
            {
                foreach (PlatePoint iPoint in Plates[i].PlatePoints)
                {
                    PastHeights[iPoint.X, iPoint.Y] = iPoint.Height;
                }
            });
        }

        /// <summary>
        /// Sets <see cref="ActivePoints"/> to true for points above magnitude threshold, false otherwise.
        /// </summary>
        /// <param name="magnitudeThreshold">Magnitude of threshold for setting points to active.</param>
        private void MagnitudeFilter(double magnitudeThreshold)
        {
            for (int x = 0; x < 2 * _layerSize; x++)
            {
                for (int y = 0; y < _layerSize; y++)
                {
                    if (magnitudeThreshold < PointMagnitudes[x, y])
                    {
                        ActivePoints[x, y] = true;
                    }
                    else
                    {
                        ActivePoints[x, y] = false;
                    }
                    PointMagnitudes[x, y] = 0;
                }
            }
        }

        /// <summary>
        /// Sorts all point magnitudes and returns the magnitude at the given index.
        /// </summary>
        /// <param name="index">Index to return height value.</param>
        /// <returns>Height at given index.</returns>
        private double MediumMagnitude(int index)
        {
            double[] output = new double[2 * _layerSize * _layerSize];
            int k = 0;
            foreach (double pointMagnitude in PointMagnitudes)
            {
                output[k] = pointMagnitude;
                k++;
            }
            Array.Sort(output);
            return output[index];
        }

        /// <summary>
        /// Uses <see cref="CheckNeighbor"/> to generate a plate starting at the given point, and transfers it
        /// to <see cref="Plates"/> if there is an empty plate or the plate is larger than the smallest existing plate.
        /// </summary>
        /// <param name="inPoint">Starting Point.</param>
        private void PlateMaker(SimplePoint inPoint)
        {
            if (temporaryPlate.PlatePoints != null)
            {
                temporaryPlate.PlatePoints.Clear();
            }
            if (ActivePoints[inPoint.X, inPoint.Y])
            {
                CheckNeighbor(inPoint);
                foreach (Plate iPlate in Plates)
                {
                    if (iPlate.PlatePoints.Count == 0)
                    {
                        iPlate.CopyPoints(temporaryPlate.PlatePoints);
                        return;
                    }
                }
                foreach (Plate iPlate in Plates)
                {
                    if (iPlate.PlatePoints.Count < temporaryPlate.PlatePoints.Count)
                    {
                        iPlate.CopyPoints(temporaryPlate.PlatePoints);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Generates the largest <see cref="Plates"/>, sets <see cref="ActivePoints"/> to true for every point in one
        /// of those plates, and false otherwise.
        /// </summary>
        private void PlateMaking()
        {
            for (int x = 0; x < 2 * _layerSize; x++)
            {
                for (int y = 0; y < _layerSize; y++)
                {
                    if (ActivePoints[x, y])
                    {
                        PlateMaker(new SimplePoint(x, y));
                    }
                }
            }
            Parallel.For(0, (Plates.Length), (i) =>
            {
                foreach (PlatePoint iPoint in Plates[i].PlatePoints)
                {
                    ActivePoints[iPoint.X, iPoint.Y] = true;
                }
            });
        }

        /// <summary>
        /// Stores which points belonged to which plate in <see cref="PastPlates"/>.
        /// </summary>
        private void PlateTracker()
        {
            Parallel.For(0, (Plates.Length), (i) =>
            {
                foreach (PlatePoint iPoint in Plates[i].PlatePoints)
                {
                    PastPlates[iPoint.X, iPoint.Y] = i;
                }
            });
        }

        /// <summary>
        /// Corrects to one plate entry for a given point.
        /// </summary>
        /// <param name="input">Input Overlap Point.</param>
        private void ResolveOverlap(OverlapPoint input)
        {
            int index = 0;
            for (int i = 1; i < input.plateIndex.Count; i++)
            {
                if (Plates[input.plateIndex[i]].PlatePoints[input.pointIndex[i]].Height != 0)
                {
                    if (Plates[input.plateIndex[index]].PlatePoints[input.pointIndex[index]].Height != 0)
                    {
                        Plates[input.plateIndex[index]].PlatePoints[input.pointIndex[index]].Height += 0.6 * Plates[input.plateIndex[i]].PlatePoints[input.pointIndex[i]].Height;
                        Plates[input.plateIndex[i]].PlatePoints.RemoveAt(input.pointIndex[i]);
                    }
                    else
                    {
                        Plates[input.plateIndex[index]].PlatePoints.RemoveAt(input.pointIndex[index]);
                        index = i;
                    }
                }
                else
                {
                    Plates[input.plateIndex[i]].PlatePoints.RemoveAt(input.pointIndex[i]);
                }
            }
        }

        /// <summary>
        /// Adds points to plates by tracing them backwards.
        /// </summary>
        /// <param name="input">Point to add.</param>
        /// <param name="timeStep">Time factor for moving plates.</param>
        private void ReverseAdd(BasePoint input, double timeStep)
        {
            for (int i = 0; i < Plates.Length; i++)
            {
                double[] angle = new double[3]
                {
                    Plates[i].Direction[0], Plates[i].Direction[1], -1 * Plates[i].Speed * timeStep
                };
                SimplePoint reversedPoint = input.Transform(angle);
                if (PastPlates[reversedPoint.X, reversedPoint.Y] == i)
                {
                    bool horizontalMove = false;
                    if (input.Y == reversedPoint.Y)
                    {
                        horizontalMove = true;
                    }
                    double average = AdjacentHeightAverage(i, reversedPoint, horizontalMove);
                    Plates[i].PlatePoints.Add(new PlatePoint(input, average));
                    ActivePoints[input.X, input.Y] = true;
                }
            }
        }

        /// <summary>
        /// Opens height and plate files and loads the data.
        /// </summary>
        /// <param name="directory">Directory of files.</param>
        /// <param name="heightFile">Height File name, not including .bin extension.</param>
        /// <param name="plateFile">Plate File name, not including .bin extension.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool OpenFiles(string inDirectory, string heightFile, string plateFile)
        {
            PointIO openData = new PointIO
            {
                directory = inDirectory
            };
            bool output;
            output = openData.OpenHeightData(heightFile, _layerSize, out double[,] heights);
            if (!output)
            {
                return false;
            }
            output = openData.OpenPlateData(plateFile, _layerSize, out int[,] plateNumbers);
            if (!output)
            {
                return false;
            }
            foreach (BasePoint iPoint in PointMap)
            {
                PlatePoint newPoint = new PlatePoint(iPoint.X, iPoint.Y, heights[iPoint.X, iPoint.Y]);
                Plates[plateNumbers[iPoint.X, iPoint.Y]].PlatePoints.Add(newPoint);
            }
            return true;
        }

        /// <summary>
        /// Generates plates based off of sets of circles with given probabilistic distribution corrected for area.
        /// Then, expands plates to include all points.
        /// </summary>
        /// <param name="magnitude">Array of weight of each set of circles.</param>
        /// <param name="radius">Radius of each set of circles.</param>
        /// <param name="pointConcentration">Raw probability that a point will be in a circle.</param>
        /// <param name="cutOff">Approximate number of points which will initially become plates.</param>
        public void PlateGeneration(double[] magnitude, double[] radius, double[] pointConcentration, int cutOff)
        {
            Random rnd = new Random();
            for (int i = 0; i < magnitude.Length; i++)
            {
                List<BasePoint> circleList = new List<BasePoint>();
                foreach (BasePoint iPoint in PointMap)
                {
                    if (iPoint.TestMomentum(rnd.NextDouble(), pointConcentration[i]))
                    {
                        circleList.Add(iPoint);
                    }
                }
                FlowStep(radius[i], magnitude[i], circleList);
            }
            MagnitudeFilter(MediumMagnitude(cutOff));
            PlateMaking();
            ExpandPlates();
        }

        /// <summary>
        /// Moves all plates for a given time factor.
        /// </summary>
        /// <param name="timeStep">Time factor for plate movement.</param>
        public void PlateMove(double timeStep)
        {
            PlateTracker();
            HeightTracker();
            Parallel.For(0, Plates.Length, (i) =>
            {
                Plates[i].Slide(timeStep);
            });
            Parallel.For(0, Plates.Length, (i) =>
            {
                foreach (PlatePoint iPoint in Plates[i].PlatePoints)
                {
                    ActivePoints[iPoint.X, iPoint.Y] = true;
                }
            });
            Parallel.For(0, 2 * _layerSize, (x) =>
              {
                  for (int y = 0; y < _layerSize; y++)
                  {
                      if (!ActivePoints[x, y])
                      {
                          ReverseAdd(PointMap[x, y], timeStep);
                      }
                  }
              });
            ExpandPlates();
            List<OverlapPoint> overlapList = FindOverlap();
            foreach (OverlapPoint iPoint in overlapList)
            {
                ResolveOverlap(iPoint);
            }
            HeightTracker();
            PlateTracker();
        }

        /// <summary>
        /// Inputs speed and direction data.
        /// </summary>
        /// <param name="speeds">Speed of plate movement to input to <see cref="Plate.Speed"/>.</param>
        /// <param name="directions">Direction of plate movement to input to <see cref="Plate.Direction"/>.</param>
        public void PlatesVelocityInputs(double[] speeds, double[,] directions)
        {
            for (int i = 0; i < Plates.Length; i++)
            {
                Plates[i].Speed = speeds[i];
                Plates[i].Direction[0] = directions[0, i];
                Plates[i].Direction[1] = directions[1, i];
            }
        }
    }
}