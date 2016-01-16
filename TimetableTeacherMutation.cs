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
        private IDictionary<Przedmiot, IList<Prowadzacy>> _prowadzacyZajecia;

        public TimetableTeacherMutation(IDictionary<Przedmiot, IList<Prowadzacy>> prowadzacyZajecia, IRandomGenerator<double> randMutationRate,
                IRandomGenerator<int> randGenSelector, double mutationSuccessRate)
        {
            _prowadzacyZajecia = prowadzacyZajecia;
            _randMutationRate = randMutationRate;
            _randGenSelector = randGenSelector;
            _mutationSuccessRate = mutationSuccessRate;
        }

        public void Mutate(Timetable chromosome, TimetableLocus locus)
        {
            var list = _prowadzacyZajecia[chromosome[locus].Przedmiot];
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
