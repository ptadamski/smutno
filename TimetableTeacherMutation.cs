using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    class TimetableTeacherMutation  : IMutation<Timetable, TimetableLocus>
    {
        private IRandomGenerator<double> _randMutationRate;
        private IRandomGenerator<int> _randGenSelector;
        private double _mutationSuccessRate;
        private IDictionary<Prowadzacy, IList<Zajecia>> _prowadzacy;

        public TimetableTeacherMutation(IDictionary<Prowadzacy, IList<Zajecia>> prowadzacy, IRandomGenerator<double> randMutationRate,
                IRandomGenerator<int> randGenSelector, double mutationSuccessRate)
        {
            _prowadzacy = prowadzacy;
            _randMutationRate = randMutationRate;
            _randGenSelector = randGenSelector;
            _mutationSuccessRate = mutationSuccessRate;
        }

        public void Mutate(Timetable chromosome, TimetableLocus locus)
        {
            var list = _prowadzacy[chromosome[locus].Prowadzacy];
            chromosome[locus].Prowadzacy = list[_randGenSelector.Next(list.Count)].Prowadzacy;
        }

        public void TryMutate(Timetable chromosome, TimetableLocus locus)
        {
            if (_randMutationRate.Next() <= _mutationSuccessRate)
                Mutate(chromosome, locus);
        }

        public double SuccessRate
        {
            get { return _mutationSuccessRate; }
        }
    }
}
