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
            textBoxVhod0.Text = Config.Vhodnost0;
            textBoxVhod5.Text = Config.Vhodnost5;
            textBoxVhod10.Text = Config.Vhodnost10;
            textBoxVhod25.Text = Config.Vhodnost25;
            textBoxVhod50.Text = Config.Vhodnost50;
            textBoxVhodDef.Text = Config.VhodnostDefault;

            textBoxPocetMiest130.Text = Config.PocetMiest130;
            textBoxPocetMiest110.Text = Config.PocetMiest110;
            textBoxPocetMiest100.Text = Config.PocetMiest100;
            textBoxPocetMiest90.Text = Config.PocetMiest90;
            textBoxPocetMiestDef.Text = Config.PocetMiestDefault;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _jadro.AktualizujConfig(textBoxVhod0.Text, 7);
            _jadro.AktualizujConfig(textBoxVhod5.Text, 8);
            _jadro.AktualizujConfig(textBoxVhod10.Text, 9);
            _jadro.AktualizujConfig(textBoxVhod25.Text, 10);
            _jadro.AktualizujConfig(textBoxVhod50.Text, 11);
            _jadro.AktualizujConfig(textBoxVhodDef.Text, 12);
            _jadro.AktualizujConfig(textBoxPocetMiest130.Text, 13);
            _jadro.AktualizujConfig(textBoxPocetMiest110.Text, 14);
            _jadro.AktualizujConfig(textBoxPocetMiest100.Text, 15);
            _jadro.AktualizujConfig(textBoxPocetMiest90.Text, 16);
            _jadro.AktualizujConfig(textBoxPocetMiestDef.Text, 17);

            Close();
        }
    }
}
