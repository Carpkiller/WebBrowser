using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WebBrowser.Planety;
using WebBrowser.PomocneTriedy;

namespace WebBrowser.Sektory
{
    public partial class ZoznamHracovForm : Form
    {
        private readonly Jadro _jadro;
        private readonly string _rasa;

        public ZoznamHracovForm(string sektor, Jadro jadro, string title)
        {
            InitializeComponent();
            _jadro = jadro;
            Text = title;
            dataGridView1.DataSource = jadro.ZobrazSektor(sektor);
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public ZoznamHracovForm(List<SektorPlanety> najdenePlanety, Jadro jadro, string title)
        {
            InitializeComponent();
            _jadro = jadro;
            Text = title;
            _rasa = title.Substring(title.IndexOf(": ", System.StringComparison.Ordinal) + 2);
            dataGridView1.DataSource = najdenePlanety.OrderByDescending(x => x.Sektor).ToList();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var zoznamHracovForm = new PlanetaDetail(dataGridView1.DataSource, dataGridView1.CurrentRow, _jadro);
            zoznamHracovForm.KoniecOkna += RefreshOkno;
            zoznamHracovForm.Show();
        }

        private void RefreshOkno()
        {
            _jadro.UkoncenieHladaniePlanetRasy += KoniecHladaniaPlanetRasy;
            _jadro.VypisPlanetyRasy(_rasa, 0);
        }

        private void KoniecHladaniaPlanetRasy()
        {
            Invoke((MethodInvoker)(() =>
            {
                dataGridView1.DataSource = _jadro.NajdenePlanety.OrderByDescending(x => x.Sektor).ToList();
                _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
            }));
        }

        private void ZoznamHracovForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _jadro.UkoncenieHladaniePlanetRasy -= KoniecHladaniaPlanetRasy;
        }
    }
}
