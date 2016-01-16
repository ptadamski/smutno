using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public class TimetableLocus
    {
        private static int _maxTimeLength;
        private static int _maxClassRooomCount;

        public static int MaxTimeLength { get { return _maxTimeLength; } }
        public static int MaxClassRoomCount { get { return _maxClassRooomCount; } }

        public static void SetBoundry(int maxTimeLength, int maxClassRooomCount) 
        {
            _maxTimeLength=maxTimeLength  ;
            _maxClassRooomCount = maxClassRooomCount;
        }

        public TimetableLocus(int time, int classRoom)
        {
            _classRoom = classRoom;
            _time = time;
        }

        private int _time;

        public int Time
        {
            get { return _time; }
            set { _time = value % _maxTimeLength; }
        }

        private int _classRoom;

        public int ClassRoom
        {
            get { return _classRoom; }
            set { _classRoom = value % _maxClassRooomCount; }
        }

        public override bool Equals(object obj)
        {
            TimetableLocus t = (TimetableLocus)obj;
            return _time == t._time && _classRoom == t._classRoom;
        }
    }
}
