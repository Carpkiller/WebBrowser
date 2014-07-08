using System.Drawing;
using System.Windows.Forms;

namespace WebBrowser.ChytanieNN
{
    partial class ChytanieNNForm
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRefresh = new System.Windows.Forms.TextBox();
            this.textBoxCena = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelPoziciaX = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBoxSuradnice = new System.Windows.Forms.CheckBox();
            this.textBoxPoziciaY = new System.Windows.Forms.TextBox();
            this.textBoxPoziciaX = new System.Windows.Forms.TextBox();
            this.labelPoziciaY = new System.Windows.Forms.Label();
            this.textBoxRelatX = new System.Windows.Forms.TextBox();
            this.textBoxRelatY = new System.Windows.Forms.TextBox();
            this.checkBoxSuradniceRelativne = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(388, 84);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(925, 376);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser1_NewWindow);
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(359, 376);
            this.dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(308, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Refresh (s) ";
            // 
            // textBoxRefresh
            // 
            this.textBoxRefresh.Location = new System.Drawing.Point(79, 9);
            this.textBoxRefresh.Name = "textBoxRefresh";
            this.textBoxRefresh.Size = new System.Drawing.Size(48, 20);
            this.textBoxRefresh.TabIndex = 4;
            this.textBoxRefresh.Text = "3";
            // 
            // textBoxCena
            // 
            this.textBoxCena.Location = new System.Drawing.Point(131, 44);
            this.textBoxCena.Name = "textBoxCena";
            this.textBoxCena.Size = new System.Drawing.Size(148, 20);
            this.textBoxCena.TabIndex = 5;
            this.textBoxCena.Text = "100000000";
            this.textBoxCena.TextChanged += new System.EventHandler(this.textBoxCena_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cena planet :";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(778, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Kontrolny klik";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelPoziciaX
            // 
            this.labelPoziciaX.AutoSize = true;
            this.labelPoziciaX.Location = new System.Drawing.Point(925, 12);
            this.labelPoziciaX.Name = "labelPoziciaX";
            this.labelPoziciaX.Size = new System.Drawing.Size(0, 13);
            this.labelPoziciaX.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(308, 48);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(138, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Automaticke kupovanie";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBoxSuradnice
            // 
            this.checkBoxSuradnice.AutoSize = true;
            this.checkBoxSuradnice.Location = new System.Drawing.Point(778, 60);
            this.checkBoxSuradnice.Name = "checkBoxSuradnice";
            this.checkBoxSuradnice.Size = new System.Drawing.Size(137, 17);
            this.checkBoxSuradnice.TabIndex = 10;
            this.checkBoxSuradnice.Text = "Pouzit pevne suradnice";
            this.checkBoxSuradnice.UseVisualStyleBackColor = true;
            // 
            // textBoxPoziciaY
            // 
            this.textBoxPoziciaY.Location = new System.Drawing.Point(998, 57);
            this.textBoxPoziciaY.Name = "textBoxPoziciaY";
            this.textBoxPoziciaY.Size = new System.Drawing.Size(71, 20);
            this.textBoxPoziciaY.TabIndex = 11;
            this.textBoxPoziciaY.Text = "0";
            // 
            // textBoxPoziciaX
            // 
            this.textBoxPoziciaX.Location = new System.Drawing.Point(921, 57);
            this.textBoxPoziciaX.Name = "textBoxPoziciaX";
            this.textBoxPoziciaX.Size = new System.Drawing.Size(71, 20);
            this.textBoxPoziciaX.TabIndex = 12;
            this.textBoxPoziciaX.Text = "0";
            // 
            // labelPoziciaY
            // 
            this.labelPoziciaY.AutoSize = true;
            this.labelPoziciaY.Location = new System.Drawing.Point(995, 12);
            this.labelPoziciaY.Name = "labelPoziciaY";
            this.labelPoziciaY.Size = new System.Drawing.Size(0, 13);
            this.labelPoziciaY.TabIndex = 13;
            // 
            // textBoxRelatX
            // 
            this.textBoxRelatX.Location = new System.Drawing.Point(921, 34);
            this.textBoxRelatX.Name = "textBoxRelatX";
            this.textBoxRelatX.Size = new System.Drawing.Size(71, 20);
            this.textBoxRelatX.TabIndex = 16;
            this.textBoxRelatX.Text = "0";
            // 
            // textBoxRelatY
            // 
            this.textBoxRelatY.Location = new System.Drawing.Point(998, 34);
            this.textBoxRelatY.Name = "textBoxRelatY";
            this.textBoxRelatY.Size = new System.Drawing.Size(71, 20);
            this.textBoxRelatY.TabIndex = 15;
            this.textBoxRelatY.Text = "0";
            // 
            // checkBoxSuradniceRelativne
            // 
            this.checkBoxSuradniceRelativne.AutoSize = true;
            this.checkBoxSuradniceRelativne.Location = new System.Drawing.Point(778, 37);
            this.checkBoxSuradniceRelativne.Name = "checkBoxSuradniceRelativne";
            this.checkBoxSuradniceRelativne.Size = new System.Drawing.Size(147, 17);
            this.checkBoxSuradniceRelativne.TabIndex = 14;
            this.checkBoxSuradniceRelativne.Text = "Pouzit relativne suradnice";
            this.checkBoxSuradniceRelativne.UseVisualStyleBackColor = true;
            // 
            // ChytanieNNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 472);
            this.Controls.Add(this.textBoxRelatX);
            this.Controls.Add(this.textBoxRelatY);
            this.Controls.Add(this.checkBoxSuradniceRelativne);
            this.Controls.Add(this.labelPoziciaY);
            this.Controls.Add(this.textBoxPoziciaX);
            this.Controls.Add(this.textBoxPoziciaY);
            this.Controls.Add(this.checkBoxSuradnice);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelPoziciaX);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCena);
            this.Controls.Add(this.textBoxRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.webBrowser1);
            this.HelpButton = true;
            this.Location = new System.Drawing.Point(100, 10);
            this.Name = "ChytanieNNForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ChytanieNNForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChytanieNNForm_FormClosed);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChytanieNNForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRefresh;
        private System.Windows.Forms.TextBox textBoxCena;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private Label labelPoziciaX;
        private CheckBox checkBox1;
        private CheckBox checkBoxSuradnice;
        private TextBox textBoxPoziciaY;
        private TextBox textBoxPoziciaX;
        private Label labelPoziciaY;
        private TextBox textBoxRelatX;
        private TextBox textBoxRelatY;
        private CheckBox checkBoxSuradniceRelativne;
    }
}