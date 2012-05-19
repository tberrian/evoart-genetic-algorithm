using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// The concrete genetic algorithm which will attempt to recreate images. 
    /// </summary>
    public class ImageGeneticAlgorithm : GeneticAlgorithm.GeneticAlgorithm
    {
        #region members
        #endregion members

        #region properties
            /// <summary>
            /// Returns the current best individual. 
            /// </summary>
            public Individual BestIndividual
            {
                get { return (Individual)_currentGeneration.First(); }
            }
        #endregion properties

        #region constructors
            /// <summary>
            /// Create the genetic algorithm and inject the correct components. 
            /// </summary>
            /// <param name="configuration"></param>
            public ImageGeneticAlgorithm(Configuration configuration)
                : base(configuration)
            {
                _fitnessFunction = new ImageFitnessEvaluator(configuration);
                _individualComparer = new IndividualComparer();
                _parentSelector = new IndividualParentSelector();
                _individualReproducer = new ImageReproducer();
                _individualMutator = new ImageMutator(configuration);

                CreateInitialPopulation();
                EvaluateFitnessOfPopulation();
            }
            
            /// <summary>
            /// Creates the initial population of individuals, each with a random starting genome. 
            /// </summary>
            protected override void CreateInitialPopulation()
            {
                var configuration = (_configuration as ImageConfiguration);

                for (int index = 0; index < _configuration.PopulationSize; index++)
                {
                    Individual individual = new Individual( configuration.Width * configuration.Height );
                    _currentGeneration.Add(individual);
                }
            }
        #endregion construcors

        #region methods
        #endregion methods
    }
}
