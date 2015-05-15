namespace WebBrowser.OnlineStatistiky
{
    partial class OnlineStatistikyForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelPocetPlanet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Potvrdit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(12, 22);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(82, 13);
            this.labelText.TabIndex = 1;
            this.labelText.Text = "Pocet planet od";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(452, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // labelPocetPlanet
            // 
            this.labelPocetPlanet.AutoSize = true;
            this.labelPocetPlanet.Location = new System.Drawing.Point(244, 22);
            this.labelPocetPlanet.Name = "labelPocetPlanet";
            this.labelPocetPlanet.Size = new System.Drawing.Size(13, 13);
            this.labelPocetPlanet.TabIndex = 3;
            this.labelPocetPlanet.Text = "0";
            // 
            // OnlineStatistikyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 285);
            this.Controls.Add(this.labelPocetPlanet);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.button1);
            this.Name = "OnlineStatistikyForm";
            this.Text = "OnlineStatistiky";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelPocetPlanet;
    }
}