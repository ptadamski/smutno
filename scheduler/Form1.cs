using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticAlgorithm;

namespace scheduler
{
    public partial class Form1 : Form
    {
        public bool temp = false;
        public bool temp2 = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            temp = true;  
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.output.Focus();

            MyChromosome.Genotype = "qwertyuiopasdfghjklzxcvbnm ".ToList();
            MyChromosome primeChromosome = new MyChromosome();
            for (int i = 0; i < 10; i++)
                primeChromosome[new MyInt(i)] = '#';
            //primeChromosome.Randomize();

            IFactory<MyIndividual, MyChromosome> breeder = new MyBreeder(primeChromosome);
            IReproducer<MyIndividual> reproducer = new CrossOverReproducer<MyIndividual, MyChromosome, MyInt, char>(0.0002, breeder, 1);
            ISelector<MyIndividual> selector = new Roulet<MyIndividual>(); ;
            IFitnessFunc<MyIndividual> fitness = new MyFitnessFunc();
            IList<MyIndividual> population = new List<MyIndividual>();

            for (int i = 0; i < 100; i++)
                population.Add(breeder.Create());

            GA<MyIndividual, MyInt, char> ga = new GA<MyIndividual, MyInt, char>(population, reproducer, selector, fitness, 20);
            int it = 0;
            Console.WriteLine("zaczawszy");
            this.output.AppendText("zaczawszy\n");
            while (ga.Iterate(() =>
            {
                Application.DoEvents();

                Console.WriteLine(" iteracja nr {0} ", it++);
                this.output.AppendText(" iteracja nr " + it + "\n");
                
                foreach (var locus in ga.Population[0].Chromosome.Loci)
                {
                    Console.Write("{0}", ga.Population[0].Chromosome[locus]);
                }
                Console.WriteLine();

                return true;
            }, temp));

            foreach (var item in ga.Population)
            {
                if (fitness.Fit(item) > 0.0f)
                {
                    foreach (var locus in item.Chromosome.Loci)
                    {
                        Console.Write("{0}", item.Chromosome[locus]);

                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine(MyChromosome.Genotype);
            
            if (temp) Application.Exit();
        }
    }

    class MyBreeder : IFactory<MyIndividual, MyChromosome>
    {
        private MyChromosome _args;
        private MyIndividual _last;

        public MyBreeder(MyChromosome args)
        {
            _args = args;
        }

        public MyIndividual Create()
        {
            return Create(_args);
        }

        public MyIndividual Create(MyChromosome args)
        {
            MyIndividual obj = new MyIndividual(null);
            var chromosome = new MyChromosome();
            chromosome.Concatenate(args);
            chromosome.Randomize();
            obj.Chromosome = chromosome;
            return obj;
        }

        public MyChromosome Args
        {
            get { return _args; }
            set { _args = value; }
        }

        public MyIndividual Last { get { return _last; } }
    }

    class MyChromosome : Chromosome<MyInt, char> { }

    class MyIndividual : Individual<MyInt, char>
    {
        public MyIndividual(IChromosome<MyInt, char> chromosome) :
            base(chromosome)
        {
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

        public override bool Equals(object x)
        {
            return ((MyInt)x).Value == Value;
        }

        public override int GetHashCode()
        {
            return Value + 1;
        }
    }

    class MyFitnessFunc : GeneticAlgorithm.IFitnessFunc<MyIndividual>
    {
        public float Fit(MyIndividual individual)
        {
            //zawiera podciag "ala" 
            float result = 0.0f;
            var chromosome = individual.Chromosome;
            for (int i = 0, length = chromosome.Loci.Count; i < length - 2; i++)
            {
                if (chromosome[new MyInt(i)] == 'a')
                    if (chromosome[new MyInt(i + 1)] == 'l')
                        if (chromosome[new MyInt(i + 2)] == 'a')
                            result = 1.0f;
            }
            return result;
        }
    }
}
