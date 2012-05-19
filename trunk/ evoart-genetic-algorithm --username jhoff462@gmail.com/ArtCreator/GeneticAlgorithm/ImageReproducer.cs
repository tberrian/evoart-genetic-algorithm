using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// Given two individuals, will create a child using a crossover point. 
    /// </summary>
    public class ImageReproducer : IReproduction
    {
        #region members
            private static Random RNG = new Random();
        #endregion members

        #region methods
            /// <summary>
            /// Returns a child of the two parents. 
            /// </summary>
            /// <param name="parentOne"></param>
            /// <param name="parentTwo"></param>
            /// <returns></returns>
            public IGeneticIndividual Reproduce(IGeneticIndividual parentOne, IGeneticIndividual parentTwo)
            {
                var one = (parentOne as Individual);
                var two = (parentTwo as Individual);

                var childOne = GenerateChild(one, two);
                var childTwo = GenerateChild(two, one);

                var child = ChooseRandomChild(childOne, childTwo);
                return child;
            }

            /// <summary>
            /// Create a child using crossover reproduction between the two parents. 
            /// </summary>
            /// <param name="one"></param>
            /// <param name="two"></param>
            /// <returns></returns>
            private Individual GenerateChild(Individual one, Individual two)
            {
                //StringBuilder genome = new StringBuilder(one.Genome.Length);
                RGB[] genome = new RGB[one.Genome.Length];
                int crossoverPoint = one.Genome.Length / 2;

                GetFirstParentGenome(one, genome, crossoverPoint);
                GetSecondParentGenome(two, genome, crossoverPoint);

                return new Individual(genome);
            }

            /// <summary>
            /// give the child the first parents genome. 
            /// </summary>
            /// <param name="parentOne"></param>
            /// <param name="childCoins"></param>
            /// <param name="crossoverPoint"></param>
            private void GetFirstParentGenome(Individual one, RGB[] genome, int crossoverPoint)
            {
                for (int index = 0; index < crossoverPoint; index++)
                    genome[index] = (one.Genome[index]);
            }

            /// <summary>
            /// Give the child the second parents genomes. 
            /// </summary>
            /// <param name="parentTwo"></param>
            /// <param name="childCoins"></param>
            /// <param name="crossoverPoint"></param>
            private void GetSecondParentGenome(Individual two, RGB[] genome, int crossoverPoint)
            {
                for (int index = crossoverPoint; index < two.Genome.Length; index++)
                    genome[index] = (two.Genome[index]);
            }

            /// <summary>
            /// Choose a child to survive. 
            /// (ideally this would be done via a gladiator match between the two children. (pay per view is not out of the question))
            /// </summary>
            /// <param name="childOne"></param>
            /// <param name="childTwo"></param>
            /// <returns></returns>
            private Individual ChooseRandomChild(Individual childOne, Individual childTwo)
            {
                int randomChild = RNG.Next(0, 2);

                if (randomChild == 0)
                    return childOne;
                else
                    return childTwo;
            }
        #endregion methods
    }
}
