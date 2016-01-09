﻿using System.Windows.Forms.DataVisualization.Charting;

namespace scheduler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.stop = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dtas_s383964DataSet1 = new scheduler.dtas_s383964DataSet();
            this.przedmiotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.przedmiotTableAdapter = new scheduler.dtas_s383964DataSetTableAdapters.PrzedmiotTableAdapter();
            this.prowadzącyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.prowadzącyTableAdapter = new scheduler.dtas_s383964DataSetTableAdapters.ProwadzącyTableAdapter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.restart = new System.Windows.Forms.Button();
            this.wykres = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.studentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.update_1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.prowadzącyBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.update_2 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.nazwaDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rokDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.semestrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kierunekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wykładDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ćwiczeniaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.laboratoriaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.przedmiotBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.nazwaDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.miejscaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.idprowadźącegoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idprzedmiotuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodzajzajęćDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ilośćzajęćDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.przypisanyprzedmiotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Update = new System.Windows.Forms.Button();
            this.studentTableAdapter = new scheduler.dtas_s383964DataSetTableAdapters.StudentTableAdapter();
            this.salaTableAdapter = new scheduler.dtas_s383964DataSetTableAdapters.SalaTableAdapter();
            this.przypisany_przedmiotTableAdapter = new scheduler.dtas_s383964DataSetTableAdapters.Przypisany_przedmiotTableAdapter();
            this.id_prowadźącego = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwaDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tytułDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_studenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indeksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rokDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtas_s383964DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedmiotBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prowadzącyBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wykres)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prowadzącyBindingSource1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedmiotBindingSource1)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salaBindingSource)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.przypisanyprzedmiotBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(153, 237);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(112, 28);
            this.stop.TabIndex = 1;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(6, 237);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(112, 28);
            this.start.TabIndex = 0;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // output
            // 
            this.output.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.output.Location = new System.Drawing.Point(6, 6);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(259, 225);
            this.output.TabIndex = 2;
            this.output.Text = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 271);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(259, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Zatrzymaj po osiągnięciu funkcji ocenu równej:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(504, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Zatrzymaj po wykonaniu i-tej iteracji:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(504, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 7;
            // 
            // dtas_s383964DataSet1
            // 
            this.dtas_s383964DataSet1.DataSetName = "dtas_s383964DataSet";
            this.dtas_s383964DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // przedmiotBindingSource
            // 
            this.przedmiotBindingSource.DataMember = "Przedmiot";
            this.przedmiotBindingSource.DataSource = this.dtas_s383964DataSet1;
            // 
            // przedmiotTableAdapter
            // 
            this.przedmiotTableAdapter.ClearBeforeFill = true;
            // 
            // prowadzącyTableAdapter
            // 
            this.prowadzącyTableAdapter.ClearBeforeFill = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(642, 348);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.restart);
            this.tabPage1.Controls.Add(this.wykres);
            this.tabPage1.Controls.Add(this.output);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.start);
            this.tabPage1.Controls.Add(this.stop);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(634, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Program";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(6, 237);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(112, 28);
            this.restart.TabIndex = 9;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Visible = false;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // wykres
            // 
            chartArea1.Name = "Wykres";
            this.wykres.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.wykres.Legends.Add(legend1);
            this.wykres.Location = new System.Drawing.Point(271, 61);
            this.wykres.Name = "wykres";
            this.wykres.Size = new System.Drawing.Size(333, 233);
            this.wykres.TabIndex = 8;
            this.wykres.Text = "wykres";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(634, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Studenci";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.update_1);
            this.splitContainer2.Size = new System.Drawing.Size(628, 316);
            this.splitContainer2.SplitterDistance = 276;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_studenta,
            this.nazwaDataGridViewTextBoxColumn,
            this.indeksDataGridViewTextBoxColumn,
            this.rokDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.studentBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(628, 276);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_DefaultValuesNeeded);
            // 
            // studentBindingSource
            // 
            this.studentBindingSource.DataMember = "Student";
            this.studentBindingSource.DataSource = this.dtas_s383964DataSet1;
            // 
            // update_1
            // 
            this.update_1.Location = new System.Drawing.Point(513, 2);
            this.update_1.Name = "update_1";
            this.update_1.Size = new System.Drawing.Size(112, 28);
            this.update_1.TabIndex = 10;
            this.update_1.Text = "Update";
            this.update_1.UseVisualStyleBackColor = true;
            this.update_1.Click += new System.EventHandler(this.update1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(634, 322);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Prowadźący";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.update_2);
            this.splitContainer3.Size = new System.Drawing.Size(634, 322);
            this.splitContainer3.SplitterDistance = 281;
            this.splitContainer3.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_prowadźącego,
            this.nazwaDataGridViewTextBoxColumn1,
            this.tytułDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.prowadzącyBindingSource1;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(634, 281);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView2_DefaultValuesNeeded);
            // 
            // prowadzącyBindingSource1
            // 
            this.prowadzącyBindingSource1.DataMember = "Prowadzący";
            this.prowadzącyBindingSource1.DataSource = this.dtas_s383964DataSet1;
            // 
            // update_2
            // 
            this.update_2.Location = new System.Drawing.Point(519, 2);
            this.update_2.Name = "update_2";
            this.update_2.Size = new System.Drawing.Size(112, 28);
            this.update_2.TabIndex = 11;
            this.update_2.Text = "Update";
            this.update_2.UseVisualStyleBackColor = true;
            this.update_2.Click += new System.EventHandler(this.update2_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(634, 322);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Przedmioty";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nazwaDataGridViewTextBoxColumn2,
            this.rokDataGridViewTextBoxColumn1,
            this.semestrDataGridViewTextBoxColumn,
            this.kierunekDataGridViewTextBoxColumn,
            this.wykładDataGridViewTextBoxColumn,
            this.ćwiczeniaDataGridViewTextBoxColumn,
            this.laboratoriaDataGridViewTextBoxColumn});
            this.dataGridView3.DataSource = this.przedmiotBindingSource1;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(634, 322);
            this.dataGridView3.TabIndex = 0;
            // 
            // nazwaDataGridViewTextBoxColumn2
            // 
            this.nazwaDataGridViewTextBoxColumn2.DataPropertyName = "nazwa";
            this.nazwaDataGridViewTextBoxColumn2.HeaderText = "nazwa";
            this.nazwaDataGridViewTextBoxColumn2.Name = "nazwaDataGridViewTextBoxColumn2";
            // 
            // rokDataGridViewTextBoxColumn1
            // 
            this.rokDataGridViewTextBoxColumn1.DataPropertyName = "rok";
            this.rokDataGridViewTextBoxColumn1.HeaderText = "rok";
            this.rokDataGridViewTextBoxColumn1.Name = "rokDataGridViewTextBoxColumn1";
            // 
            // semestrDataGridViewTextBoxColumn
            // 
            this.semestrDataGridViewTextBoxColumn.DataPropertyName = "semestr";
            this.semestrDataGridViewTextBoxColumn.HeaderText = "semestr";
            this.semestrDataGridViewTextBoxColumn.Name = "semestrDataGridViewTextBoxColumn";
            // 
            // kierunekDataGridViewTextBoxColumn
            // 
            this.kierunekDataGridViewTextBoxColumn.DataPropertyName = "kierunek";
            this.kierunekDataGridViewTextBoxColumn.HeaderText = "kierunek";
            this.kierunekDataGridViewTextBoxColumn.Name = "kierunekDataGridViewTextBoxColumn";
            // 
            // wykładDataGridViewTextBoxColumn
            // 
            this.wykładDataGridViewTextBoxColumn.DataPropertyName = "wykład";
            this.wykładDataGridViewTextBoxColumn.HeaderText = "wykład";
            this.wykładDataGridViewTextBoxColumn.Name = "wykładDataGridViewTextBoxColumn";
            // 
            // ćwiczeniaDataGridViewTextBoxColumn
            // 
            this.ćwiczeniaDataGridViewTextBoxColumn.DataPropertyName = "ćwiczenia";
            this.ćwiczeniaDataGridViewTextBoxColumn.HeaderText = "ćwiczenia";
            this.ćwiczeniaDataGridViewTextBoxColumn.Name = "ćwiczeniaDataGridViewTextBoxColumn";
            // 
            // laboratoriaDataGridViewTextBoxColumn
            // 
            this.laboratoriaDataGridViewTextBoxColumn.DataPropertyName = "laboratoria";
            this.laboratoriaDataGridViewTextBoxColumn.HeaderText = "laboratoria";
            this.laboratoriaDataGridViewTextBoxColumn.Name = "laboratoriaDataGridViewTextBoxColumn";
            // 
            // przedmiotBindingSource1
            // 
            this.przedmiotBindingSource1.DataMember = "Przedmiot";
            this.przedmiotBindingSource1.DataSource = this.dtas_s383964DataSet1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dataGridView4);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(634, 322);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Sale";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AutoGenerateColumns = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nazwaDataGridViewTextBoxColumn3,
            this.typDataGridViewTextBoxColumn,
            this.miejscaDataGridViewTextBoxColumn});
            this.dataGridView4.DataSource = this.salaBindingSource;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(634, 322);
            this.dataGridView4.TabIndex = 0;
            // 
            // nazwaDataGridViewTextBoxColumn3
            // 
            this.nazwaDataGridViewTextBoxColumn3.DataPropertyName = "nazwa";
            this.nazwaDataGridViewTextBoxColumn3.HeaderText = "nazwa";
            this.nazwaDataGridViewTextBoxColumn3.Name = "nazwaDataGridViewTextBoxColumn3";
            // 
            // typDataGridViewTextBoxColumn
            // 
            this.typDataGridViewTextBoxColumn.DataPropertyName = "typ";
            this.typDataGridViewTextBoxColumn.HeaderText = "typ";
            this.typDataGridViewTextBoxColumn.Name = "typDataGridViewTextBoxColumn";
            // 
            // miejscaDataGridViewTextBoxColumn
            // 
            this.miejscaDataGridViewTextBoxColumn.DataPropertyName = "miejsca";
            this.miejscaDataGridViewTextBoxColumn.HeaderText = "miejsca";
            this.miejscaDataGridViewTextBoxColumn.Name = "miejscaDataGridViewTextBoxColumn";
            // 
            // salaBindingSource
            // 
            this.salaBindingSource.DataMember = "Sala";
            this.salaBindingSource.DataSource = this.dtas_s383964DataSet1;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dataGridView5);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(634, 322);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Przypisania przedmiotów";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dataGridView5
            // 
            this.dataGridView5.AutoGenerateColumns = false;
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idprowadźącegoDataGridViewTextBoxColumn,
            this.idprzedmiotuDataGridViewTextBoxColumn,
            this.rodzajzajęćDataGridViewTextBoxColumn,
            this.ilośćzajęćDataGridViewTextBoxColumn});
            this.dataGridView5.DataSource = this.przypisanyprzedmiotBindingSource;
            this.dataGridView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView5.Location = new System.Drawing.Point(3, 3);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(628, 316);
            this.dataGridView5.TabIndex = 0;
            // 
            // idprowadźącegoDataGridViewTextBoxColumn
            // 
            this.idprowadźącegoDataGridViewTextBoxColumn.DataPropertyName = "id_prowadźącego";
            this.idprowadźącegoDataGridViewTextBoxColumn.HeaderText = "id_prowadźącego";
            this.idprowadźącegoDataGridViewTextBoxColumn.Name = "idprowadźącegoDataGridViewTextBoxColumn";
            // 
            // idprzedmiotuDataGridViewTextBoxColumn
            // 
            this.idprzedmiotuDataGridViewTextBoxColumn.DataPropertyName = "id_przedmiotu";
            this.idprzedmiotuDataGridViewTextBoxColumn.HeaderText = "id_przedmiotu";
            this.idprzedmiotuDataGridViewTextBoxColumn.Name = "idprzedmiotuDataGridViewTextBoxColumn";
            // 
            // rodzajzajęćDataGridViewTextBoxColumn
            // 
            this.rodzajzajęćDataGridViewTextBoxColumn.DataPropertyName = "rodzaj_zajęć";
            this.rodzajzajęćDataGridViewTextBoxColumn.HeaderText = "rodzaj_zajęć";
            this.rodzajzajęćDataGridViewTextBoxColumn.Name = "rodzajzajęćDataGridViewTextBoxColumn";
            // 
            // ilośćzajęćDataGridViewTextBoxColumn
            // 
            this.ilośćzajęćDataGridViewTextBoxColumn.DataPropertyName = "ilość_zajęć";
            this.ilośćzajęćDataGridViewTextBoxColumn.HeaderText = "ilość_zajęć";
            this.ilośćzajęćDataGridViewTextBoxColumn.Name = "ilośćzajęćDataGridViewTextBoxColumn";
            // 
            // przypisanyprzedmiotBindingSource
            // 
            this.przypisanyprzedmiotBindingSource.DataMember = "Przypisany_przedmiot";
            this.przypisanyprzedmiotBindingSource.DataSource = this.dtas_s383964DataSet1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(150, 100);
            this.splitContainer1.TabIndex = 0;
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(0, 0);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(75, 23);
            this.Update.TabIndex = 0;
            // 
            // studentTableAdapter
            // 
            this.studentTableAdapter.ClearBeforeFill = true;
            // 
            // salaTableAdapter
            // 
            this.salaTableAdapter.ClearBeforeFill = true;
            // 
            // przypisany_przedmiotTableAdapter
            // 
            this.przypisany_przedmiotTableAdapter.ClearBeforeFill = true;
            // 
            // id_prowadźącego
            // 
            this.id_prowadźącego.DataPropertyName = "id";
            this.id_prowadźącego.HeaderText = "id";
            this.id_prowadźącego.Name = "id_prowadźącego";
            // 
            // nazwaDataGridViewTextBoxColumn1
            // 
            this.nazwaDataGridViewTextBoxColumn1.DataPropertyName = "nazwa";
            this.nazwaDataGridViewTextBoxColumn1.HeaderText = "nazwa";
            this.nazwaDataGridViewTextBoxColumn1.Name = "nazwaDataGridViewTextBoxColumn1";
            // 
            // tytułDataGridViewTextBoxColumn
            // 
            this.tytułDataGridViewTextBoxColumn.DataPropertyName = "tytuł";
            this.tytułDataGridViewTextBoxColumn.HeaderText = "tytuł";
            this.tytułDataGridViewTextBoxColumn.Name = "tytułDataGridViewTextBoxColumn";
            // 
            // id_studenta
            // 
            this.id_studenta.DataPropertyName = "id";
            this.id_studenta.HeaderText = "id";
            this.id_studenta.Name = "id_studenta";
            this.id_studenta.Visible = false;
            // 
            // nazwaDataGridViewTextBoxColumn
            // 
            this.nazwaDataGridViewTextBoxColumn.DataPropertyName = "nazwa";
            this.nazwaDataGridViewTextBoxColumn.HeaderText = "nazwa";
            this.nazwaDataGridViewTextBoxColumn.Name = "nazwaDataGridViewTextBoxColumn";
            // 
            // indeksDataGridViewTextBoxColumn
            // 
            this.indeksDataGridViewTextBoxColumn.DataPropertyName = "indeks";
            this.indeksDataGridViewTextBoxColumn.HeaderText = "indeks";
            this.indeksDataGridViewTextBoxColumn.Name = "indeksDataGridViewTextBoxColumn";
            // 
            // rokDataGridViewTextBoxColumn
            // 
            this.rokDataGridViewTextBoxColumn.DataPropertyName = "rok";
            this.rokDataGridViewTextBoxColumn.HeaderText = "rok";
            this.rokDataGridViewTextBoxColumn.Name = "rokDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 348);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtas_s383964DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedmiotBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prowadzącyBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wykres)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prowadzącyBindingSource1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.przedmiotBindingSource1)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salaBindingSource)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.przypisanyprzedmiotBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private dtas_s383964DataSet dtas_s383964DataSet1;
        private System.Windows.Forms.BindingSource przedmiotBindingSource;
        private dtas_s383964DataSetTableAdapters.PrzedmiotTableAdapter przedmiotTableAdapter;
        private System.Windows.Forms.BindingSource prowadzącyBindingSource;
        private dtas_s383964DataSetTableAdapters.ProwadzącyTableAdapter prowadzącyTableAdapter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.BindingSource studentBindingSource;
        private dtas_s383964DataSetTableAdapters.StudentTableAdapter studentTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource prowadzącyBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn rokDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn semestrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kierunekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wykładDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ćwiczeniaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn laboratoriaDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource przedmiotBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.BindingSource salaBindingSource;
        private dtas_s383964DataSetTableAdapters.SalaTableAdapter salaTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn typDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn miejscaDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dataGridView5;
        private System.Windows.Forms.BindingSource przypisanyprzedmiotBindingSource;
        private dtas_s383964DataSetTableAdapters.Przypisany_przedmiotTableAdapter przypisany_przedmiotTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idprowadźącegoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idprzedmiotuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodzajzajęćDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ilośćzajęćDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataVisualization.Charting.Chart wykres;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button update_1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button update_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_studenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indeksDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rokDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_prowadźącego;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwaDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tytułDataGridViewTextBoxColumn;
    }
}

