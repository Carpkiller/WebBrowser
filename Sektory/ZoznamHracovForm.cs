using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WebBrowser.Export;
using WebBrowser.Planety;
using WebBrowser.PomocneTriedy;

namespace WebBrowser.Sektory
{
    public partial class ZoznamHracovForm : Form
    {
        private readonly Jadro _jadro;
        private readonly string _rasa;
        private readonly string _sektor;
        private int _firstDisplayedRow;
        private SektorPlanety _hladanaPLaneta;
        private int _selectedRow;

        public ZoznamHracovForm(string sektor, Jadro jadro, string title)
        {
            InitializeComponent();
            _jadro = jadro;
            Text = title;
            dataGridView1.DataSource = jadro.ZobrazSektor(sektor);
            _sektor = sektor;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public ZoznamHracovForm(List<SektorPlanety> najdenePlanety, Jadro jadro, string title, string sektor)
        {
            InitializeComponent();
            _jadro = jadro;
            Text = title;
            _sektor = sektor;
            _rasa = title.Substring(title.IndexOf(": ", System.StringComparison.Ordinal) + 2);
            dataGridView1.DataSource = najdenePlanety.OrderByDescending(x => x.Sektor).ThenBy(x => x.Meno).ToList();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var zoznamHracovForm = new PlanetaDetail(dataGridView1.DataSource, dataGridView1.CurrentRow, _jadro);
            zoznamHracovForm.KoniecOkna += RefreshOkno;
            zoznamHracovForm.Show();
            _firstDisplayedRow = dataGridView1.FirstDisplayedScrollingRowIndex;
            //_hladanaPLaneta = ((List<SektorPlanety>)dataGridView1.DataSource)[dataGridView1.CurrentRow.Index];
            _selectedRow = dataGridView1.CurrentRow.Index;

        }

        private string RefreshOkno(string typ)
        {
            if (_rasa != null)
            {
                var planety = (List<SektorPlanety>)dataGridView1.DataSource;
                planety[_selectedRow].Typ = typ;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = planety;
                //_jadro.UkoncenieHladaniePlanetRasy += KoniecHladaniaPlanetRasy;
                //_jadro.VypisPlanetyRasy(_rasa, int.Parse(_sektor));
                dataGridView1.FirstDisplayedScrollingRowIndex = _firstDisplayedRow;
            }
            else if (_sektor != null)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _jadro.ZobrazSektor(_sektor);
            }

            return string.Empty;
        }

        private void KoniecHladaniaPlanetRasy()
        {
            Invoke((MethodInvoker)(() =>
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _jadro.NajdenePlanety.OrderByDescending(x => x.Sektor).ThenBy(x => x.Meno).ToList();
                _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
                dataGridView1.FirstDisplayedScrollingRowIndex = _firstDisplayedRow;
            }));
        }

        private void ZoznamHracovForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            ExportForm exportForm = new ExportForm(_jadro.ExportPlanetString(dataGridView1.DataSource));
            exportForm.Show();
        }
    }
}
