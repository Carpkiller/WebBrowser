using System;
using System.Threading;
using System.Windows.Forms;

namespace WebBrowser.Hviezdne_brany
{
    public partial class HviezdnaBranaForm : Form
    {
        private Jadro _jadro;
        public HviezdnaBranaForm(Jadro jadro)
        {
            _jadro = jadro;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _jadro.HladajHB(textBoxCas.Text, textBoxCena.Text);
            Visible = false;
        }

        private void HladajHb(string cas, string cenaVstup)
        {
            var cen = int.Parse(cenaVstup);
            var cass = int.Parse(cas);
            var wb1 = new System.Windows.Forms.WebBrowser();
            wb1.ScriptErrorsSuppressed = true;
            wb1.Navigate(new Uri("http://www.stargate-game.cz/obchod.php?page=4"));
            while (true)
            {
                while (true)
                {
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(wb1.StatusText) && !wb1.StatusText.Contains("obchod.php?"))
                        break;
                }

                if (
                    wb1.Document.Body.InnerText.Contains("nabízena za "))
                {
                    var zac = wb1.Document.Body.InnerText.IndexOf("nabízena za ") + 12;
                    var kon = wb1.Document.Body.InnerText.IndexOf("kg naquadahu", zac);

                    var cena =
                        wb1.Document.Body.InnerText.Substring(zac,
                            kon - zac).Replace(" ", "");

                    if (int.Parse(cena) < cen)
                    {
                        MessageBox.Show("Lacne HB", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    Random rand = new Random();
                    Thread.Sleep((cass + rand.Next(-5, 10)) * 1000);
                    wb1.Refresh();
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
