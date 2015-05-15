using System;
using System.Linq;
using System.Windows.Forms;

namespace WebBrowser.OnlineStatistiky
{
    public partial class OnlineStatistikyForm : Form
    {
        public OnlineStatistikyForm(Jadro jadro)
        {
            InitializeComponent();
            var data = jadro.VytvorOnlineStatistiky();
            labelPocetPlanet.Text = data.Values.Sum().ToString();
            labelText.Text = @"Pocet planet od " + Config.ZaciatokVeku.ToShortDateString();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = data.ToList();
            dataGridView1.Columns[0].HeaderText = @"Hrac";
            dataGridView1.Columns[1].HeaderText = @"Pocet vlozenych";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
