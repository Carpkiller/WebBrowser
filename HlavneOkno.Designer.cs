namespace WebBrowser
{
    partial class HlavneOkno
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
            this.Login = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.PredchadzajuciSektor = new System.Windows.Forms.Button();
            this.NasledujuciSektor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonVyvrhel = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vytvoritZalohuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chytanieTrolovToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hviezdneBranyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odstranitStareZaznamyZDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineDatabazaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSpojeniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stiahnutDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.odPoslednehoStiahnutiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadnutDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odZaciatkuVekuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odPoslednehoUploaduToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nastaveniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moznostiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelInfoPlanety = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.warModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(642, 32);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 0;
            this.Login.Text = "Login";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(72, 33);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(228, 20);
            this.username.TabIndex = 3;
            this.username.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(374, 35);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(247, 20);
            this.password.TabIndex = 5;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 61);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(907, 586);
            this.webBrowser1.TabIndex = 6;
            this.webBrowser1.Url = new System.Uri("http://www.stargate-game.cz/index.php", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // PredchadzajuciSektor
            // 
            this.PredchadzajuciSektor.Location = new System.Drawing.Point(723, 33);
            this.PredchadzajuciSektor.Name = "PredchadzajuciSektor";
            this.PredchadzajuciSektor.Size = new System.Drawing.Size(127, 23);
            this.PredchadzajuciSektor.TabIndex = 7;
            this.PredchadzajuciSektor.Text = "Predchadzajuci sektor";
            this.PredchadzajuciSektor.UseVisualStyleBackColor = true;
            this.PredchadzajuciSektor.Click += new System.EventHandler(this.button2_Click);
            // 
            // NasledujuciSektor
            // 
            this.NasledujuciSektor.Location = new System.Drawing.Point(856, 33);
            this.NasledujuciSektor.Name = "NasledujuciSektor";
            this.NasledujuciSektor.Size = new System.Drawing.Size(122, 23);
            this.NasledujuciSektor.TabIndex = 8;
            this.NasledujuciSektor.Text = "Nasledujuci sektor";
            this.NasledujuciSektor.UseVisualStyleBackColor = true;
            this.NasledujuciSektor.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(925, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Zmapovane sektory :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(928, 241);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(422, 406);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(928, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(422, 32);
            this.button1.TabIndex = 13;
            this.button1.Text = "Dohazdovanie";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1533, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(53, 61);
            this.button2.TabIndex = 14;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1533, 116);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(53, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(928, 150);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 62);
            this.button3.TabIndex = 16;
            this.button3.Text = "Hladanie vsetkeho mozneho";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(1219, 150);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 62);
            this.button4.TabIndex = 17;
            this.button4.Text = "Stavba PO";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(1081, 150);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 62);
            this.button5.TabIndex = 18;
            this.button5.Text = "Chytanie NN";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonVyvrhel
            // 
            this.buttonVyvrhel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonVyvrhel.Location = new System.Drawing.Point(1218, 70);
            this.buttonVyvrhel.Name = "buttonVyvrhel";
            this.buttonVyvrhel.Size = new System.Drawing.Size(131, 62);
            this.buttonVyvrhel.TabIndex = 19;
            this.buttonVyvrhel.Text = "Chytanie VV";
            this.buttonVyvrhel.UseVisualStyleBackColor = true;
            this.buttonVyvrhel.Click += new System.EventHandler(this.buttonVyvrhel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.onlineDatabazaToolStripMenuItem,
            this.nastaveniaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1362, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vytvoritZalohuToolStripMenuItem,
            this.chytanieTrolovToolStripMenuItem,
            this.warModToolStripMenuItem,
            this.hviezdneBranyToolStripMenuItem,
            this.odstranitStareZaznamyZDBToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // vytvoritZalohuToolStripMenuItem
            // 
            this.vytvoritZalohuToolStripMenuItem.Name = "vytvoritZalohuToolStripMenuItem";
            this.vytvoritZalohuToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.vytvoritZalohuToolStripMenuItem.Text = "Naskenovat vesmir";
            this.vytvoritZalohuToolStripMenuItem.Click += new System.EventHandler(this.vytvoritZalohuToolStripMenuItem_Click);
            // 
            // chytanieTrolovToolStripMenuItem
            // 
            this.chytanieTrolovToolStripMenuItem.Name = "chytanieTrolovToolStripMenuItem";
            this.chytanieTrolovToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.chytanieTrolovToolStripMenuItem.Text = "Chytanie trolov";
            this.chytanieTrolovToolStripMenuItem.Click += new System.EventHandler(this.chytanieTrolovToolStripMenuItem_Click);
            // 
            // hviezdneBranyToolStripMenuItem
            // 
            this.hviezdneBranyToolStripMenuItem.Name = "hviezdneBranyToolStripMenuItem";
            this.hviezdneBranyToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.hviezdneBranyToolStripMenuItem.Text = "Hviezdne brany";
            this.hviezdneBranyToolStripMenuItem.Click += new System.EventHandler(this.hviezdneBranyToolStripMenuItem_Click);
            // 
            // odstranitStareZaznamyZDBToolStripMenuItem
            // 
            this.odstranitStareZaznamyZDBToolStripMenuItem.Name = "odstranitStareZaznamyZDBToolStripMenuItem";
            this.odstranitStareZaznamyZDBToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.odstranitStareZaznamyZDBToolStripMenuItem.Text = "Odstranit stare zaznamy z DB";
            this.odstranitStareZaznamyZDBToolStripMenuItem.Click += new System.EventHandler(this.odstranitStareZaznamyZDBToolStripMenuItem_Click);
            // 
            // onlineDatabazaToolStripMenuItem
            // 
            this.onlineDatabazaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSpojeniaToolStripMenuItem,
            this.stiahnutDataToolStripMenuItem,
            this.uploadnutDataToolStripMenuItem});
            this.onlineDatabazaToolStripMenuItem.Name = "onlineDatabazaToolStripMenuItem";
            this.onlineDatabazaToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.onlineDatabazaToolStripMenuItem.Text = "Online databaza";
            // 
            // testSpojeniaToolStripMenuItem
            // 
            this.testSpojeniaToolStripMenuItem.Name = "testSpojeniaToolStripMenuItem";
            this.testSpojeniaToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.testSpojeniaToolStripMenuItem.Text = "Test spojenia";
            this.testSpojeniaToolStripMenuItem.Click += new System.EventHandler(this.testSpojeniaToolStripMenuItem_Click);
            // 
            // stiahnutDataToolStripMenuItem
            // 
            this.stiahnutDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.odPoslednehoStiahnutiaToolStripMenuItem});
            this.stiahnutDataToolStripMenuItem.Name = "stiahnutDataToolStripMenuItem";
            this.stiahnutDataToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.stiahnutDataToolStripMenuItem.Text = "Stiahnut data";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
            this.toolStripMenuItem1.Text = "Od zaciatku veku";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // odPoslednehoStiahnutiaToolStripMenuItem
            // 
            this.odPoslednehoStiahnutiaToolStripMenuItem.Name = "odPoslednehoStiahnutiaToolStripMenuItem";
            this.odPoslednehoStiahnutiaToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.odPoslednehoStiahnutiaToolStripMenuItem.Text = "Od posledneho stiahnutia";
            this.odPoslednehoStiahnutiaToolStripMenuItem.Click += new System.EventHandler(this.odPoslednehoStiahnutiaToolStripMenuItem_Click);
            // 
            // uploadnutDataToolStripMenuItem
            // 
            this.uploadnutDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.odZaciatkuVekuToolStripMenuItem,
            this.odPoslednehoUploaduToolStripMenuItem});
            this.uploadnutDataToolStripMenuItem.Name = "uploadnutDataToolStripMenuItem";
            this.uploadnutDataToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.uploadnutDataToolStripMenuItem.Text = "Uploadnut data";
            // 
            // odZaciatkuVekuToolStripMenuItem
            // 
            this.odZaciatkuVekuToolStripMenuItem.Name = "odZaciatkuVekuToolStripMenuItem";
            this.odZaciatkuVekuToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.odZaciatkuVekuToolStripMenuItem.Text = "Od zaciatku veku";
            this.odZaciatkuVekuToolStripMenuItem.Click += new System.EventHandler(this.odZaciatkuVekuToolStripMenuItem_Click);
            // 
            // odPoslednehoUploaduToolStripMenuItem
            // 
            this.odPoslednehoUploaduToolStripMenuItem.Name = "odPoslednehoUploaduToolStripMenuItem";
            this.odPoslednehoUploaduToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.odPoslednehoUploaduToolStripMenuItem.Text = "Od posledneho uploadu";
            this.odPoslednehoUploaduToolStripMenuItem.Click += new System.EventHandler(this.odPoslednehoUploaduToolStripMenuItem_Click);
            // 
            // nastaveniaToolStripMenuItem
            // 
            this.nastaveniaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moznostiToolStripMenuItem});
            this.nastaveniaToolStripMenuItem.Name = "nastaveniaToolStripMenuItem";
            this.nastaveniaToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.nastaveniaToolStripMenuItem.Text = "Nastavenia";
            // 
            // moznostiToolStripMenuItem
            // 
            this.moznostiToolStripMenuItem.Name = "moznostiToolStripMenuItem";
            this.moznostiToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.moznostiToolStripMenuItem.Text = "Moznosti";
            this.moznostiToolStripMenuItem.Click += new System.EventHandler(this.moznostiToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(925, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pocet novych planet  : ";
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(1042, 225);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 11;
            // 
            // labelInfoPlanety
            // 
            this.labelInfoPlanety.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfoPlanety.AutoSize = true;
            this.labelInfoPlanety.Location = new System.Drawing.Point(1101, 86);
            this.labelInfoPlanety.Name = "labelInfoPlanety";
            this.labelInfoPlanety.Size = new System.Drawing.Size(0, 13);
            this.labelInfoPlanety.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(925, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Zaciatok veku  : ";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(1019, 112);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(189, 20);
            this.dateTimePicker1.TabIndex = 22;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1362, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // warModToolStripMenuItem
            // 
            this.warModToolStripMenuItem.Name = "warModToolStripMenuItem";
            this.warModToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.warModToolStripMenuItem.Text = "War mod";
            this.warModToolStripMenuItem.Click += new System.EventHandler(this.warModToolStripMenuItem_Click);
            // 
            // HlavneOkno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 672);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelInfoPlanety);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonVyvrhel);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NasledujuciSektor);
            this.Controls.Add(this.PredchadzajuciSektor);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HlavneOkno";
            this.Text = "Hlavne okno";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button PredchadzajuciSektor;
        private System.Windows.Forms.Button NasledujuciSektor;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox username;
        public System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonVyvrhel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vytvoritZalohuToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelInfoPlanety;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ToolStripMenuItem chytanieTrolovToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem onlineDatabazaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSpojeniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stiahnutDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadnutDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nastaveniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moznostiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem odPoslednehoStiahnutiaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odZaciatkuVekuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odPoslednehoUploaduToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hviezdneBranyToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem odstranitStareZaznamyZDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warModToolStripMenuItem;
    }
}

