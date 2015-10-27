namespace WebBrowser.Hladanie
{
    partial class HladanieForm
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
            this.buttonPlaneta = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonHrac = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRasa = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBoxRasaSucasna = new System.Windows.Forms.ComboBox();
            this.comboBoxRasaPovodna = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxRasaTypPlanety = new System.Windows.Forms.ComboBox();
            this.comboBoxTypPlanety = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonPlaneta
            // 
            this.buttonPlaneta.Location = new System.Drawing.Point(373, 30);
            this.buttonPlaneta.Name = "buttonPlaneta";
            this.buttonPlaneta.Size = new System.Drawing.Size(75, 23);
            this.buttonPlaneta.TabIndex = 0;
            this.buttonPlaneta.Text = "Hladaj";
            this.buttonPlaneta.UseVisualStyleBackColor = true;
            this.buttonPlaneta.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nazov planety :";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(199, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Zrus";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Planety hraca :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(118, 69);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 20);
            this.textBox2.TabIndex = 5;
            // 
            // buttonHrac
            // 
            this.buttonHrac.Location = new System.Drawing.Point(373, 72);
            this.buttonHrac.Name = "buttonHrac";
            this.buttonHrac.Size = new System.Drawing.Size(75, 23);
            this.buttonHrac.TabIndex = 4;
            this.buttonHrac.Text = "Zobraz z DB";
            this.buttonHrac.UseVisualStyleBackColor = true;
            this.buttonHrac.Click += new System.EventHandler(this.button3_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 289);
            this.progressBar1.Maximum = 200;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(461, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Planety rasy :";
            // 
            // buttonRasa
            // 
            this.buttonRasa.Location = new System.Drawing.Point(373, 110);
            this.buttonRasa.Name = "buttonRasa";
            this.buttonRasa.Size = new System.Drawing.Size(75, 23);
            this.buttonRasa.TabIndex = 8;
            this.buttonRasa.Text = "Zobraz z DB";
            this.buttonRasa.UseVisualStyleBackColor = true;
            this.buttonRasa.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(118, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 10;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(246, 110);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 11;
            // 
            // comboBoxRasaSucasna
            // 
            this.comboBoxRasaSucasna.FormattingEnabled = true;
            this.comboBoxRasaSucasna.Location = new System.Drawing.Point(246, 183);
            this.comboBoxRasaSucasna.Name = "comboBoxRasaSucasna";
            this.comboBoxRasaSucasna.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRasaSucasna.TabIndex = 15;
            // 
            // comboBoxRasaPovodna
            // 
            this.comboBoxRasaPovodna.FormattingEnabled = true;
            this.comboBoxRasaPovodna.Location = new System.Drawing.Point(246, 149);
            this.comboBoxRasaPovodna.Name = "comboBoxRasaPovodna";
            this.comboBoxRasaPovodna.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRasaPovodna.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Dobyte planety";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(373, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Zobraz z DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Povodna rasa";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Sucasna rasa";
            // 
            // comboBoxRasaTypPlanety
            // 
            this.comboBoxRasaTypPlanety.FormattingEnabled = true;
            this.comboBoxRasaTypPlanety.Location = new System.Drawing.Point(246, 219);
            this.comboBoxRasaTypPlanety.Name = "comboBoxRasaTypPlanety";
            this.comboBoxRasaTypPlanety.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRasaTypPlanety.TabIndex = 21;
            // 
            // comboBoxTypPlanety
            // 
            this.comboBoxTypPlanety.FormattingEnabled = true;
            this.comboBoxTypPlanety.Location = new System.Drawing.Point(118, 218);
            this.comboBoxTypPlanety.Name = "comboBoxTypPlanety";
            this.comboBoxTypPlanety.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTypPlanety.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Typ planety z rasy";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(373, 219);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 18;
            this.button3.Text = "Zobraz z DB";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // HladanieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 324);
            this.Controls.Add(this.comboBoxRasaTypPlanety);
            this.Controls.Add(this.comboBoxTypPlanety);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxRasaSucasna);
            this.Controls.Add(this.comboBoxRasaPovodna);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonRasa);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.buttonHrac);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonPlaneta);
            this.Name = "HladanieForm";
            this.Text = "HladanieForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HladanieForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlaneta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonHrac;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRasa;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBoxRasaSucasna;
        private System.Windows.Forms.ComboBox comboBoxRasaPovodna;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxRasaTypPlanety;
        private System.Windows.Forms.ComboBox comboBoxTypPlanety;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
    }
}