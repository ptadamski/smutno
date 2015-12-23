using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows.Forms;
using GeneticAlgorithm;

namespace scheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main(String[] args)
        {
            MyChromosome.Genotype = "qwertyuiopasdfghjklzxcvbnm ".ToList();
            MyChromosome primeChromosome = new MyChromosome();
            for (int i = 0; i < 10; i++)
                primeChromosome[new MyInt(i)] = '#';
            //primeChromosome.Randomize();

            MyBreeder breeder = new MyBreeder(primeChromosome);
            IReproducer<MyIndividual> reproducer = new CrossOverReproducer<MyIndividual, MyChromosome, MyInt, char>(0.0002, breeder, 1);
            ISelector<MyIndividual> selector = new Roulet<MyIndividual>(); ;
            IFitnessFunc<MyIndividual> fitness = new MyFitnessFunc();
            IList<MyIndividual> population = new List<MyIndividual>();

            for (int i = 0; i < 100; i++)
                population.Add(breeder.Create());

            GA<MyIndividual, MyChromosome, MyInt, char> ga = new GA<MyIndividual, MyChromosome, MyInt, char>(population, reproducer, selector, fitness, 20);
            int it = 0;
            ga.Iterate(() => {
                Console.WriteLine("iteracja nr ", it);
                return false; });

            Console.WriteLine(MyChromosome.Genotype);
            Console.ReadKey();


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }

    class MyBreeder : GeneticAlgorithm.IFactory<MyIndividual, MyChromosome>
    {
        MyChromosome _args = null;
        MyIndividual _last = null;

        public MyBreeder(MyChromosome args)
        {
            _args = args;    
        }

        public MyIndividual Create()
        {
            _last = new MyIndividual();
            _last.Chromosome.Concatenate(_args);
            _last.Chromosome.Randomize();
            return _last;
        }

        public MyIndividual Create(MyChromosome args)
        {
            _last = new MyIndividual();
            _last.Chromosome.Concatenate(args);
            _last.Chromosome.Randomize();
            return _last;
        }

        public MyChromosome Args
        {
            get
            {
                return _args;
            }
            set
            {
                _args = value;
            }
        }

        public MyIndividual Last
        {
            get { return _last; }
        }        
    }
               
    class MyChromosome : GeneticAlgorithm.Chromosome<MyInt, char> { }

    class MyIndividual : GeneticAlgorithm.IIndividual<MyChromosome>
    {          
        private MyChromosome _chromosome = new MyChromosome();

        public MyChromosome Chromosome
        {
            get
            {
                return _chromosome;
            }
            set
            {
                _chromosome = value;
            }
        }
                                                 
        public object Clone()
        {
            return new MyIndividual() { Chromosome = this.Chromosome };
        }
    }

    class MyInt : Algebra.IArithmetic<MyInt>
    {             
        public MyInt()
        {
            Value = 0;
        }

        public MyInt(int x)
        {
            Value = x;
        }

        public int Value { get; set; }

        public MyInt Add(MyInt other)
        {
            Value += other.Value;          
            return this; ;
        }

        public MyInt Sub(MyInt other)
        {
            Value -= other.Value;
            return this; ;
        }

        public MyInt Mul(MyInt other)
        {
            Value *= other.Value;
            return this; ;
        }

        public MyInt Div(MyInt other)
        {
            Value /= other.Value;
            return this; ;
        }

        public int CompareTo(MyInt other)
        {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(MyInt x, MyInt y)
        {
            return x.Value == y.Value;
        }

        public bool Equals(MyInt x)
        {
            return x.Value == Value;
        }

        public int GetHashCode(MyInt obj)
        {
            return Value+1;
        }
    }

    class MyFitnessFunc : GeneticAlgorithm.IFitnessFunc<MyIndividual>
    {
        public float Fit(MyIndividual individual) 
        {                                   
            //zawiera podciag "ala" 
            float result = 0.0f;
            var chromosome = individual.Chromosome;
            for (int i = 0, length=chromosome.Loci.Count; i < length-2; i++)
            {
                if (chromosome[new MyInt(i)] == 'a')
                    if(chromosome[new MyInt(i+1)] == 'l')
                        if(chromosome[new MyInt(i+2)] == 'a')
                            result = 1.0f;
            }
            return result;
        }
    }

}
