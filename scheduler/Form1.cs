using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GeneticAlgorithm;

namespace scheduler
{
    public partial class Form1 : Form
    {
        public bool temp = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dtas_s383964DataSet1.Przypisany_przedmiot' table. You can move, or remove it, as needed.
            this.przypisany_przedmiotTableAdapter.Fill(this.dtas_s383964DataSet1.Przypisany_przedmiot);
            // TODO: This line of code loads data into the 'dtas_s383964DataSet1.Sala' table. You can move, or remove it, as needed.
            this.salaTableAdapter.Fill(this.dtas_s383964DataSet1.Sala);
            // TODO: This line of code loads data into the 'dtas_s383964DataSet1.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.dtas_s383964DataSet1.Student);
            // TODO: This line of code loads data into the 'dtas_s383964DataSet1.Prowadzący' table. You can move, or remove it, as needed.
            this.prowadzącyTableAdapter.Fill(this.dtas_s383964DataSet1.Prowadzący);
            // TODO: This line of code loads data into the 'dtas_s383964DataSet1.Przedmiot' table. You can move, or remove it, as needed.
            this.przedmiotTableAdapter.Fill(this.dtas_s383964DataSet1.Przedmiot);           
        }

        private void stop_Click(object sender, EventArgs e)
        {
            temp = true;  
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.output.Focus();

            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Series",
                Color = System.Drawing.Color.Green,
                ChartType = SeriesChartType.Line,
                IsVisibleInLegend = false
            };
            this.wykres.Series.Add(series);

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
            this.output.AppendText("START\n");
            while (ga.Iterate(() =>
            {
                Application.DoEvents();

                if (it % 10 == 0) series.Points.AddXY(it, 100);
                this.output.AppendText(" iteracja nr " + it++ + " - ");
                          
                foreach (var locus in ga.Population[0].Chromosome.Loci)
                {
                    this.output.AppendText("" + ga.Population[0].Chromosome[locus]);
                }
                this.output.AppendText("\n");

                return true;
            }, temp));

            foreach (var item in ga.Population)
            {
                if (fitness.Fit(item) > 0.0f)
                {
                    foreach (var locus in item.Chromosome.Loci)
                    {
                        this.output.AppendText("" + item.Chromosome[locus]);

                    }
                    this.output.AppendText("\n");
                }
            }

             this.output.AppendText("" + MyChromosome.Genotype);
            
            if (temp) Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void output_TextChanged(object sender, EventArgs e)
        {

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
