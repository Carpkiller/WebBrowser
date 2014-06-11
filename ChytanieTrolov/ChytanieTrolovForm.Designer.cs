namespace WebBrowser.ChytanieTrolov
{
    partial class ChytanieTrolovForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSila = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMenoHraca = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUni = new System.Windows.Forms.TextBox();
            this.textBoxOrbit = new System.Windows.Forms.TextBox();
            this.textBoxEB = new System.Windows.Forms.TextBox();
            this.textBoxPechota = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxCas = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.webBrowser3 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Kriticka sila";
            // 
            // textBoxSila
            // 
            this.textBoxSila.Location = new System.Drawing.Point(154, 64);
            this.textBoxSila.Name = "textBoxSila";
            this.textBoxSila.Size = new System.Drawing.Size(100, 20);
            this.textBoxSila.TabIndex = 26;
            this.textBoxSila.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Hrac";
            // 
            // textBoxMenoHraca
            // 
            this.textBoxMenoHraca.Location = new System.Drawing.Point(154, 38);
            this.textBoxMenoHraca.Name = "textBoxMenoHraca";
            this.textBoxMenoHraca.Size = new System.Drawing.Size(100, 20);
            this.textBoxMenoHraca.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "EB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Orbity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Uni";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Pechota";
            // 
            // textBoxUni
            // 
            this.textBoxUni.Location = new System.Drawing.Point(154, 116);
            this.textBoxUni.Name = "textBoxUni";
            this.textBoxUni.Size = new System.Drawing.Size(100, 20);
            this.textBoxUni.TabIndex = 19;
            this.textBoxUni.Text = "0";
            // 
            // textBoxOrbit
            // 
            this.textBoxOrbit.Location = new System.Drawing.Point(154, 142);
            this.textBoxOrbit.Name = "textBoxOrbit";
            this.textBoxOrbit.Size = new System.Drawing.Size(100, 20);
            this.textBoxOrbit.TabIndex = 18;
            this.textBoxOrbit.Text = "0";
            // 
            // textBoxEB
            // 
            this.textBoxEB.Location = new System.Drawing.Point(154, 168);
            this.textBoxEB.Name = "textBoxEB";
            this.textBoxEB.Size = new System.Drawing.Size(100, 20);
            this.textBoxEB.TabIndex = 17;
            this.textBoxEB.Text = "0";
            // 
            // textBoxPechota
            // 
            this.textBoxPechota.Location = new System.Drawing.Point(154, 90);
            this.textBoxPechota.Name = "textBoxPechota";
            this.textBoxPechota.Size = new System.Drawing.Size(100, 20);
            this.textBoxPechota.TabIndex = 16;
            this.textBoxPechota.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Spust refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(310, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Typ utoku :";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(313, 89);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(82, 17);
            this.radioButton1.TabIndex = 29;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Partyzansky";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(313, 141);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(70, 17);
            this.radioButton2.TabIndex = 30;
            this.radioButton2.Text = "Dobyvaci";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(313, 115);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(41, 17);
            this.radioButton3.TabIndex = 31;
            this.radioButton3.Text = "Uni";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(305, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Refreshovaci cas";
            // 
            // textBoxCas
            // 
            this.textBoxCas.Location = new System.Drawing.Point(405, 168);
            this.textBoxCas.Name = "textBoxCas";
            this.textBoxCas.Size = new System.Drawing.Size(57, 20);
            this.textBoxCas.TabIndex = 32;
            this.textBoxCas.Text = "3";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(274, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(193, 21);
            this.comboBox1.TabIndex = 34;
            // 
            // webBrowser3
            // 
            this.webBrowser3.Location = new System.Drawing.Point(493, 10);
            this.webBrowser3.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser3.Name = "webBrowser3";
            this.webBrowser3.Size = new System.Drawing.Size(755, 250);
            this.webBrowser3.TabIndex = 35;
            // 
            // ChytanieTrolovForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 272);
            this.Controls.Add(this.webBrowser3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxCas);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxSila);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMenoHraca);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUni);
            this.Controls.Add(this.textBoxOrbit);
            this.Controls.Add(this.textBoxEB);
            this.Controls.Add(this.textBoxPechota);
            this.Controls.Add(this.button1);
            this.Name = "ChytanieTrolovForm";
            this.Text = "ChytanieTrolovForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSila;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMenoHraca;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUni;
        private System.Windows.Forms.TextBox textBoxOrbit;
        private System.Windows.Forms.TextBox textBoxEB;
        private System.Windows.Forms.TextBox textBoxPechota;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxCas;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.WebBrowser webBrowser3;
    }
}