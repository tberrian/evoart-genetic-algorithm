using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgorithm;

namespace ArtCreator.ImageGeneticAlgorithm
{
    /// <summary>
    /// Uses fitness proportionate selection to return a parent. 
    /// </summary>
    public class IndividualParentSelector : IParentSelector
    {
        #region const members
            private static Random RNG = new Random();
        #endregion

        #region members
            /// <summary>
            /// The summed fitness of the entire population. 
            /// </summary>
            private double _totalPopulationFitness;
        #endregion members

        #region properties
        #endregion properties

        #region constructors
        #endregion construcors

        #region methods

            /// <summary>
            /// Returns a parent selected based on it's genetic fitness. 
            /// </summary>
            /// <param name="population"></param>
            /// <returns></returns>
            public void Initalize(List<IGeneticIndividual> population)
            {
                FindTotalPopulationFitness(population);
                NormalizePopulationFitness(population);
                SortPopulation(population);
                AccumulateNormalizedFitnessValues(population);
            }

            /// <summary>
            /// Sums the total fitness of the population 
            /// </summary>
            /// <param name="population"></param>
            private void FindTotalPopulationFitness(List<IGeneticIndividual> population)
            {
                _totalPopulationFitness = 0;
                population.ForEach(o => _totalPopulationFitness += o.Fitness);
            }

            /// <summary>
            /// Normalize the total populations fitness. 
            /// </summary>
            /// <param name="population"></param>
            private void NormalizePopulationFitness(List<IGeneticIndividual> population)
            {
                foreach (var individual in population)
                {
                    individual.Fitness = (individual.Fitness / _totalPopulationFitness);
                }
            }

            /// <summary>
            /// Sort the new normalized values in a descending order. 
            /// </summary>
            /// <param name="population"></param>
            private void SortPopulation(List<IGeneticIndividual> population)
            {
                population.Sort(new IndividualComparer());
            }

            /// <summary>
            /// Set the fitness value of the individual to the sum of the previous individuals and itself. 
            /// </summary>
            /// <param name="population"></param>
            private void AccumulateNormalizedFitnessValues(List<IGeneticIndividual> population)
            {
                List<double> accumulatedFitnessValues = new List<double>();

                for (int index = 0; index < population.Count; index++)
                {
                    double sum = 0;

                    for (int children = index; children < population.Count; children++)
                        sum += population[children].Fitness;

                    accumulatedFitnessValues.Add(sum);
                }

                SetPopulationFitnessToAccumulatedValues(population, accumulatedFitnessValues);
            }

            /// <summary>
            /// Sets the populations fitness values to the new accumulated normalized values. 
            /// </summary>
            /// <param name="population"></param>
            /// <param name="accumulatedFitnessValues"></param>
            private void SetPopulationFitnessToAccumulatedValues(List<IGeneticIndividual> population, List<double> accumulatedFitnessValues)
            {
                //Must be in descending order
                accumulatedFitnessValues.Reverse();

                for (int index = 0; index < population.Count; index++)
                    population[index].Fitness = accumulatedFitnessValues[index];
            }

            /// <summary>
            /// Returns a parent selected based on it's genetic fitness. 
            /// </summary>
            /// <param name="population"></param>
            /// <returns></returns>
            public IGeneticIndividual SelectParent(List<IGeneticIndividual> population)
            {
                bool parentFound = false;
                double diceRoll = RNG.NextDouble();

                while (!parentFound)
                {
                    int randomIndex = RNG.Next(0, population.Count);
                    var individual = population[randomIndex];

                    if (individual.Fitness >= diceRoll)
                        return individual;
                }

                throw new Exception("Should never be here");
            }

        #endregion methods
    }
}
