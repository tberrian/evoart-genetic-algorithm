using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// Will randomly mutate an individual. 
    /// </summary>
    public class ImageMutator : IMutator
    {
        #region members
            private static Random RNG = new Random();
            private Configuration _configuration;
        #endregion members

        #region constructors
            public ImageMutator(Configuration configuration)
            {
                _configuration = configuration;
            }
        #endregion constructors

        #region methods
            /// <summary>
            /// Mutate the individual. 
            /// </summary>
            /// <param name="individual"></param>
            public void Mutate(IGeneticIndividual individual)
            {
                var toMutate = individual as Individual;
                ChangeGenome(toMutate);
            }

            /// <summary>
            /// Iterate over each allele, each has a random probability of being mutated to a new color value. 
            /// </summary>
            /// <param name="toMutate"></param>
            private void ChangeGenome(Individual toMutate)
            {
                double probabilityOfMutation = _configuration.MutationRate;

                for (int index = 0; index < toMutate.Genome.Length; index++)
                {
                    if (RNG.NextDouble() < probabilityOfMutation)
                    {
                        var newAllele = GetNewRandomAllele();
                        toMutate.Genome[index] = newAllele;
                    }
                }
            }

            private RGB GetNewRandomAllele()
            {
                return new RGB(RNG.Next(0, 256),RNG.Next(0, 256),RNG.Next(0, 256));
            }
        #endregion methods
    }
}
