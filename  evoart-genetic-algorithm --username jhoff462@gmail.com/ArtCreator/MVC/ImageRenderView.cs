using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// This class translates our RGB collection to a Bitmap which can be rendered on a winforms screen. 
    /// It requires a collection of RGB color values taken from any concrete ImageProcessor
    /// </summary>
    class ImageRenderView
    {
        /// <summary>
        /// To display the image, create a Bitmap (easist way to render to a winforms picturebox) 
        /// Write our image data pixel by pixel to the new bitmap. 
        /// </summary>
        /// <param name="colorData"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public Bitmap GetImageFromColorData(Individual individual, int width, int height)
        {
            Bitmap image = new Bitmap(width, height);

            var colorData = individual.Genome;

            for (int x = image.Width - 1; x > 0; x--)
            {
                for (int y = image.Height - 1; y > 0; y--)
                {
                    int index = TranslateXYToListIndex(x, y, image.Width);

                    if (index >= colorData.Length)
                        index = colorData.Length - 1;

                    Color color = Color.FromArgb(colorData[index].R, colorData[index].G, colorData[index].B);
                    image.SetPixel(x, y, color);
                }
            }

            //Flip the bmp to display it on screen
            image.RotateFlip(RotateFlipType.Rotate270FlipX);

            return image;
        }

        /// <summary>
        /// The RGB list given by the ImageProcessor can be thought of as a 2D array of pixel data. 
        /// In order to create the image from this data we have to convert the x, and y value to an
        /// index in the list which corresponds to that exact pixel. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        private int TranslateXYToListIndex(int x, int y, int rowCount)
        {
            return (rowCount * y) + (x);
        }
    }
}
