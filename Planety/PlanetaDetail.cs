using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WebBrowser.BrowserForm;
using WebBrowser.Hladanie;
using WebBrowser.PomocneTriedy;

namespace WebBrowser.Planety
{
    public partial class PlanetaDetail : Form
    {
        private readonly Jadro _jadro;
        private List<Planeta> _listPlanetHraca;
        private List<Planeta> _listPlanetHracaHladanie;
        private string _typPlanety;
        private HladanieForm _hladanieForm;
        private bool _najdena;

        public delegate void UkoncenieOkna();
        public event UkoncenieOkna KoniecOkna;

        public PlanetaDetail()
        {
            InitializeComponent();
        }

        public PlanetaDetail(object p1, DataGridViewRow dataGridViewRow, Jadro jadro)
        {
            _jadro = jadro;
            _jadro.ZmenaPlanety += ZobrazTypPlanety;
            InitializeComponent();
            _najdena = false;
            
            var index = dataGridViewRow.Index;
            var c = p1.GetType().ToString();

            if (p1.GetType().ToString() == "System.Collections.Generic.List`1[WebBrowser.PomocneTriedy.Planeta]")
            {
                var list = (List<Planeta>)p1;
                _listPlanetHraca = (List<Planeta>)jadro.ParsujPlanetyHraca(list[index].Sektor, list[index].Pozicia);
                _listPlanetHracaHladanie = _listPlanetHraca;
                dataGridView1.DataSource = _listPlanetHraca;
            }
            if (p1.GetType().ToString() == "System.Collections.Generic.List`1[WebBrowser.PomocneTriedy.SektorPlanety]")
            {
                var list = (List<SektorPlanety>)p1;
                _listPlanetHraca = (List<Planeta>)jadro.ParsujPlanetyHraca(list[index].Sektor, list[index].Pozicia);
                _listPlanetHracaHladanie = _listPlanetHraca;
                dataGridView1.DataSource = _listPlanetHraca;
                //_jadro.VlozZaznamOnlineDB(_listPlanetHraca.First());
            }
        }

        public PlanetaDetail(List<Planeta> list, Jadro jadro, string title)
        {
            if (!list.Any())
                MessageBox.Show(@"Ziadna planeta nenajdena", @"Najdene planety", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            else
            {
                _jadro = jadro;
                InitializeComponent();
                Text = title;
                _listPlanetHracaHladanie = new List<Planeta>(list);
                dataGridView1.DataSource = list;
            }
        }

        // http://www.stargate-game.cz/vesmir/mapa/planeta.php?planeta=7385726
        private void button1_Click(object sender, EventArgs e)
        {
            var planeta = _listPlanetHraca[_listPlanetHraca.Count - 1];
            _jadro.ZistiTypPlanety(planeta);
           // _jadro.ZmenaPlanety -= ZobrazTypPlanety;
        }

        private void ZobrazTypPlanety()
        {
            _typPlanety = _jadro.TypPlanety;
            Console.WriteLine(_typPlanety);
            if (_typPlanety.Contains("Výsledek průzkumu:"))
            {
                _listPlanetHraca = _jadro.ZmenPlanetyDb(_typPlanety.Replace("Výsledek průzkumu: ", ""), _listPlanetHraca.FirstOrDefault());
                dataGridView1.DataSource = _listPlanetHraca;
                _jadro.ZmenaPlanety -= ZobrazTypPlanety;
                _najdena = true;
            }
            else if (_jadro.TypPlanety == "Neexistuje")
            {
                _jadro.ZmenaPlanety -= ZobrazTypPlanety;
                _najdena = true;
            }                
            else
            {
                _jadro.ZmenaPlanety -= ZobrazTypPlanety;
                MessageBox.Show(@"Neznama chyba",@"Najdene planety",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
            
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var zoznamHracovForm = new PlanetaDetail(dataGridView1.DataSource, dataGridView1.CurrentRow, _jadro);
            zoznamHracovForm.Show(this);
        }

        private void PlanetaDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (KoniecOkna != null && _najdena)
                KoniecOkna();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hladanaPlaneta = _listPlanetHraca.FirstOrDefault();
            var url = "http://www.stargate-game.cz/utok.php?jakej=2&cil="+hladanaPlaneta.Majitel;
            var id = hladanaPlaneta.Meno;
            var browser = new DefaultBrowser(url, id);
            browser.Show();
        }
    }
}
