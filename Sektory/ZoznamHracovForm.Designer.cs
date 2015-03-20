namespace WebBrowser.Sektory
{
    partial class ZoznamHracovForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxMeno = new System.Windows.Forms.CheckBox();
            this.checkBoxSektor = new System.Windows.Forms.CheckBox();
            this.checkBoxTyp = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(788, 297);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxMeno
            // 
            this.checkBoxMeno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxMeno.AutoSize = true;
            this.checkBoxMeno.Checked = true;
            this.checkBoxMeno.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMeno.Location = new System.Drawing.Point(135, 319);
            this.checkBoxMeno.Name = "checkBoxMeno";
            this.checkBoxMeno.Size = new System.Drawing.Size(94, 17);
            this.checkBoxMeno.TabIndex = 2;
            this.checkBoxMeno.Text = "Meno + nazov";
            this.checkBoxMeno.UseVisualStyleBackColor = true;
            // 
            // checkBoxSektor
            // 
            this.checkBoxSektor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxSektor.AutoSize = true;
            this.checkBoxSektor.Checked = true;
            this.checkBoxSektor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSektor.Location = new System.Drawing.Point(245, 319);
            this.checkBoxSektor.Name = "checkBoxSektor";
            this.checkBoxSektor.Size = new System.Drawing.Size(57, 17);
            this.checkBoxSektor.TabIndex = 3;
            this.checkBoxSektor.Text = "Sektor";
            this.checkBoxSektor.UseVisualStyleBackColor = true;
            // 
            // checkBoxTyp
            // 
            this.checkBoxTyp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxTyp.AutoSize = true;
            this.checkBoxTyp.Location = new System.Drawing.Point(328, 319);
            this.checkBoxTyp.Name = "checkBoxTyp";
            this.checkBoxTyp.Size = new System.Drawing.Size(81, 17);
            this.checkBoxTyp.TabIndex = 4;
            this.checkBoxTyp.Text = "Typ planety";
            this.checkBoxTyp.UseVisualStyleBackColor = true;
            // 
            // ZoznamHracovForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 344);
            this.Controls.Add(this.checkBoxTyp);
            this.Controls.Add(this.checkBoxSektor);
            this.Controls.Add(this.checkBoxMeno);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ZoznamHracovForm";
            this.Text = "ZoznamHracovForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ZoznamHracovForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxMeno;
        private System.Windows.Forms.CheckBox checkBoxSektor;
        private System.Windows.Forms.CheckBox checkBoxTyp;
    }
}