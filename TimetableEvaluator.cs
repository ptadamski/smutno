using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{

    public class TimetableEvaluator : IEvaluator<double, Timetable>
    { //to do: evaluation tree
        public double Evaluate(Timetable item)
        {
            return 0.0;
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
