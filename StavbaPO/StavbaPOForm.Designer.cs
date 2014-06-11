﻿namespace WebBrowser.StavbaPO
{
    partial class StavbaPOForm
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
            this.textBoxBS = new System.Windows.Forms.TextBox();
            this.labelBS = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSDI = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(13, 64);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(822, 507);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://www.stargate-game.cz/stavby.php", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // textBoxBS
            // 
            this.textBoxBS.Location = new System.Drawing.Point(124, 6);
            this.textBoxBS.Name = "textBoxBS";
            this.textBoxBS.Size = new System.Drawing.Size(100, 20);
            this.textBoxBS.TabIndex = 1;
            this.textBoxBS.Text = "500";
            // 
            // labelBS
            // 
            this.labelBS.AutoSize = true;
            this.labelBS.Location = new System.Drawing.Point(12, 9);
            this.labelBS.Name = "labelBS";
            this.labelBS.Size = new System.Drawing.Size(52, 13);
            this.labelBS.TabIndex = 2;
            this.labelBS.Text = "Pocet BS";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(729, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PocetPO";
            // 
            // textBoxPO
            // 
            this.textBoxPO.Location = new System.Drawing.Point(323, 38);
            this.textBoxPO.Name = "textBoxPO";
            this.textBoxPO.Size = new System.Drawing.Size(100, 20);
            this.textBoxPO.TabIndex = 4;
            this.textBoxPO.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pocet SDI";
            // 
            // textBoxSDI
            // 
            this.textBoxSDI.Location = new System.Drawing.Point(124, 38);
            this.textBoxSDI.Name = "textBoxSDI";
            this.textBoxSDI.Size = new System.Drawing.Size(100, 20);
            this.textBoxSDI.TabIndex = 6;
            this.textBoxSDI.Text = "500";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(276, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox1.Size = new System.Drawing.Size(64, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Max PO";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // StavbaPOForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 582);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSDI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPO);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelBS);
            this.Controls.Add(this.textBoxBS);
            this.Controls.Add(this.webBrowser1);
            this.Name = "StavbaPOForm";
            this.Text = "StavbaPOForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox textBoxBS;
        private System.Windows.Forms.Label labelBS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSDI;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}