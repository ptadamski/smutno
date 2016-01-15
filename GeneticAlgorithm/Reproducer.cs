using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    /// <summary>
    /// Operator krzyzowek
    /// </summary>
    class CrossOverReproducer<_Chromosome, _Locus, _Gen> : IReproducer<_Chromosome>
        where _Chromosome : IChromosome<_Locus, _Gen, _Chromosome>
    {                               
 
        //teoretycznie powinna zostac utworzona mapa, dla kazdego rodzica, ile moze miec dzieci
        //chwilo nie bede drobiazgowy => co daje Config
        public class Config
        {
            public int ParentCount { get; set; }
            public int ChildCount { get; set; }
        }

        private IRandomGenerator<int> _randomMate;
        private IFactory<_Chromosome, _Chromosome> _chromosomeFactory;
        //private IMutation<_Chromosome> _mutation;
        Config _config;

        public CrossOverReproducer(IRandomGenerator<int> randomMate, IFactory<_Chromosome, _Chromosome> chromosomeFactory, Config config)
        {
            _randomMate = randomMate;
            _chromosomeFactory = chromosomeFactory;
            //_mutation = mutation;
            _config = config;
        }

        //istnieje konfiguracja mowiaca 
        //*ilu rodzicow jest potrzebnych do stworzenia okreslonej liczby dzieci
        //*ile razy okreslony rodzic moze brac udzial w rozmnazaniu  

        public void PairUp(IList<_Chromosome> individuals, out IDictionary<int, IList<_Chromosome>> intoMates)
        {
            //mozna zrobic kolejke, ktora dopoki nie jest pusta, 
            //wypluwa rodzica do parowania
            //pobiera z generatora kolejnego rodzica (jezeli generator w ogole jakiegos poda) 

            //inicjalizacja "wektora parowania"
            int[] family = new int[individuals.Count];
            for (int i = 0; i < family.Length; i++)
                family[i] = -1;

            //losuje "pary" do "wektora parowania"
            //rodziny tworza klasy abstrakcji wzgledem indeksu rodziny

            for (int i = 0; i < individuals.Count; i++)
                if (family[i] == -1)
                {
                    family[i] = i;

                    for (int j = 1; j < _config.ParentCount; j++)
                    {
                        var r = _randomMate.Next(individuals.Count);
                        while (family[r] != -1)
                            r = _randomMate.Next(individuals.Count);
                        family[r] = i;
                    }
                }
                                                                         
            IList<_Chromosome> mates;
            intoMates = new Dictionary<int, IList<_Chromosome>>(); 
            for (int i = 0; i < individuals.Count; i++)
            {
                if (intoMates.TryGetValue(family[i], out mates))
                {
                    mates.Add(individuals[i]);
                }
                else
                {
                    mates = new List<_Chromosome>();
                    mates.Add(individuals[i]);
                    intoMates.Add(family[i], mates);
                }
            }
        }



        public void Reproduce(out IList<_Chromosome> newPopulation, IList<_Chromosome> fromIndividuals, int uptoCount)
        {
            IDictionary<int, IList<_Chromosome>> mates;
            newPopulation = new List<_Chromosome>();

            PairUp(fromIndividuals, out mates);
                                         
            //dopoki nie osiagnieto uptoCount, dodajemy do newPopulation osobnika, 
            //ktory pochodzi z krzyzowki osobnikow fromIndividuals

            var keys = mates.Keys.ToList();
            for (int i = 0; i < uptoCount; i++)
            {                       
                var groupIndex = _randomMate.Next(keys.Count);
                var chromosome = _chromosomeFactory.CreateNew(mates[keys[groupIndex]].First());
                var indices = new Dictionary<_Locus, int>();
                
                //dwa podejscia rzychodza mi do glowy : 
                //-wypelniamy sloty nie unikatowymi genami
                //-wypelniamy sloty unikatowymi genami
                //mozna to zrealizowac poprzez lambde, delegata, albo przez zewnetrzna implementacje - ja chce zewnetrzna implementacje
                //chce dostac ... zbior par (locus, index) czyli z ktorego rodzica bedzie brany gen na podanym locus

                //wniosek - niech Mix() w pelni decyduje tylko w oparciu o liste rodzicow!
                chromosome.Mix(mates[keys[groupIndex]], _randomMate);
                newPopulation.Add(chromosome);
            }
        }
    }
}
