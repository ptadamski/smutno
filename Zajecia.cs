using System;
using System.Linq;
using System.Collections.Generic;

namespace scheduler
{
    public enum TypZajec { Laboratoria, Cwiczenia }; 

    public class Zajecia
    {       
        public override bool Equals(object x)
        {
            Zajecia obj = (Zajecia)x;
            return Przedmiot.id == obj.Przedmiot.id  && Grupa.id == obj.Grupa.id 
                && obj.Typ == Typ && Index == obj.Index;// && Prowadzacy.id == obj.Przedmiot.id;
        }

        public override int GetHashCode()
        {
            return Przedmiot.id.GetHashCode() + Grupa.id.GetHashCode();// +Prowadzacy.id.GetHashCode();
        }

        public Przedmiot Przedmiot { get; set; }
        public Prowadz¹cy Prowadzacy { get; set; }
        public Grupa Grupa { get; set; }
        public int Index { get; set; }
        public TypZajec Typ { get; set; }
        //public Sala Sala { get; set; }
        //public int Godzina { get; set; }
    }
}