using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Planetary_Generation
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Runs when form first loads up.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void directoryInput_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Directory = directoryInput.Text;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// When the user presses the Generation Button, retrieves inputs from text boxes and runs Plate Generation.
        /// </summary>
        private void GenerationButton_Click(object sender, EventArgs e)
        {
            errorDisplay.Text = "";
            if (!Int32.TryParse(sizeInput.Text, out int size))
            {
                errorDisplay.Text += "Size is not an integer. ";
                return;
            }
            if (!Int32.TryParse(numberOfPlatesInput.Text, out int numberOfPlates))
            {
                errorDisplay.Text += "Number of Plates is not an integer. ";
                return;
            }
            List<double>[] genParams = new List<double>[3];
            genParams = ProcessString(genParamsInput.Text, 3);
            if (genParams == null || genParams[0].Count != genParams[1].Count || genParams[1].Count != genParams[2].Count)
            {
                errorDisplay.Text += "Magnitude is input incorrectly. Format is: 12.3,4.56,789; . ";
                return;
            }
            double[] magnitude = new double[genParams[0].Count];
            double[] radius = new double[genParams[1].Count];
            double[] pointConcentration = new double[genParams[2].Count];
            for (int i = 0; i < genParams[0].Count; i++)
            {
                magnitude[i] = genParams[0][i];
                radius[i] = genParams[1][i];
                pointConcentration[i] = genParams[2][i];
            }
            if (!Int32.TryParse(cutoffInput.Text, out int cutoff))
            {
                errorDisplay.Text += "Cutoff is not an integer. ";
                return;
            }
            PlateLayer PlateLayerI = new PlateLayer(size, numberOfPlates);
            PlateLayerI.PlateGeneration(magnitude, radius, pointConcentration, cutoff);
            PointIO pointIO = new PointIO { directory = directoryInput.Text };
            pointIO.SaveHeightImage(outNameInput.Text, PlateLayerI.PastHeights);
            pointIO.SaveMapData(outNameInput.Text, PlateLayerI.PastHeights);
            pictureBox.ImageLocation = directoryInput.Text + "\\" + outNameInput.Text + ".png";
            errorDisplay.Text = "No errors yet. Errors will be displayed here.";
        }

        /// <summary>
        /// When the user presses the Move Plate Button, retrieves inputs from text boxes and runs Plate Movement.
        /// </summary>
        private void MovePlateButton_Click(object sender, EventArgs e)
        {
            errorDisplay.Text = "";
            if (!Int32.TryParse(sizeInput.Text, out int size))
            {
                errorDisplay.Text += "Size is not an integer. ";
                return;
            }
            List<double>[] genParams = new List<double>[3];
            genParams = ProcessString(velocityInput.Text, 3);
            if (genParams == null || genParams[0].Count != genParams[1].Count || genParams[1].Count != genParams[2].Count)
            {
                errorDisplay.Text += "Velocity is input incorrectly. Format is: 12.3,4.56,789; . ";
                return;
            }
            double[] speeds = new double[genParams[0].Count];
            double[,] directions = new double[2, genParams[1].Count];
            for (int i = 0; i < genParams[2].Count; i++)
            {
                speeds[i] = genParams[0][i];
                directions[0, i] = genParams[1][i];
                directions[1, i] = genParams[2][i];
            }
            if (!Double.TryParse(timeStepInput.Text, out double timeStep))
            {
                errorDisplay.Text += "Time Step is not a number. ";
                return;
            }
            PlateLayer PlateLayerI = new PlateLayer(size, speeds.Length);
            if (!PlateLayerI.OpenFiles(directoryInput.Text, heightMapInput.Text, plateMapInput.Text))
            {
                errorDisplay.Text += "Bin Map input is unreadable. ";
                return;
            }
            PlateLayerI.PlatesVelocityInputs(speeds, directions);
            PlateLayerI.PlateMove(timeStep);
            PointIO pointIO = new PointIO { directory = directoryInput.Text };
            pointIO.SaveHeightImage(heightMapInput.Text, PlateLayerI.PastHeights);
            pointIO.SaveMapData(heightMapInput.Text, PlateLayerI.PastHeights);

            pointIO.SavePlateImage(plateMapInput.Text, PlateLayerI.PastPlates);
            pointIO.SaveMapData(plateMapInput.Text, PlateLayerI.PastPlates);

            pictureBox.ImageLocation = directoryInput.Text + "\\" + heightMapInput.Text + ".png";
            errorDisplay.Text = "No errors yet. Errors will be displayed here.";
        }

        /// <summary>
        /// Converts string to an array of double lists.
        /// </summary>
        /// <param name="text">Input string.</param>
        /// <param name="outputSize">Number of lists per array.</param>
        /// <returns>Array of double lists.</returns>
        private List<double>[] ProcessString(string text, int outputSize)
        {
            List<double>[] output = new List<double>[3];
            for (int i = 0; i < outputSize; i++)
            {
                output[i] = new List<double>();
            }
            List<double> magnitudeList = new List<double>();
            List<double> radiusList = new List<double>();
            List<double> pointConcentrationList = new List<double>();
            int index = 0;
            int type = 0;
            string tempString = "";
            foreach (char iChar in text)
            {
                if (!(Char.IsNumber(iChar) || iChar == '.' || iChar == ',' || iChar == ';'))
                {
                    return null;
                }
                if (Char.IsNumber(iChar) || iChar == '.')
                {
                    tempString += iChar;
                }
                else
                {
                    switch (type)
                    {
                        case 0:
                            output[0].Add(Double.Parse(tempString));
                            break;

                        case 1:
                            output[1].Add(Double.Parse(tempString));
                            break;

                        case 2:
                            output[2].Add(Double.Parse(tempString));
                            break;

                        default:
                            return null;
                    }
                    if (iChar == ',')
                    {
                        type++;
                    }
                    else
                    {
                        type = 0;
                        index++;
                    }
                    tempString = "";
                }
            }
            return output;
        }

        /// <summary>
        /// Sets default values to text boxes.
        /// </summary>
        public void Initialize()
        {
            directoryInput.Text = "D:\\Documents\\Visual Studio 2017\\Planetary Generation\\Data";
            sizeInput.Text = "1000";
            int tempNumberOfCycles = 10;
            genParamsInput.Text = "";
            for (int i = 0; i < tempNumberOfCycles; i++)
            {
                genParamsInput.Text += (16 - i).ToString() + ",";
                genParamsInput.Text += (Math.Round(Math.Sin(Math.PI * (12 - i) / 120), 2)).ToString() + ",";
                genParamsInput.Text += (0.999).ToString() + ";";
            }
            cutoffInput.Text = (1000 * 1000 * 3 / 2).ToString();
            numberOfPlatesInput.Text = (10).ToString();
            outNameInput.Text = "PlateMap";
            velocityInput.Text = "";
            for (int i = 0; i < 10; i++)
            {
                double angleOne = i * Math.PI / 4.5;
                angleOne = Math.Round(angleOne, 3);
                velocityInput.Text += "0.02,";
                velocityInput.Text += angleOne.ToString();
                velocityInput.Text += ",";
                velocityInput.Text += angleOne.ToString();
                velocityInput.Text += ";";
            }
            plateMapInput.Text = "PlateMap";
            heightMapInput.Text = "PlateMap00";
            timeStepInput.Text = (1).ToString();
            errorDisplay.Text = "Errors will be displayed here.";
        }
    }
}