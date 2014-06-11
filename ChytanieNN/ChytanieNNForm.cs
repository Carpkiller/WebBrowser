using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WebBrowser.ChytanieNN
{
    public partial class ChytanieNNForm : Form
    {
        private readonly Jadro _jadro;
        private List<ObchodPlaneta> _listPlanet;
        private int _pocetLoad;
        private bool _koniec;
        private int _refreshovaciCas;
        private int _poc;
        private int _uloz;

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy,int dwData, UIntPtr dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        //private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        //private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        
        public void DoMouseClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, new UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, new UIntPtr());
        }

        public ChytanieNNForm(Jadro jadro)
        {
            InitializeComponent();
            textBoxCena.Text = Config.CenaNN.ToString();
            //ClientSize = new System.Drawing.Size(1325, Screen.PrimaryScreen.WorkingArea.Size.Height-50);
            this._jadro = jadro;
            webBrowser1.Url = new Uri("http://www.stargate-game.cz/obchod.php?page=6#pl-prodej");
            webBrowser1.ScriptErrorsSuppressed = true;
            _pocetLoad = 0;
            _koniec = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _jadro.AktualizujConfig(textBoxCena.Text, 1);
            TopMost = false;
            if (_poc == 0)
            {
                _refreshovaciCas = int.Parse(textBoxRefresh.Text);
                var refreshovacieVlakno = new Thread(WorkThreadFunction);
                refreshovacieVlakno.Start();
                button1.Text = @"Koniec";
                _poc = 1;
            }
            else
            {
                ZastavRefresh();
                button1.Text = @"Start";
                Console.WriteLine(_pocetLoad + "  ---  " + DateTime.Now);
                _poc = 0;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _listPlanet = _jadro.ParsujPlanetyObchod(webBrowser1.Document.Window.Document.Body.InnerHtml);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _listPlanet;
        }

        private void WorkThreadFunction()
        {
            try
            {
                _koniec = false;
                while (!_koniec)
                {
                    Thread.Sleep(_refreshovaciCas * 1000);
                    webBrowser1.Refresh();
                    _pocetLoad = 0;
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
            _koniec = true;
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(webBrowser1.StatusText) && _pocetLoad == 0 && !webBrowser1.StatusText.Contains("obchod.php?page=6"))
            {
                var elements = webBrowser1.Document.Body.GetElementsByTagName("input").GetElementsByName("pl_koupit");
                foreach (HtmlElement element in elements)
                {
                    element.SetAttribute("onclick", "return true");
                }

                _listPlanet = _jadro.ParsujPlanetyObchod(webBrowser1.Document.Window.Document.Body.InnerHtml);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _listPlanet;
                _pocetLoad = 1;
                if (_poc==1)
                {
                    bool akcia = _jadro.CheckKupuPlanety(_listPlanet,textBoxCena.Text);
                    var list = _listPlanet;
                    var ind = _jadro.VypocitajSkore(list);
                    if (akcia)
                    {
                        var element = webBrowser1.Document.Body.GetElementsByTagName("input").GetElementsByName("pl_koupit")[ind*2+1];

                        int vystx;
                        int vysty;
                        webBrowser1.Document.Window.ScrollTo(getXoffset(element, out vystx), getYoffset(element, out vysty) + vysty / 5);

                        var fmx = webBrowser1.Parent.Location.X;
                        var fmy = webBrowser1.Parent.Location.Y;

                        var wbx = webBrowser1.Location.X;

                        vystx = 50 + element.Parent.OffsetRectangle.X + element.Parent.Parent.OffsetRectangle.X /* + (element.Parent.OffsetRectangle.X*2 + element.Parent.Parent.OffsetRectangle.Width) / 2*/ + element.Parent.Parent.OffsetRectangle.Width / 2;
                        vysty = element.OffsetRectangle.Y;

                        var x = vystx + fmx + wbx;
                        var y = vysty + fmy + 75;
                        
                        ZastavRefresh();

                        TopMost = true;
                        if (_uloz == 0)
                        {
                            Console.WriteLine(@"Ukladanie");
                            LogHelper.UlozPlanetyObchod(_listPlanet);
                            _uloz = 1;
                        }
                      //  DoMouseClick(65535 / Screen.PrimaryScreen.WorkingArea.Size.Width * (Screen.PrimaryScreen.WorkingArea.Size.Width / 2 + 540), 65535 / Screen.PrimaryScreen.WorkingArea.Size.Height * (285 + ind * 100 - ind * 4));
                        if (checkBox1.Checked)
                        {
                            var screenBounds = Screen.PrimaryScreen.Bounds;
                            if (!checkBoxSuradnice.Checked)
                            {
                                DoMouseClick(x * 65535 / screenBounds.Width, y * 65535 / screenBounds.Height);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(textBoxPoziciaX.Text) && !string.IsNullOrEmpty(textBoxPoziciaY.Text))
                                {
                                    DoMouseClick(int.Parse(textBoxPoziciaX.Text) * 65535 / screenBounds.Width, int.Parse(textBoxPoziciaY.Text) * 65535 / screenBounds.Height);
                                }
                            }
                            _poc++;
                        }
                    }
                }
            }
        }

        private void webBrowser1_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
           // webBrowser1.Navigate(url);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int vystx;
            int vysty;

            int i = int.Parse(textBoxRefresh.Text);

            var element = webBrowser1.Document.Body.GetElementsByTagName("input").GetElementsByName("pl_koupit")[i * 2 + 1];

            webBrowser1.Document.Window.ScrollTo(getXoffset(element, out vystx), getYoffset(element, out vysty) + vysty / 5);

            var fmx = webBrowser1.Parent.Location.X;
            var fmy = webBrowser1.Parent.Location.Y;

            var wbx = webBrowser1.Location.X;

            vystx = 50 + element.Parent.OffsetRectangle.X + element.Parent.Parent.OffsetRectangle.X /* + (element.Parent.OffsetRectangle.X*2 + element.Parent.Parent.OffsetRectangle.Width) / 2*/ + element.Parent.Parent.OffsetRectangle.Width / 2;
            vysty = element.OffsetRectangle.Y;

            var x = vystx + fmx + wbx;
            var y = vysty + fmy + 75;

            var screenBounds = Screen.PrimaryScreen.Bounds;

            if (!checkBoxSuradnice.Checked)
            {
                DoMouseClick(x * 65535 / screenBounds.Width, y * 65535 / screenBounds.Height);
                textBoxPoziciaX.Text = x.ToString();
                textBoxPoziciaY.Text = y.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(textBoxPoziciaX.Text) && !string.IsNullOrEmpty(textBoxPoziciaY.Text))
                {
                    DoMouseClick(int.Parse(textBoxPoziciaX.Text) * 65535 / screenBounds.Width, int.Parse(textBoxPoziciaY.Text) * 65535 / screenBounds.Height);
                }
            }
        }

        private void ChytanieNNForm_MouseMove(object sender, MouseEventArgs e)
        {
            labelPoziciaX.Text=Cursor.Position.X + @" : " + Cursor.Position.Y;
        }

        private void webBrowser1_MouseMove(object sender, MouseEventArgs e)
        {
            labelPoziciaX.Text = Cursor.Position.X.ToString();
            labelPoziciaY.Text = Cursor.Position.Y.ToString();
        }

        public int getXoffset(HtmlElement el,out int vyst)
        {
            //get element pos
            int xPos = el.OffsetRectangle.Left;
            int poc = 0;
            vyst = 0;
            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                if (poc==0)
                {
                    vyst = tempEl.OffsetRectangle.Width/2;
                    poc++;
                }
                xPos += tempEl.OffsetRectangle.Left;
                tempEl = tempEl.OffsetParent;
            }
            Console.WriteLine();
            return xPos;
        }

        public int getYoffset(HtmlElement el, out int vyst)
        {
            //get element pos
            int yPos = el.OffsetRectangle.Top;
            int poc = 0;
            vyst = 0;
            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                if (poc == 0)
                {
                    vyst = tempEl.OffsetRectangle.Height / 2;
                    poc++;
                }
                yPos += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }

            return yPos;
        }

        private void ChytanieNNForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ZastavRefresh();
        }

        private void textBoxCena_TextChanged(object sender, EventArgs e) 
        {
        }
    }
}
