using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{

    //class Zajecia 
    //{
    //    public Przedmiot Przedmiot { get; set; }
    //    public Prowadzący Prowadzacy { get; set; }
    //    public Grupa Grupa { get; set; }
    //};

    //class LocusZajecia
    //{
    //    private static HashSet<Prowadzący> _prowadzacy = new HashSet<Prowadzący>();
    //    private static HashSet<Grupa> _grupy = new HashSet<Grupa>();

    //    private Prowadzący prowadzący;

    //    public Prowadzący Prowadzący
    //    {
    //        get { return prowadzący; }
    //        set
    //        {
    //            if (!_prowadzacy.Contains(value))
    //                _prowadzacy.Add(value);
    //            prowadzący = value;
    //        }
    //    }

    //    private Grupa grupa;

    //    public Grupa Grupa
    //    {
    //        get { return grupa; }
    //        set
    //        {
    //            if (!_grupy.Contains(value))
    //                _grupy.Add(value);
    //            grupa = value;
    //        }
    //    }
    //}

    //class ZajeciaGenerator  : GeneticAlgorithm.Chromosome<KeyValuePair<Grupa, Prowadzący>, Przedmiot>
    //{
    //    public ZajeciaGenerator()
    //    {
    //    }

    //    static Random randProwadzacy = new Random();

    //    IEnumerable<Zajecia> x()
    //    {
    //        BazaDanychDataContext db = new BazaDanychDataContext();

    //        var wymagane_przedmioty =
    //            (from p in db.Przedmiots
    //            join g in db.Grupas
    //            on new { p.rok, p.kierunek } equals new { g.rok, g.kierunek }
    //            select new { Przedmiot = p, Grupa = g });

    //        var znani_prowadzacy =
    //            (from p in db.Prowadzącies
    //             select p).ToList();    

    //        foreach (var pair in wymagane_przedmioty)
    //        {                                                           
    //            var r = randProwadzacy.Next(znani_prowadzacy.Count);
    //            yield return new Zajecia(){Grupa=pair.Grupa, Przedmiot=pair.Przedmiot, Prowadzacy=znani_prowadzacy[r]};
    //        }
    //    }

    //}
}
