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
            this.exit = new System.Windows.Forms.Button();
            this.start = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(160, 244);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(112, 28);
            this.exit.TabIndex = 1;
            this.exit.Text = "Zamknij";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 244);
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
            this.output.Location = new System.Drawing.Point(13, 13);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(259, 225);
            this.output.TabIndex = 2;
            this.output.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 284);
            this.Controls.Add(this.output);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.RichTextBox output;
    }
}

