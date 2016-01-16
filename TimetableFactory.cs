using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    class TimetableFactory  : IFactory<Timetable, Timetable>
    {
        private IRandomGenerator<int> _randLocus;
        private IMutation<Timetable, TimetableLocus> _mutation;

        public TimetableFactory(IMutation<Timetable, TimetableLocus> mutation, IRandomGenerator<int> randLocus)
        {
            _mutation = mutation;
            _randLocus = randLocus;
            _args = _last = new Timetable(_mutation);
        }

        public Timetable CreateNew(Timetable args)
        {
            return args.Clone();
        }

        public Timetable CreateNew()
        {
            //losowa permutacja planu zajec

            var result = new Timetable(_mutation);

            //---wszystkie przedmioty ktore musza byc zrealizowane przez dane grupy zajeciow--- (moga pochodzic z genomu)
            //var db = new BazaDanychDataContext();
            //var listaObligatoryjnychPrzedmiotow = (from przedmiot in db.Przedmiots
            //                                       join grupa in db.Grupas on new { przedmiot.rok, przedmiot.kierunek } equals new { grupa.rok, grupa.kierunek }
            //                                       select new Zajecia() { Grupa = grupa, Przedmiot = przedmiot, Prowadzacy = null }).ToList();

            foreach (var e in Timetable.Genome)
            {            
                TimetableLocus locus;
                do
                {
                    var czas = _randLocus.Next() % TimetableLocus.MaxTimeLength;
                    var sala = _randLocus.Next() % TimetableLocus.MaxClassRoomCount;
                    locus = new TimetableLocus(czas, sala);
                    if (result[locus] == null)
                    {
                        result[locus] = e;
                        break;
                    }
                } while (true);

                _mutation.Mutate(result, locus);

            }

            return _last = result;
        }

        Timetable _args;
        public Timetable Args
        {
            get
            {
                return _args;
            }
            set
            {
                value = _args;
            }
        }

        Timetable _last;
        public Timetable Last
        {
            get { return _last; }
        }
    }
}
