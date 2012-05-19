using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;
using System.Drawing;
using System.Drawing.Imaging;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// Holds data on the individuals genome configuration (width x height), as well as the target image. 
    /// </summary>
    public class ImageConfiguration : Configuration
    {
        #region members
            /// <summary>
            /// The pixel data of the image. 
            /// </summary>
            private readonly List<RGB> _target;
            /// <summary>
            /// The images width 
            /// </summary>
            private readonly int _width;
            /// <summary>
            /// The images height
            /// </summary>
            private readonly int _height;
        #endregion members

        #region properties
            public List<RGB> Target
            {
                get { return _target; }
            }

            public int Width
            {
                get { return _width; }
            }

            public int Height
            {
                get { return _height; }
            }
        #endregion properties

        #region constructors
            public ImageConfiguration(Bitmap target, int populationSize, double mutationRate, double elitsmRate)
                : base(populationSize, mutationRate, elitsmRate)
            {
                _target = new List<RGB>();

                _width = target.Width;
                _height = target.Height;

                CreateTarget(target);
            }

            private void CreateTarget(Bitmap target)
            {
                for (int x = target.Width - 1; x >= 0; x--)
                {
                    for (int y = target.Height - 1; y >= 0; y--)
                    {
                        var targetPixel = target.GetPixel(x, y);
                        _target.Add(new RGB(targetPixel.R, targetPixel.G, targetPixel.B));
                    }
                }
            }
        #endregion construcors

        #region methods
        #endregion methods
    }
}
