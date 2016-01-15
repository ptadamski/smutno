using System;
using System.Linq;
using System.Collections.Generic;

namespace scheduler
{
    public class Zajecia
    {       
        public override bool Equals(object x)
        {
            Zajecia obj = (Zajecia)x;
            return Przedmiot.id == obj.Przedmiot.id  && Grupa.id == obj.Grupa.id;// && Prowadzacy.id == obj.Przedmiot.id;
        }

        public override int GetHashCode()
        {
            return Przedmiot.id.GetHashCode() + Prowadzacy.id.GetHashCode() + Grupa.id.GetHashCode();
        }

        public Przedmiot Przedmiot { get; set; }
        public Prowadzacy Prowadzacy { get; set; }
        public Grupa Grupa { get; set; }
        public Sala Sala { get; set; }
        public int Godzina { get; set; }
    }
}