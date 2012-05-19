using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;
using System.Drawing;
using System.Threading.Tasks;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// Evaluates how close an individual's genome is to the target. 
    /// </summary>
    class ImageFitnessEvaluator : IFitnessEvaluator
    {
        #region members
            /// <summary>
            /// The configuration of the target. 
            /// </summary>
            private ImageConfiguration _configuration;
            /// <summary>
            /// The furthest fitness value from the maimum distance from target is the best. 
            /// </summary>
            private readonly int MAXIMUM_FITNESS;
        #endregion members

        #region properties
        #endregion properties

        #region constructors
            public ImageFitnessEvaluator(Configuration configuration)
            {
                _configuration = (configuration as ImageConfiguration);

                MAXIMUM_FITNESS = ((int)Math.Pow(255, 2) * 3);
            }
        #endregion construcors

        #region methods
            public void GetFitnessOfPopulation(List<IGeneticIndividual> population)
            {
                Parallel.ForEach(population, EvaluateIndividual);
            }

            /// <summary>
            /// Use the distance formula to assign a distance to each pixel of the individuals from the targets. 
            /// The total fitness is the average fitness of all the pixels. 
            /// </summary>
            /// <param name="individual"></param>
            private void EvaluateIndividual(IGeneticIndividual individual)
            {
                var target = _configuration.Target;
                var genome = (individual as Individual).Genome;
                double fitness = 0;

                for (int index = 0; index < target.Count; index++)
                {
                    var targetPixel = target[index];
                    var individualPixel = genome[index];

                    float rDistance = (targetPixel.R - individualPixel.R);
                    float gDistance = (targetPixel.G - individualPixel.G);
                    float bDistance = (targetPixel.B - individualPixel.B);


                    float distance = ((rDistance * rDistance) + (gDistance * gDistance) + (bDistance * bDistance));                                    
                                    

                    fitness += Math.Abs(distance - MAXIMUM_FITNESS) + 1;
                }

                individual.Fitness = fitness / target.Count;
            }
        #endregion methods
    }
}
