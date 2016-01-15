using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public class TimetableLocus
    {
        public TimetableLocus(int time, int classRoom)
        {
            _classRoom = classRoom;
            _time = time;
        }

        private int _time;

        public int Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private int _classRoom;

        public int ClassRoom
        {
            get { return _classRoom; }
            set { _classRoom = value; }
        }   
    }
}
