using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebBrowser.ChytanieNN;
using WebBrowser.ChytanieTrolov;
using WebBrowser.ChytanieVV;
using WebBrowser.Hladanie;
using WebBrowser.Hviezdne_brany;
using WebBrowser.PomocneTriedy;
using WebBrowser.Sektory;
using WebBrowser.StavbaPO;
using WebBrowser.WarMod;


namespace WebBrowser
{
    public partial class HlavneOkno : Form
    {
        private readonly Jadro _jadro;
        private int _poc;
        private int _pocHlad;

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(
            int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

        private const int URLMON_OPTION_USERAGENT = 0x10000001;
        private const int URLMON_OPTION_USERAGENT_REFRESH = 0x10000002;

        public void ChangeUserAgent()
        {
            const string ua = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64; rv:26.0) Gecko/20100101 Firefox/26.0\r\n";

            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT_REFRESH, null, 0, 0);
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, ua, ua.Length, 0);
        }

        public HlavneOkno()
        {
            ChangeUserAgent();
            InitializeComponent();

            _jadro = new Jadro(webBrowser1,this);
            dataGridView1.DataSource = _jadro.PrehladSektorovList;
            
            labelInfo.Text = _jadro.PrehladSektorovList.Count + @" / 200";

            _jadro.Zmeneno += StavZarovkyZmenenObsluha;
            _jadro.ZmenaSektora += AktualizujSektory;
            _jadro.AktualizujAktualnySektor += AktualizujAktualnySektor;

            var userData = SpravaHesiel.SpravaHesiel.LoadHesla();
           
            if (userData != null)
            {
                username.Text = userData.Login;
                password.Text = userData.Heslo;
            }

            dateTimePicker1.Value = Config.ZaciatokVeku;
        }

        private void AktualizujAktualnySektor()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    if (_jadro.AktualnaHodnota != 200)
                    {
                        toolStripStatusLabel1.Text = @"Aktualny sektor : " + _jadro.AktualnaHodnota;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = @"Ukladanie dokoncene";
                    }
                }));
            }
        }

        private void StavZarovkyZmenenObsluha()
        {
            labelInfoPlanety.Text = _jadro.NovePlanety.ToString(CultureInfo.InvariantCulture);
        }

        private void AktualizujSektory()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _jadro.PrehladSektorovList;
                    labelInfo.Text = _jadro.PrehladSektorovList.Count + @" / 200";
                }));
            }
        }

        internal void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("log-jmeno").SetAttribute("value", username.Text);
            webBrowser1.Document.GetElementById("log-heslo").SetAttribute("value", password.Text);
            var c = webBrowser1.Document.GetElementsByTagName("input");
            var d = c.GetElementsByName("prihlasit");

            d[1].InvokeMember("Click");
            _jadro.User =username.Text;
            _jadro.Heslo = password.Text;
            Console.WriteLine("Login ... ");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                _jadro.UlozData(webBrowser1.Document.Window.Frames["hlavni"].Document.Body.InnerHtml, webBrowser1.Document.Window.Frames["hlavni"].Document.Url.ToString());
            }
            catch (Exception exception)
            {
            //    MessageBox.Show(exception.ToString(),"Spustanie okna");
            }          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Document.Window.Frames["hlavni"].Navigate(new Uri(_jadro.GetDalsiSektor(1)));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "NasledujuciSektor");
            }    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Document.Window.Frames["hlavni"].Navigate(new Uri(_jadro.GetDalsiSektor(-1)));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "PredchadzajuciSektor");
            }   
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_jadro.Povolene)
            {
                    _jadro.VytvorDohadzovaca();
                    var uc = new DohadzovanieForm(_jadro, this, null, null, 3, 1);
                    uc.Visible = true;
                    uc.Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (_poc == 0)
            {
                _jadro.SpustRefresh(textBox1.Text);
                _poc = 1;
            }
            else
            {
                _jadro.ZastavRefresh();
                _poc = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_jadro.Povolene)
            {
                var title = "Sektor : " + ((List<SektorPrehlad>)dataGridView1.DataSource)[dataGridView1.CurrentRow.Index].Sektor;
                ZoznamHracovForm zoznamHracovForm =
                    new ZoznamHracovForm(dataGridView1.CurrentRow.Cells[0].Value.ToString(), _jadro, title);
                zoznamHracovForm.Show(this);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (_jadro.Povolene)
            {
                HladanieForm hladanie = new HladanieForm(_jadro);
                hladanie.Show(this);
            }
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_jadro.Povolene)
            {
                StavbaPOForm stavbaPo = new StavbaPOForm();
                stavbaPo.Show(this);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChytanieNNForm chytanieNN = new ChytanieNNForm(_jadro);
            chytanieNN.Show(this);
        }

        private void buttonVyvrhel_Click(object sender, EventArgs e)
        {
            ChytanieVVForm chytanieVV = new ChytanieVVForm(_jadro);
            chytanieVV.Show(this);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _jadro.AktualizujConfig(dateTimePicker1.Value,0);
        }

        private void chytanieTrolovToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate("http://www.stargate-game.cz/utok.php?jakej=2&cil=Tartarus");
            ChytanieTrolovForm chytanieTrolovForm = new ChytanieTrolovForm(_jadro);
            chytanieTrolovForm.Show(this);
        }

        private void vytvoritZalohuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_pocHlad == 0)
            {
                vytvoritZalohuToolStripMenuItem.Text = @"Zrusit skenovanie";
                _jadro.NaskenujVesmir();
                _pocHlad = 1;
            }
            else
            {
                vytvoritZalohuToolStripMenuItem.Text = @"Naskenovat vesmir";
                _jadro.ZastavHladanie();
                _pocHlad = 0;
            }
        }

        private void testSpojeniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_jadro.CheckSpojenie())
                MessageBox.Show(@"Pripojenie na DB je funkcne", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(@"Problem pri pripojeni na DB", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void moznostiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() => _jadro.StiahniOnlineData(Config.ZaciatokVeku));
            //Thread t = new Thread(() => _jadro.StiahniOnlineData(Config.ZaciatokVeku));
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            _jadro.StiahniOnlineData(Config.ZaciatokVeku);
        }

        private void odPoslednehoStiahnutiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var poslednyDownload = Config.PoslednyDownload ?? Config.ZaciatokVeku;
            //Task.Factory.StartNew(() => _jadro.StiahniOnlineData(poslednyDownload));
            Thread t = new Thread(() => _jadro.StiahniOnlineData(poslednyDownload));
            t.SetApartmentState(ApartmentState.MTA);
            t.Start();
        }

        private void odZaciatkuVekuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() => _jadro.UploadOnlineData(Config.ZaciatokVeku));
            _jadro.UploadOnlineData(Config.ZaciatokVeku);
            //Thread t = new Thread(() => _jadro.UploadOnlineData(Config.ZaciatokVeku));
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
        }

        private void odPoslednehoUploaduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var poslednyDownload = Config.PoslednyUpload ?? Config.ZaciatokVeku;
            //Task.Factory.StartNew(() => _jadro.UploadOnlineData(poslednyDownload));
            //_jadro.UploadOnlineData(poslednyDownload);
            Thread t = new Thread(() => _jadro.UploadOnlineData(poslednyDownload));
            t.SetApartmentState(ApartmentState.MTA);
            t.Start();
        }

        private void hviezdneBranyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HviezdnaBranaForm hb = new HviezdnaBranaForm(_jadro);
            hb.Show(); 
        }

        private void odstranitStareZaznamyZDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_jadro.OdstranStareZaznamy())
            {
                MessageBox.Show("Zaznamy odstranene", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Zaznamy neboli odstranene, skus to este raz", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static Task<T> StartSTATask<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        private void warModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (warModToolStripMenuItem.Text == "War mod")
            {
                warModToolStripMenuItem.Text = "War mod - vypnut";
                WarMode wm = new WarMode();
                wm.ShowDialog();
                if (wm.DialogResult == DialogResult.OK)
                {
                    _jadro.SpustWarMod(wm.RefreshovaciCas, wm.MnozstvoNaqu);
                }
                if (wm.DialogResult == DialogResult.Cancel)
                {
                    _jadro.VypniWarMod();
                }
            }
            else if (warModToolStripMenuItem.Text == "War mod - vypnut")
            {
                warModToolStripMenuItem.Text = "War mod";
                _jadro.VypniWarMod();
            }
        }
    }
}
