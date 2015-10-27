using System;
using System.Globalization;
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
        private string _hladanyTyp;

        public HladanieForm()
        {
            InitializeComponent();
            comboBox1.DataSource = RasyId.ListId.Keys.ToList();
            comboBoxRasaPovodna.DataSource = RasyId.ListId.Keys.ToList();
            comboBoxRasaSucasna.DataSource = RasyId.ListId.Keys.ToList();
        }

        public HladanieForm(Jadro jadro)
        {
            _jadro = jadro;
            _jadro.ZmenaPoradiaHladania += UpdateProgressBar;
            _jadro.KoniecVlakna += KoniecVlakana;
            InitializeComponent();
            comboBox1.DataSource = RasyId.ListId.Keys.ToList();
            comboBoxRasaPovodna.DataSource = RasyId.ListId.Keys.ToList();
            comboBoxRasaSucasna.DataSource = RasyId.ListId.Keys.ToList();
            comboBox2.DataSource = _jadro.NaplnSektory();
            comboBoxRasaTypPlanety.DataSource = RasyId.ListId.Keys.ToList();
            comboBoxTypPlanety.DataSource = _jadro.NaplnTypyPlanet();
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
                var detailPlanety = new ZoznamHracovForm(_jadro.NajdenePlanety, _jadro, title, comboBox2.SelectedIndex.ToString(CultureInfo.InvariantCulture));
                detailPlanety.Show(this);
                progressBar1.Style = ProgressBarStyle.Blocks;
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
            progressBar1.Style = ProgressBarStyle.Marquee;
            _jadro.UkoncenieHladaniePlanetRasy += KoniecHladaniaPlanetRasy;
            var rasa = comboBox1.SelectedValue.ToString();
            var sektor = comboBox2.SelectedIndex;
            _hladanyItem = rasa;
            _jadro.VypisPlanetyRasy(rasa, sektor);
        }

        private void HladanieForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_jadro.HladacieVlakno != null)
            {
                _jadro.HladacieVlakno.Abort();
            }
            _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var hrac = textBox2.Text;
            var title = "Povodna rasa : " + comboBoxRasaPovodna.Text + " , sucasna rasa : "+comboBoxRasaSucasna.Text;
            var detailPlanety = new PlanetaDetail(_jadro.NajdiZmenenePlanety(comboBoxRasaSucasna.Text, comboBoxRasaPovodna.Text), _jadro, title);
            detailPlanety.Show(this);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            _jadro.UkoncenieHladaniaTypuPlanetRasy += KoniecHladaniaTypuPlanetRasy;
            var rasa = comboBoxRasaTypPlanety.SelectedValue.ToString();
            var typ = comboBoxTypPlanety.SelectedValue.ToString();
            _hladanyItem = rasa;
            _hladanyTyp = typ;
            _jadro.VypisTypPlanetyRasy(rasa, typ);
        }

        public void KoniecHladaniaTypuPlanetRasy()
        {
            Invoke((MethodInvoker)(() =>
            {
                _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
                var title = "Vsetky planety rasy : " + _hladanyItem+ " a zadaneho typu "+_hladanyTyp;
                var detailPlanety = new ZoznamHracovForm(_jadro.NajdenePlanety, _jadro, title, comboBox2.SelectedIndex.ToString(CultureInfo.InvariantCulture));
                detailPlanety.Show(this);
                progressBar1.Style = ProgressBarStyle.Blocks;
            }));
        }
    }
}
