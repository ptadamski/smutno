using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{

    public class TimetableEvaluator : IEvaluator<double, Timetable>
    { //to do: evaluation tree

        int IleRazyWTymSamymCzasie<T>(IDictionary<T, IList<TimetableLocus>> prowadzacy)
        {                                                                    
                                                     
            //(prowadzacy, czas, licznik)    
            var licznik = new Dictionary<T, Dictionary<int, int>>();
            Dictionary<int, int> slownik;

            foreach (var e in prowadzacy.Keys)
            {
                if (!licznik.TryGetValue(e, out slownik))
                {
                    slownik = new Dictionary<int, int>();
                    licznik.Add(e, slownik);
                }
                foreach (var locus in prowadzacy[e])
                {
                    if (slownik.ContainsKey(locus.Time))
                        slownik[locus.Time] += 1;
                    else
                        slownik.Add(locus.Time, 1);
                }
            }

            var wynik = new Dictionary<T, int>();

            foreach (var e in prowadzacy.Keys)
                wynik[e] =  licznik[e].Values.Select(x => x).Where(x => x > 1).Sum();

            return wynik.Select(x=>x.Value).Sum();
        }

        public double Evaluate(Timetable plan)
        {
            var prowadzacy = new Dictionary<Prowadzący, IList<TimetableLocus>>();
            var grupy = new Dictionary<Grupa, IList<TimetableLocus>>();

            foreach (var locus in plan.Loci)
            {
                IList<TimetableLocus> lista_miejsc;
                if (!prowadzacy.TryGetValue(plan[locus].Prowadzacy, out lista_miejsc))
                {                              
                    lista_miejsc = new List<TimetableLocus>();
                    prowadzacy.Add(plan[locus].Prowadzacy, lista_miejsc);
                }
                lista_miejsc.Add(locus);


                if (!grupy.TryGetValue(plan[locus].Grupa, out lista_miejsc))
                {
                    lista_miejsc = new List<TimetableLocus>();
                    grupy.Add(plan[locus].Grupa, lista_miejsc);
                }
                lista_miejsc.Add(locus);
            }


            Double konfliktyProwadzacych = IleRazyWTymSamymCzasie<Prowadzący>(prowadzacy);
            Double konfliktyGrup = IleRazyWTymSamymCzasie<Grupa>(grupy);

            if (plan.Loci.Count>0)  
                return 1.0 - (konfliktyGrup + konfliktyProwadzacych) / plan.Loci.Count;

            return -1.0;

            //zlicz okienka prowadzacego
            //zlicz okienka grupy
        }
    }


    //pominiety -  na obecna potrzebe chwili
    public class TimetablePopupationEvaluator : IEvaluator<bool, IList<Timetable>>
    {
        TimetableEvaluator _chromosomeEvaluator;

        public TimetablePopupationEvaluator(TimetableEvaluator chromosomeEvaluator)
        {
            _chromosomeEvaluator = chromosomeEvaluator;
        }

        public bool Evaluate(IList<Timetable> item)
        {
            var result = item.Average(x => _chromosomeEvaluator.Evaluate(x));
            return result >= 0.95;
        }
    }
}
