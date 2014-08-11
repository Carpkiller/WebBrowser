using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WebBrowser.Dohadzovanie;

namespace WebBrowser.ChytanieTrolov
{
    public partial class ChytanieTrolovForm : Form
    {
        private readonly ChytanieTrolov _chytanieTrolov;
        private readonly System.Windows.Forms.WebBrowser _webBrowser1;
        private System.Windows.Forms.WebBrowser _webBrowser2;
        private int _poc;
        private int _refreshovaciCas;
        private Thread _refreshovacieVlakno;
        private int _pocetLoad;
        private bool _koniec;
        private readonly Jadro _jadro;
        private List<HracPodmienky> _listPodm;
        public List<Hrac> listHracov;

        public ChytanieTrolovForm(Jadro jadro)
        {
            InitializeComponent();
            _jadro = jadro;
            _pocetLoad = 1;
            _chytanieTrolov = new ChytanieTrolov(this,jadro);
            _webBrowser1 = new System.Windows.Forms.WebBrowser {Url = new Uri("http://www.stargate-game.cz/vesmir.php")};
            comboBox1.DataSource = RasyId.ListId.Keys.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _listPodm = new List<HracPodmienky>
            {
                new HracPodmienky(textBoxMenoHraca.Text,int.Parse(textBoxSila.Text),int.Parse(textBoxPechota.Text),int.Parse(textBoxUni.Text),int.Parse(textBoxOrbit.Text),int.Parse(textBoxEB.Text))
            };

            string id;
            if (RasyId.ListId.TryGetValue(comboBox1.Text, out id))
            {
                _webBrowser2 = new System.Windows.Forms.WebBrowser { Url = new Uri("http://www.stargate-game.cz/vesmir.php?page=1&id_rasa=" + id) };
                _webBrowser2.ProgressChanged += webBrowser1_ProgressChanged;

                while (true)
                {
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(_webBrowser2.StatusText) && !_webBrowser2.StatusText.Contains("vesmir.php"))
                        break;
                }
                _pocetLoad = 0;

                if (_poc == 0)
                {
                    SpustRefreshDohadzovanie(textBoxCas.Text);
                    button1.Text = "Vypnut refresh";
                    _poc = 1;
                }
                else
                {
                    ZastavRefresh();
                    _poc = 0;
                }
            }
            else
            {
                MessageBox.Show("Neznama rasa", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Utok()
        {
            _chytanieTrolov.Start(this, null, textBoxMenoHraca.Text, comboBox1.SelectedValue.ToString(), _webBrowser1, webBrowser3, textBoxPechota.Text, textBoxEB.Text);
        }

        private void SpustRefreshDohadzovanie(string text)
        {
            _webBrowser2.Refresh();
            _refreshovaciCas = Convert.ToInt32((text));
            _refreshovacieVlakno = new Thread(WorkThreadFunction);
            _refreshovacieVlakno.Start();
        }

        public void WorkThreadFunction()
        {
            try
            {
                _koniec = false;
                while (!_koniec)
                {
                    //    Console.WriteLine(koniec);
                    Thread.Sleep(_refreshovaciCas * 1000);
                    _webBrowser2.Refresh();
                    //pocetLoad = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception pri behu vlakna " + ex);
            }
        }

        public void ZastavRefresh()
        {
            _pocetLoad = 2;
            _poc = 0;
            _koniec = true;
            button1.Text = "Zapnut refresh";
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_webBrowser2.StatusText) && _pocetLoad == 0 && !_webBrowser2.StatusText.Contains("vesmir.php"))
            {
                listHracov = _jadro.parsujHracov(_webBrowser2.Document.Window.Document.Body.InnerHtml);
                var ee = listHracov.Contains(new Hrac(textBoxMenoHraca.Text, "", ""));
                if (!listHracov.Contains(new Hrac(textBoxMenoHraca.Text, "", "")))
                {
                    ZastavRefresh();
                    MessageBox.Show("Hrac v zadanej rase neexistuje", "Chyba", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    _pocetLoad = 1;
                    if (_webBrowser2.Document.Url.AbsolutePath.Contains("vesmir.php"))
                    {
                        _jadro.SkontrolujHracov(listHracov, this, _listPodm, false, 2);
                    }
                }
            }
        }
    }
}
