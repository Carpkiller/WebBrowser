using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WebBrowser.Dohadzovanie
{
    public partial class InfoHrac : Form
    {
        public bool Cancelled = true;
        public HracPodmienky hrac;
        private string meno;

        public InfoHrac(string menoHraca, List<HracPodmienky> listPodm)
        {
            InitializeComponent();
            textBox5.Text = menoHraca;
            this.meno = menoHraca;

            foreach (var hracPodmienky in listPodm)
            {
                if (hracPodmienky.Meno == menoHraca)
                {
                    textBox1.Text = hracPodmienky.Jedn1_RO.ToString();
                    textBox4.Text = hracPodmienky.Jedn2_Uni.ToString();
                    textBox3.Text = hracPodmienky.Jedn3_Orb.ToString();
                    textBox2.Text = hracPodmienky.Jedn4_EB.ToString(); 
                    textBox6.Text = hracPodmienky.KritickaSila.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.hrac = new HracPodmienky(meno,int.Parse(textBox6.Text),int.Parse(textBox1.Text),int.Parse(textBox4.Text),int.Parse(textBox3.Text),int.Parse(textBox2.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
