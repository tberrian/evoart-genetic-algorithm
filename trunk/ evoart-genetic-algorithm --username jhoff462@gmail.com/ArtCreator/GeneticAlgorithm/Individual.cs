using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// A member of the population. Has a genome which represents an image. 
    /// </summary>
    public class Individual : IGeneticIndividual
    {
        #region const members
            private static Random RNG = new Random();
        #endregion

        #region members
            /// <summary>
            /// The fitness of the individual 
            /// </summary>
            private double _fitness;
            /// <summary>
            /// The genome of the individual
            /// </summary>
            private RGB[] _genome;
        #endregion members

        #region properties
            /// <summary>
            /// The fitness of the individual 
            /// </summary>
            public double Fitness
            {
                get { return _fitness; }
                set { _fitness = value; }
            }
            /// <summary>
            /// The genome of the individual
            /// </summary>
            public RGB[] Genome
            {
                get { return _genome; }
                set { _genome = value; }
            }
        #endregion properties

        #region constructors
            /// <summary>
            /// Creates a new individual
            /// </summary>
            /// <param name="numberOfPixels"></param>
            public Individual(int numberOfPixels)
            {
                GenerateInitialGenome(numberOfPixels);
            }

            public Individual(RGB[] genome)
            {
                _genome = genome;
            }
            
            /// <summary>
            /// Initializes the individual with a random genome. 
            /// </summary>
            /// <param name="numberOfPixels"></param>
            private void GenerateInitialGenome(int numberOfPixels)
            {
                _genome = new RGB[numberOfPixels];

                for (int index = 0; index < _genome.Length; index++)                
                    _genome[index] = GenerateRandomAllele();                
            }

            /// <summary>
            /// Returns a random allele for the individual
            /// </summary>
            /// <returns></returns>
            private RGB GenerateRandomAllele()
            {
                int r = RNG.Next(0, byte.MaxValue + 1);
                int g = RNG.Next(0, byte.MaxValue + 1);
                int b = RNG.Next(0, byte.MaxValue + 1);

                return new RGB(r, g, b);
            }
        #endregion construcors

        #region methods
            public override string ToString()
            {
                return _fitness.ToString();
            }
        #endregion methods
    }

    /// <summary>
    /// Represents one pixel. 
    /// </summary>
    public class RGB : ICloneable
    {
        /// <summary>
        /// Red color data. 
        /// </summary>
        public int R;
        /// <summary>
        /// Green color data. 
        /// </summary>
        public int G;
        /// <summary>
        /// Blue color data.
        /// </summary>
        public int B;

        public RGB(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public object Clone()
        {
            return new RGB(this.R, this.G, this.B);
        }
    }
}
