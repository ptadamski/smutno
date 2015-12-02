using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{              
    public class Range<T> : IComparable<Range<T>>
        where T : IComparable
    {
        public Range(T low, T high)
        {
            if (low.CompareTo(high)<=0)
            {
                this.low = low;
                this.high = high;
            }
            else
            {
                this.low = high;
                this.high = low;
            }

        }

        private T low;

        public T Low
        {
            get { return low; }
            set { low = value; }
        }

        private T high;

        public T High
        {
            get { return high; }
            set { high = value; }
        }

        public int CompareTo(Range<T> other)
        {
            if (low.CompareTo(other.high) > 0)   //low >= other.high, return 1
                return 1;
            else if (high.CompareTo(other.low) <= 0) //high <= other.low, return -1
                return -1;
            else 
                return 0; //intersected, return 0
        }
    }

    public class Roulet<Individual> : ISelector<Individual> 
    {
        private static Random selectorRandom = new Random();

        public void Select(IList<Individual> population, IList<float> fitnessFactors,
            int populationLimit, out IList<Individual> selectedPopulation)
        {
            if (population == null || population.Count == 0)
                throw new Exception("NullOrEmpty Population");

            if (fitnessFactors == null || fitnessFactors.Count == 0)
                throw new Exception("NullOrEmpty Fitness");

            var min = fitnessFactors.Min();

            var constant = 1.0f / (population.Count * population.Count);               
            var distribution = fitnessFactors.Select(x => x - min + constant).ToList(); 
            var overall = distribution.Sum();
            var boundry = distribution.Select(x => x / overall);
            var cumulative = new SortedList<Range<float>, Individual>();
			
            //cumulative curve
            float lowerSum = 0.0f, upperSum = 0.0f;
            for (int i = 0; i < boundry.Count; i++)
            {
                lowerSum = upperSum;
                upperSum += boundry[i];
                var range = new Range<float>(lowerSum, upperSum);
                cumulative.Add(range, population[i]);
            }

            selectedPopulation = new List<Individual>();
			
            //random selection
            var randRange = new Range<float>(0.0f,0.0f);
            for (int i = 0; i < populationLimit; i++)
            {                  
                randRange.Low = (float)selectorRandom.NextDouble();
                randRange.High = randRange.Low;
                var individual = cumulative[randRange];
                selectedPopulation.Add(individual);
            }
        }
    }

    /*public class Ranking : ISelector<Field>
    {
        public Ranking(float percentage)
        {
            this.percentage = percentage;
        }

        private float percentage;

        public void Select(IList<Field> population, IList<float> fitnessFactors, 
            int populationLimit, out IList<Field> selectedPopulation)
        {
            if (population == null || population.Count == 0)
                throw new Exception("NullOrEmpty Population");

            if (fitnessFactors == null || fitnessFactors.Count == 0)
                throw new Exception("NullOrEmpty Fitness");

            var sortedPopulation = new SortedList<float, Field>();

            for (int i = 0; i < population.Count; i++)
                sortedPopulation.Add(fitnessFactors[i], population[i]);

            var count = (int)(percentage * sortedPopulation.Count);
            var index = 0;
                                         
            selectedPopulation = new List<Field>(count);

            foreach (var pair in sortedPopulation)
            {
                if (index >= count) break;
                selectedPopulation.Add(pair.Value);
                index++;
            }
        }
    } */
}
