using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCreator.ImageGeneticAlgorithm;
using System.Drawing.Imaging;

namespace ArtCreator
{
    public partial class Main : Form
    {
        #region members
            /// <summary>
            /// The genetic algorithm which will recreate the target image. 
            /// </summary>
            ImageGeneticAlgorithm.ImageGeneticAlgorithm _geneticAlgorithm;
            /// <summary>
            /// The configuration, stores genome information and the target image. 
            /// </summary>
            ImageConfiguration _configuration; 
            /// <summary>
            /// Renders the individuals to the screen. 
            /// </summary>
            ImageRenderView _drawer;
            /// <summary>
            /// The location to save the progress images of the genetic algorithm. 
            /// </summary>
            private string _progressSaveDirectory;
            /// <summary>
            /// The number of generations to advance before saving the progress. 
            /// </summary>
            private readonly int SAVE_PROGRESS_STEP_COUNT = 50;
        #endregion members

        #region constructors
            public Main()
            {
                InitializeComponent();
            }
        #endregion construcors

        #region methods
            /// <summary>
            /// Loads the target image file. 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void loadToolStripMenuItem_Click(object sender, EventArgs e)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "img files (*.png)|*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (ofd.OpenFile() != null)
                        {
                            var directory = System.IO.Path.GetDirectoryName(ofd.FileName);
                            var fileName = System.IO.Path.GetFileName(ofd.FileName);

                            var path = System.IO.Path.Combine(directory, fileName);

                            MessageBox.Show(path);

                            Image target = Image.FromFile(ofd.FileName);
                            pbTarget.Image = target;


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            /// <summary>
            /// Creates the genetic algorithm. 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnInitalize_Click(object sender, EventArgs e)
            {
                Bitmap target = new Bitmap(pbTarget.Image);
                _configuration = new ImageConfiguration(target, 500, .005, .2);
                _geneticAlgorithm = new ImageGeneticAlgorithm.ImageGeneticAlgorithm(_configuration);
                _drawer = new ImageRenderView();
                btnRun.Enabled = true;
                RenderBestIndividual();
                CreateSaveDirectory();
            }

            /// <summary>
            /// Creates the folder to save the images to. 
            /// </summary>
            private void CreateSaveDirectory()
            {
                _progressSaveDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"Genetic Algorithm Progress\");

                if (!System.IO.Directory.Exists(_progressSaveDirectory))
                    System.IO.Directory.CreateDirectory(_progressSaveDirectory);
            }

            /// <summary>
            /// Runs the genetic algorithm indefinitely. 
            /// Every 30 iterations it updates the view to display the best individual. 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnRun_Click(object sender, EventArgs e)
            {
                SaveCurrentImage(0);
                int index = 0;
                while (true)
                {
                    _geneticAlgorithm.EvolveOneGeneration();
                    this.Text = index.ToString();

                    if (index % 30 == 0)
                        RenderBestIndividual();

                    if (index % SAVE_PROGRESS_STEP_COUNT == 0)
                        SaveCurrentImage(index);

                    index++;
                }
            }
            
            /// <summary>
            /// Saves the current image to the disk, used to save progress! 
            /// </summary>
            /// <param name="index"></param>
            private void SaveCurrentImage(int index)
            {
                string savePath = System.IO.Path.Combine(_progressSaveDirectory, "IMG_" + index.ToString() + ".png");

                pbGeneticAlgorithm.Image.Save(savePath, ImageFormat.Png);
            }

            /// <summary>
            /// Displays the fittest individual in the view. 
            /// </summary>
            private void RenderBestIndividual()
            {
                var image = _drawer.GetImageFromColorData(_geneticAlgorithm.BestIndividual, _configuration.Width, _configuration.Height);
                pbGeneticAlgorithm.Image = image;
                this.Refresh();
            }

        #endregion methods
    }
}
