using System;
using System.Linq;
using System.Windows.Forms;
using WebBrowser.Planety;
using WebBrowser.Sektory;

namespace WebBrowser.Hladanie
{
    public partial class HladanieForm : Form
    {
        private readonly Jadro _jadro;
        private string _hladanyItem;

        public HladanieForm()
        {
            InitializeComponent();
            comboBox1.DataSource = RasyId.ListId.Keys.ToList();
        }

        public HladanieForm(Jadro jadro)
        {
            _jadro = jadro;
            _jadro.ZmenaPoradiaHladania += UpdateProgressBar;
            _jadro.KoniecVlakna += KoniecVlakana;
            InitializeComponent();
            comboBox1.DataSource = RasyId.ListId.Keys.ToList();
        }

        public void KoniecVlakana()
        {
            Invoke((MethodInvoker)(() =>
            {
                var title = "Hladana planeta : "+_hladanyItem;
                var detailPlanety = new PlanetaDetail(_jadro.KoniecVlaknaList, _jadro, title);
                detailPlanety.Show();
            }));
        }

        public void KoniecHladaniaPlanetRasy()
        {
            Invoke((MethodInvoker)(() =>
            {
                _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
                var title = "Vsetky planety rasy : "+_hladanyItem;
                var detailPlanety = new ZoznamHracovForm(_jadro.NajdenePlanety, _jadro, title);
                detailPlanety.Show(this);
            }));
        }

        private void UpdateProgressBar()
        {
            progressBar1.Invoke(new MethodInvoker(delegate
            {
                progressBar1.Value = _jadro.AktualnaHodnota;
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nazovPlanety = textBox1.Text;
            _hladanyItem = nazovPlanety;
            _jadro.NajdiPlanetuTask(nazovPlanety, this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_jadro.HladacieVlakno != null)
            {
                _jadro.HladacieVlakno.Abort();
            }
            _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var hrac = textBox2.Text;
            var title = "Vsetky planety hraca : " + hrac;
            var detailPlanety = new PlanetaDetail(_jadro.NajdiPlanetyHraca(hrac), _jadro, title);
            detailPlanety.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _jadro.UkoncenieHladaniePlanetRasy += KoniecHladaniaPlanetRasy;
            var rasa = comboBox1.SelectedValue.ToString();
            _hladanyItem = rasa;
            _jadro.VypisPlanetyRasy(rasa);
        }

        private void HladanieForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_jadro.HladacieVlakno != null)
            {
                _jadro.HladacieVlakno.Abort();
            }
            _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
        }
    }
}
