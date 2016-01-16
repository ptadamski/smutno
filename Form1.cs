﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

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

        const int MaxPopulationCount = 100;

        IList<Timetable> population;
        IMutation<Timetable, TimetableLocus> mutation;
        IEvaluator<double, Timetable> evalSelector;
        IReproducer<Timetable> reproducer;
        ISelector<Timetable> selector;

        int it = 0;

        public Form1()
        {
            InitializeComponent();
            values_init();
            this.wykres.Series.Add(series);
            this.comboBox1.SelectedItem = "zimowy";
            Text = Application.ExecutablePath;
        }

        static void InitializeFromDataBase(BazaDanychDataContext db, string semestr, out IList<Zajecia> genotyp,
            out IDictionary<Przedmiot, IList<Prowadzący>> prowadzacyZajecia)
        {
            prowadzacyZajecia = new Dictionary<Przedmiot, IList<Prowadzący>>();

            //filtr przedmiotow
            var przedmiotyWSemestrze = db.Przedmiots.Select(x => x).Where(x => x.semestr.Equals(semestr)).ToList();

            //ustalenie granic dla locus
            var wszystkieSale = db.Salas.Select(x => x).ToList();
            TimetableLocus.SetBoundry(8 * 14, wszystkieSale.Count);

            //genotyp = (from przydzial in db.Przypisany_przedmiots
            //         join prowadzacy in db.Prowadzacies on przydzial.id_prowadzacego equals prowadzacy.id
            //         join przedmiot in przedmiotyWSemestrze on przydzial.id_przedmiotu equals przedmiot.id
            //         join grupa in db.Grupas on new {przedmiot.kierunek, przedmiot.rok} equals new { grupa.kierunek, grupa.rok }
            //         select new Zajecia() {Grupa=grupa, Prowadzacy=prowadzacy, Przedmiot=przedmiot} ).ToList();   


            //do ustalenia genotypu
            var l = new { db.Przedmiots.First().rok, db.Przedmiots.First().kierunek };
            var listaObligatoryjnychPrzedmiotow = (from przedmiot in przedmiotyWSemestrze
                                                   join grupa in db.Grupas on new { przedmiot.rok, przedmiot.kierunek } equals new { grupa.rok, grupa.kierunek }
                                                   select new { Grupa = grupa, Przedmiot = przedmiot }).ToList();

            genotyp = new List<Zajecia>();
            foreach (var e in listaObligatoryjnychPrzedmiotow)
            {
                for (int i = 0, length = e.Przedmiot.ćwiczenia / 15; i < length; i++)
                    genotyp.Add(new Zajecia() { Typ = TypZajec.Cwiczenia, Grupa = e.Grupa, Przedmiot = e.Przedmiot, Prowadzacy = null, Index = i });

                for (int i = 0, length = e.Przedmiot.laboratoria / 15; i < length; i++)
                    genotyp.Add(new Zajecia() { Typ = TypZajec.Laboratoria, Grupa = e.Grupa, Przedmiot = e.Przedmiot, Prowadzacy = null, Index = i });
            }


            //do mutacji                   
            var prowadzacyWszyscy = db.Prowadzącies.Select(x => x).ToList();
            var n = 0;
            foreach (var e in przedmiotyWSemestrze)
            {
                var przypisaneZajecia = (from przydzial in db.Przypisany_przedmiots
                                         where przydzial.Przedmiot.Equals(e)
                                         select przydzial.Prowadzący).ToList();
                prowadzacyZajecia.Add(e, przypisaneZajecia);
                n = n + przypisaneZajecia.Count;
            }
        }

        public void values_init()
        {
            //---pobranie danych z bazy danych---    
            var db = new BazaDanychDataContext();
            IDictionary<Przedmiot, IList<Prowadzący>> prowadzacyZajecia;
            IList<Zajecia> genotyp;

            InitializeFromDataBase(db, "zimowy", out genotyp, out prowadzacyZajecia);
            Application.DoEvents();

            //---przypisanie genotypu---
            Timetable.Genome = genotyp;

            //---randomy---
            DoubleRandomGenerator randMutation = new DoubleRandomGenerator();
            DoubleRandomGenerator randSelector = new DoubleRandomGenerator();
            IntegerRandomGenerator randGen = new IntegerRandomGenerator();
            IntegerRandomGenerator randMateUp = new IntegerRandomGenerator();
            IntegerRandomGenerator randGenSelector = new IntegerRandomGenerator();


            //---komponenty algorytmu---                                               
            population = new List<Timetable>();
            mutation = new TimetableTeacherMutation(prowadzacyZajecia, randMutation, randGenSelector, 0.002);
            IFactory<Timetable, Timetable> breeder = new TimetableFactory(mutation, randMateUp);
            evalSelector = new TimetableEvaluator();

            //Timetable primeChromosome = breeder.CreateNew();

            reproducer = new CrossOverReproducer<Timetable, TimetableLocus, Zajecia>(randMateUp, breeder, new CrossOverReproducer<Timetable, TimetableLocus, Zajecia>.Config() { ChildCount = 2, ParentCount = 2 });
            selector = new RouletSelector<Timetable, TimetableLocus, Zajecia>(evalSelector, randSelector);

            //---inicjacja populacji---  (calosciowy plan dla uczelni)
            Application.DoEvents();
            for (int i = 0; i < MaxPopulationCount; i++)
                population.Add(breeder.CreateNew());


            //moze zrobic tak...
            //populacje duzych planow rozpic na N populacjie mniejszych planow

            //dla kazdego duzego planu
            //duzy plan uzyskany z populacji rozbic na mniejsze juz teraz
            //stworzyc populacje pomiejszych planow
            //przeprowadzic iteracje GA
            //potem wszystkie te plany skonkatenowac 
            //odtworzyc populacje duzych planow
            //i ocenic...

            it = 0;

            this.comboBox1.Enabled = true;
            this.dataGridView1.ReadOnly = false; this.dataGridView1.AllowUserToDeleteRows = true;
            this.dataGridView2.ReadOnly = false; this.dataGridView2.AllowUserToDeleteRows = true;
            this.dataGridView3.ReadOnly = false; this.dataGridView3.AllowUserToDeleteRows = true;
            this.dataGridView4.ReadOnly = false; this.dataGridView4.AllowUserToDeleteRows = true;
            this.dataGridView5.ReadOnly = false; this.dataGridView5.AllowUserToDeleteRows = true;
        }

        public void algorithm_loop()
        {
            this.output.Focus();
            this.output.AppendText("START\n");

            do
            {               
                if (it % 5 == 0) series.Points.AddXY(it, population.Select(x => evalSelector.Evaluate(x)).Max());
                this.output.AppendText(" iteracja nr " + it++ + "\n");

                //wydzielenie podzbiorow            
                var planyGrup = new Dictionary<Grupa, IList<Timetable>>();

                foreach (var osobnik in population)
                {
                    var lista_grup = osobnik.Gens.Select(x => x.Grupa).Distinct();
                    foreach (var grupa in lista_grup)
                    {
                        Application.DoEvents();

                        var planGrupy = new Timetable(mutation);

                        IList<TimetableLocus> loci =
                            (from locus in osobnik.Loci
                             where osobnik[locus].Grupa.Equals(grupa)
                             select locus).ToList();

                        foreach (var locus in loci)
                            planGrupy[locus] = osobnik[locus];

                        IList<Timetable> lista;
                        if (!planyGrup.TryGetValue(grupa, out lista))
                            lista = new List<Timetable>();

                        lista.Add(planGrupy);
                    }
                }

                var listaGrup = planyGrup.Keys.ToList();

                for (int i = 0; i < listaGrup.Count; i++)
                {
                    Application.DoEvents();

                    IGeneticAlgorithmEngine<Timetable> ga_grupy;
                    ga_grupy = new GeneticAlgorithmEngine<Timetable, TimetableLocus, Zajecia>(planyGrup[listaGrup[i]], selector, reproducer, 20);
                    ga_grupy.Iterate(() =>
                    {
                        //planGrupy.Value = ga_grupy.Population;                       
                        return ga_grupy.Population.Select(x => evalSelector.Evaluate(x)).Average() > 0.95;
                    });
                    planyGrup[listaGrup[i]] = ga_grupy.Population;
                }

                //polaczenie podzbiorow
                var nowaPopulacja = new List<Timetable>();
                for (int i = 0; i < population.Count; i++)
                {
                    Application.DoEvents();

                    var osobnik = new Timetable(mutation);
                    foreach (var planGrupy in planyGrup)
                    {
                        osobnik.Concat(planGrupy.Value[i]);
                    }
                }
                

            } while ((population.Select(x => evalSelector.Evaluate(x)).Average() < 0.95) && !stop_button);

            if (!stop_button)
            {
                //wypisanie wyniku

                this.output.AppendText("ZAKOŃCZONO");
                stop_Click(null, null);
                series.SetDefault(true);
            }
            else
            {
                try
                {
                    output.AppendText("STOP\n");
                }
                catch (Exception){}              
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
            this.id_przedmiotu.Visible = false;
            this.id_sali.Visible = false;
            this.id_przypisania.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_Click(null, null);
        }

        private void stop_Click(object sender, EventArgs e)
        {
            stop_button = true;
            this.output.Focus();
            this.start.Visible = true;
            this.restart.Visible = false;
            this.eksport.Enabled = true;
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (!stop_button)
            {
                values_init();

                this.wykres.Series[0].Points.Clear();
                //to gówno nieczyści
            }
            else stop_button = false;

            this.comboBox1.Enabled = false;
            this.start.Visible = false;
            this.restart.Visible = true;
            this.eksport.Enabled = false;
            this.dataGridView1.ReadOnly = true; this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView2.ReadOnly = true; this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView3.ReadOnly = true; this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView4.ReadOnly = true; this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView5.ReadOnly = true; this.dataGridView5.AllowUserToDeleteRows = false;            
            algorithm_loop();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            values_init();
            this.output.Clear();
            stop_Click(null, null);
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

        private void update3_Click(object sender, EventArgs e)
        {
            try
            {
                this.przedmiotTableAdapter.Update(dtas_s383964DataSet1);
                this.przedmiotTableAdapter.Fill(this.dtas_s383964DataSet1.Przedmiot);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dataGridView3_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["id_przedmiotu"].Value = Guid.NewGuid();
        }

        private void update4_Click(object sender, EventArgs e)
        {
            try
            {
                this.salaTableAdapter.Update(dtas_s383964DataSet1);
                this.salaTableAdapter.Fill(this.dtas_s383964DataSet1.Sala);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dataGridView4_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["id_sali"].Value = Guid.NewGuid();
        }

        private void update5_Click(object sender, EventArgs e)
        {
            try
            {
                this.przypisany_przedmiotTableAdapter.Update(dtas_s383964DataSet1);
                this.przypisany_przedmiotTableAdapter.Fill(this.dtas_s383964DataSet1.Przypisany_przedmiot);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dataGridView5_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["id_przypisania"].Value = Guid.NewGuid();
        }

        private void eksport_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                excel_eksport(path);
            }
        }

        public void excel_eksport(string path)
        {
            MemoryStream ms = new MemoryStream();

            IWorkbook workbook = new HSSFWorkbook();//Create an excel Workbook
            ISheet sheet = workbook.CreateSheet();//Create a work table in the table
            IRow headerRow = sheet.CreateRow(0); //To add a row in the table

            foreach (DataGridViewColumn column in dataGridView1.Columns) headerRow.CreateCell(column.Index).SetCellValue(column.HeaderText);

            int rowIndex = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (row.Cells[column.Index].Value == null)
                    {
                        dataRow.CreateCell(column.Index).SetCellValue("");
                    }
                    else dataRow.CreateCell(column.Index).SetCellValue(row.Cells[column.Index].Value.ToString());
                }
                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;


            //MemoryStream ms = ExcelHelper.DataToExcel(dt);
            FileStream fs = new FileStream(path + "\\test.xls", FileMode.Create);
            ms.WriteTo(fs);
            fs.Close();
            ms.Close();
        }
    }
}
