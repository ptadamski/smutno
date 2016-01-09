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
        bool stop_button = false;

        Series series = new System.Windows.Forms.DataVisualization.Charting.Series
        {
            Name = "Series",
            Color = System.Drawing.Color.Green,
            ChartType = SeriesChartType.Line,
            IsVisibleInLegend = false
        };
        
        MyChromosome primeChromosome;
        IFactory<MyIndividual, MyChromosome> breeder;
        IReproducer<MyIndividual> reproducer;
        ISelector<MyIndividual> selector;
        IFitnessFunc<MyIndividual> fitness;
        IList<MyIndividual> population;
        GA<MyIndividual, MyInt, char> ga;
        int it = 0;

        public Form1()
        {
            InitializeComponent();
            values_init();
            this.wykres.Series.Add(series);
        }

        public void values_init()
        {
            MyChromosome.Genotype = "qwertyuiopasdfghjklzxcvbnm ".ToList();
            primeChromosome = new MyChromosome();
            for (int i = 0; i < 10; i++)
                primeChromosome[new MyInt(i)] = '#';
            //primeChromosome.Randomize();

            breeder = new MyBreeder(primeChromosome);
            reproducer = new CrossOverReproducer<MyIndividual, MyChromosome, MyInt, char>(0.0002, breeder, 1);
            selector = new Roulet<MyIndividual>();
            fitness = new MyFitnessFunc();
            population = new List<MyIndividual>();

            for (int i = 0; i < 100; i++)
                population.Add(breeder.Create());

            it = 0;
            ga = new GA<MyIndividual, MyInt, char>(population, reproducer, selector, fitness, 20);
        }

        public void algorithm_loop()
        {
            this.output.Focus();
           
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
            }, stop_button)) ;

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

            if (!stop_button)
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                this.output.AppendText("" + MyChromosome.Genotype);
            } 
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

            this.id_studenta.Visible = false;
            this.id_prowadźącego.Visible = false;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            stop_button = true;
            this.output.Focus();
            this.start.Visible = true;
            this.restart.Visible = false;
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
        }

        private void start_Click(object sender, EventArgs e)
        {                       
            if(stop_button) stop_button = false;
            this.start.Visible = false;
            this.restart.Visible = true;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            algorithm_loop();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            if (stop_button) stop_button = false;
            values_init();
            this.output.Clear();
            stop_Click(null,null);
            algorithm_loop();
        }

        private void update1_Click(object sender, EventArgs e)
        {
            try
            {
                this.studentTableAdapter.Update(dtas_s383964DataSet1);
                this.studentTableAdapter.Fill(this.dtas_s383964DataSet1.Student);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["id_studenta"].Value = Guid.NewGuid();
        }

        private void update2_Click(object sender, EventArgs e)
        {
            try
            {
                this.prowadzącyTableAdapter.Update(dtas_s383964DataSet1);
                this.prowadzącyTableAdapter.Fill(this.dtas_s383964DataSet1.Prowadzący);
            }          
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dataGridView2_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["id_prowadźącego"].Value = Guid.NewGuid();
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
