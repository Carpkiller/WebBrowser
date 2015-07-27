using System.Globalization;
using System.Windows.Forms;

namespace WebBrowser.ChytanieNN
{
    public partial class NastavenieNN : Form
    {
        private Jadro _jadro ;
        public NastavenieNN(Jadro jadro)
        {
            _jadro = jadro;
            InitializeComponent();
            textBoxVhod0.Text = (decimal.Parse(Config.Vhodnost0) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod5.Text = (decimal.Parse(Config.Vhodnost5) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod10.Text = (decimal.Parse(Config.Vhodnost10) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod25.Text = (decimal.Parse(Config.Vhodnost25) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod50.Text = (decimal.Parse(Config.Vhodnost50) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhodDef.Text = (decimal.Parse(Config.VhodnostDefault) / 100).ToString(CultureInfo.InvariantCulture);

            textBoxPocetMiest130.Text = (decimal.Parse(Config.PocetMiest130) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiest110.Text = (decimal.Parse(Config.PocetMiest110) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiest100.Text = (decimal.Parse(Config.PocetMiest100) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiest90.Text = (decimal.Parse(Config.PocetMiest90) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiestDef.Text = (decimal.Parse(Config.PocetMiestDefault) / 100).ToString(CultureInfo.InvariantCulture);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _jadro.AktualizujConfig(decimal.Parse(textBoxVhod0.Text)*100, 7);
            _jadro.AktualizujConfig(decimal.Parse(textBoxVhod5.Text)*100, 8);
            _jadro.AktualizujConfig(decimal.Parse(textBoxVhod10.Text)*100, 9);
            _jadro.AktualizujConfig(decimal.Parse(textBoxVhod25.Text)*100, 10);
            _jadro.AktualizujConfig(decimal.Parse(textBoxVhod50.Text)*100, 11);
            _jadro.AktualizujConfig(decimal.Parse(textBoxVhodDef.Text)*100, 12);
            _jadro.AktualizujConfig(decimal.Parse(textBoxPocetMiest130.Text)*100, 13);
            _jadro.AktualizujConfig(decimal.Parse(textBoxPocetMiest110.Text)*100, 14);
            _jadro.AktualizujConfig(decimal.Parse(textBoxPocetMiest100.Text)*100, 15);
            _jadro.AktualizujConfig(decimal.Parse(textBoxPocetMiest90.Text)*100, 16);
            _jadro.AktualizujConfig(decimal.Parse(textBoxPocetMiestDef.Text)*100, 17);

            Close();
        }
    }
}
