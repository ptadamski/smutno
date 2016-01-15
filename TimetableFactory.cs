using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    class TimetableFactory  : IFactory<Timetable, Timetable>
    {
        public Timetable CreateNew(Timetable args)
        {
            return args.Clone();
        }

        public Timetable CreateNew()
        {
            return _args.Clone();
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
