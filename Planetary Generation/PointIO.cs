using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Planetary_Generation
{
    /// <summary>
    /// Allows inputting and outputting data for points.
    /// </summary>
    internal class PointIO
    {
        /// <summary>
        /// Location of file directory.
        /// </summary>
        public string directory;

        /// <summary>
        /// Finds the minimum and maximum value in a set.
        /// </summary>
        /// <param name="data">Set of data to search.</param>
        /// <param name="min">Minimum value of set.</param>
        /// <param name="max">Maximum value of set.</param>
        private void FindMinMax<T>(T[,] data, out T min, out T max) where T : IComparable<T>
        {
            min = max = data[0, 0];
            foreach (T item in data)
            {
                if (item.CompareTo(min) < 0)
                {
                    min = item;
                }
                if (item.CompareTo(max) > 0)
                {
                    max = item;
                }
            }
        }

        /// <summary>
        /// Scales data to integers from -255 to 255.
        /// </summary>
        /// <param name="data">Data to scale.</param>
        /// <returns>Data returned as integers from -255 to 255.</returns>
        private int[,] ScaleData(double[,] data)
        {
            FindMinMax(data, out double min, out double max);
            double dif = max - min;
            int[,] output = new int[data.GetLength(0), data.GetLength(1)];
            Parallel.For(0, data.GetLength(0), (x) =>
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    output[x, y] = (int)Math.Round(((data[x, y] - min) * 510 / dif), 0) - 255;
                }
            });
            return output;
        }

        /// <summary>
        /// Opens a data file and outputs a double array of the data, presumed to be heights. Returns
        /// true if successful, false otherwise.
        /// </summary>
        /// <param name="fileName">File Name (not including extension) to open.</param>
        /// <param name="heights">Output of file data.</param>
        /// <param name="size">Size of map, as <see cref="PlateLayer._layerSize"/>.</param>
        /// <returns>Returns true if successful, false otherwise.</returns>
        public bool OpenHeightData(string fileName, int size, out double[,] heights)
        {
            heights = new double[2 * size, size];
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(directory + "\\" + fileName + ".bin", FileMode.Create)))
                {
                    for (int x = 0; x < 2 * size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            heights[x, y] = reader.ReadDouble();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Opens a data file and outputs a int array of the data, presumed to be heights. Returns
        /// true if successful, false otherwise.
        /// </summary>
        /// <param name="fileName">File Name (not including extension) to open.</param>
        /// <param name="heights">Output of file data.</param>
        /// <param name="size">Size of map, as <see cref="PlateLayer._layerSize"/>.</param>
        /// <returns>Returns true if successful, false otherwise.</returns>
        public bool OpenPlateData(string fileName, int size, out int[,] plateNumbers)
        {
            plateNumbers = new int[2 * size, size];
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(directory + "\\" + fileName + ".bin", FileMode.Create)))
                {
                    for (int x = 0; x < 2 * size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            plateNumbers[x, y] = reader.ReadInt32();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Saves height data as an image file.
        /// </summary>
        /// <param name="fileName">File to store at, not including extension.</param>
        /// <param name="heightData">Height data to store.</param>
        public void SaveHeightImage(string fileName, double[,] heightData)
        {
            int[,] imageData = ScaleData(heightData);
            SaveImageData(fileName, imageData);
        }

        /// <summary>
        /// Stores integer data to image file.
        /// </summary>
        /// <param name="fileName">File to store at, not including extension.</param>
        /// <param name="imageData">Data to store.</param>
        public void SaveImageData(string fileName, int[,] imageData)
        {
            using (Image<Rgba32> image = new Image<Rgba32>(imageData.GetLength(0), imageData.GetLength(1)))
            {
                for (int x = 0; x < imageData.GetLength(0); x++)
                {
                    for (int y = 0; y < imageData.GetLength(1); y++)
                    {
                        Rgba32 plateColor;
                        plateColor.A = 255;
                        if (imageData[x, y] < 0)
                        {
                            plateColor.R = (byte)(-1 * imageData[x, y]);
                            plateColor.G = 0;
                            plateColor.B = 0;
                        }
                        else
                        {
                            plateColor.R = 0;
                            plateColor.G = (byte)imageData[x, y];
                            plateColor.B = (byte)imageData[x, y];
                        }
                        image[x, y] = plateColor;
                    }
                }
                image.Save(directory + "\\" + fileName + ".png");
            }
        }

        /// <summary>
        /// Saves integer data from 2 dimensional array to .bin file.
        /// </summary>
        /// <param name="fileName">Name of file to store at, not including .bin extension.</param>
        /// <param name="mapData">Data to store, in 2 dimesnional array of integers.</param>
        public void SaveMapData(string fileName, int[,] mapData)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(directory + "\\" + fileName + ".bin", FileMode.Create)))
            {
                foreach (int iData in mapData)
                {
                    writer.Write(iData);
                }
            }
        }

        /// <summary>
        /// Saves double data from 2 dimensional array to .bin file.
        /// </summary>
        /// <param name="fileName">Name of file to store at, not including .bin extension.</param>
        /// <param name="mapData">Data to store, in 2 dimesnional array of doubles.</param>
        public void SaveMapData(string fileName, double[,] mapData)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(directory + "\\" + fileName + ".bin", FileMode.Create)))
            {
                foreach (double iData in mapData)
                {
                    writer.Write(iData);
                }
            }
        }

        /// <summary>
        /// Saves plate data as an image file.
        /// </summary>
        /// <param name="fileName">File to store at, not including extension.</param>
        /// <param name="heightData">Height data to store.</param>
        public void SavePlateImage(string fileName, int[,] plateData)
        {
            double[,] largerData = new double[plateData.GetLength(0), plateData.GetLength(1)];
            for (int x = 0; x < plateData.GetLength(0); x++)
            {
                for (int y = 0; y < plateData.GetLength(1); y++)
                {
                    largerData[x, y] = plateData[x, y];
                }
            }
            int[,] imageData = ScaleData(largerData);
            SaveImageData(fileName, imageData);
        }
    }
}