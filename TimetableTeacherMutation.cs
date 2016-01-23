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
        private IDictionary<Przedmiot, IList<Prowadzący>> _prowadzacyZajecia;

        public TimetableTeacherMutation(IDictionary<Przedmiot, IList<Prowadzący>> prowadzacyZajecia, IRandomGenerator<double> randMutationRate,
                IRandomGenerator<int> randGenSelector, double mutationSuccessRate)
        {
            _prowadzacyZajecia = prowadzacyZajecia;
            _randMutationRate = randMutationRate;
            _randGenSelector = randGenSelector;
            _mutationSuccessRate = mutationSuccessRate;
        }

        public void Mutate(Timetable chromosome, TimetableLocus locus)
        {
            var t = chromosome[locus].Przedmiot;
            var r = _prowadzacyZajecia.Where(x => x.Key.Equals(t)).ToList();
            var list = _prowadzacyZajecia[t];
            //wybrac prowadzacego ktory ma te zajecia!!
            chromosome[locus].Prowadzacy = list[_randGenSelector.Next(list.Count)];
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
