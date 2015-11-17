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
            textBoxVhod0.Text = (double.Parse(Config.Vhodnost0) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod5.Text = (double.Parse(Config.Vhodnost5) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod10.Text = (double.Parse(Config.Vhodnost10) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod25.Text = (double.Parse(Config.Vhodnost25) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhod50.Text = (double.Parse(Config.Vhodnost50) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxVhodDef.Text = (double.Parse(Config.VhodnostDefault) / 100).ToString(CultureInfo.InvariantCulture);

            textBoxPocetMiest130.Text = (double.Parse(Config.PocetMiest130) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiest110.Text = (double.Parse(Config.PocetMiest110) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiest100.Text = (double.Parse(Config.PocetMiest100) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiest90.Text = (double.Parse(Config.PocetMiest90) / 100).ToString(CultureInfo.InvariantCulture);
            textBoxPocetMiestDef.Text = (double.Parse(Config.PocetMiestDefault) / 100).ToString(CultureInfo.InvariantCulture);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _jadro.AktualizujConfig(double.Parse(textBoxVhod0.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 7);
            _jadro.AktualizujConfig(double.Parse(textBoxVhod5.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 8);
            _jadro.AktualizujConfig(double.Parse(textBoxVhod10.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 9);
            _jadro.AktualizujConfig(double.Parse(textBoxVhod25.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 10);
            _jadro.AktualizujConfig(double.Parse(textBoxVhod50.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 11);
            _jadro.AktualizujConfig(double.Parse(textBoxVhodDef.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 12);
            _jadro.AktualizujConfig(double.Parse(textBoxPocetMiest130.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 13);
            _jadro.AktualizujConfig(double.Parse(textBoxPocetMiest110.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 14);
            _jadro.AktualizujConfig(double.Parse(textBoxPocetMiest100.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 15);
            _jadro.AktualizujConfig(double.Parse(textBoxPocetMiest90.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 16);
            _jadro.AktualizujConfig(double.Parse(textBoxPocetMiestDef.Text, NumberStyles.Any, CultureInfo.InvariantCulture) * 100, 17);

            Close();
        }
    }
}
