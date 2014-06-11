using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using WebBrowser.Dohadzovanie;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.MessageBox;

namespace WebBrowser.ChytanieTrolov
{
    public class ChytanieTrolov
    {
        private ChytanieTrolovForm _chytanieTrolovForm;
        private Jadro _jadro;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.WebBrowser wbUtok;
        private System.Windows.Forms.WebBrowser wbJednotky;
        private string hladanyHrac;
        private bool existuje;
        private int pocetZobrazeni = 0;
        private string _pocetPechota;
        private string _pocetUni;
        private string _pocetOrbit;
        private string _pocetEB;
        private ChytanieTrolovForm _form;

        public delegate void HracExistujeHandler();
        public event HracExistujeHandler HracExistuje;

        public delegate void Utok();
        public event Utok UtokJeMozny;

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public ChytanieTrolov(ChytanieTrolovForm chytanieTrolovForm, Jadro jadro)
        {
            _jadro = jadro;
            _chytanieTrolovForm = chytanieTrolovForm;
           // HracExistuje += HracExistujePotvrdenie;
            UtokJeMozny += UtokMozny;
            wbJednotky = new System.Windows.Forms.WebBrowser
            {
                Url = new Uri("http://www.stargate-game.cz/jednotky.php?vyber=3")
            };
        }

        private void UtokMozny()
        {
            ZautocNaHraca();
        }

        public void Start(ChytanieTrolovForm form, string menoHraca, string rasa, System.Windows.Forms.WebBrowser webBrowser1, System.Windows.Forms.WebBrowser webBrowser2, string pocetPechota, string pocetEb)
        {
            _form = form;
            _pocetPechota = pocetPechota;
            _pocetUni = "0";
            _pocetOrbit = "0";
            _pocetEB = pocetEb;
            wb = webBrowser1;
            wbUtok = webBrowser2;
            hladanyHrac = menoHraca;
            wb.DocumentCompleted += this.webBrowser1_DocumentCompleted;

            //ExistujeHrac(menoHraca);

            //wb.Document.GetElementById("jmeno").SetAttribute("value", menoHraca);
            //var d = wb.Document.GetElementsByTagName("input").GetElementsByName("hledej_hrace");
            //d[0].InvokeMember("Click");

            //int x = 0;
            //while (x == 0)
            //{
            //    Application.DoEvents();
            //    if (!string.IsNullOrEmpty(wb.StatusText) && !wb.StatusText.Contains("vesmir.php"))
            //        break;
            //}

            //var text = wb.Document.Body.InnerText;
            //existuje = text.Contains("Tento hráč neexistuje! ");

            existuje = false;

            if (existuje && pocetZobrazeni == 0)
            {
                pocetZobrazeni++;
                MessageBox.Show("Hrac " + hladanyHrac + " neexistuje", "Hrac neexistuje", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (!existuje && pocetZobrazeni == 0)
            {
                PosliSiJednotky(new PoctyJadnotiek(_jadro.User, pocetPechota, string.Empty, string.Empty, pocetEb));
                //pocetZobrazeni++;
            }
        }

        private void PosliSiJednotky(PoctyJadnotiek pocty)
        {
            wbJednotky.Document.GetElementById("jed1").SetAttribute("value", pocty.Pechota);
            wbJednotky.Document.GetElementById("jed2").SetAttribute("value", pocty.Uni);
            wbJednotky.Document.GetElementById("jed4").SetAttribute("value", pocty.Orbit);
            wbJednotky.Document.GetElementById("jed5").SetAttribute("value", pocty.Elitaci);
            wbJednotky.Document.GetElementsByTagName("input").GetElementsByName("ra_z_jmeno")[0].SetAttribute(
                "value", pocty.Meno);
            var d = wbJednotky.Document.GetElementsByTagName("input").GetElementsByName("ra_z");

            d[0].InvokeMember("Click");

            Console.WriteLine("Poslanie jednotiek ... ");

            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(wbJednotky.StatusText) && !wbJednotky.StatusText.Contains("jednotky.php?vyber=3#ra-z") && wbJednotky.Document.Body.InnerText.Contains("Jednotky byly úspěšně odeslány."))
                    break;
            }
            if (wbJednotky.Document.Body.InnerText.Contains("Jednotky byly úspěšně odeslány."))
            {
                Console.WriteLine("Jednotky byly úspěšně odeslány.");
                if (UtokJeMozny != null) //vyvolani udalosti
                    UtokMozny();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var webBrowser1 = ((System.Windows.Forms.WebBrowser)(sender));

            var text = webBrowser1.Document.Body.InnerText;
            existuje = text.Contains("Tento hráč neexistuje! ");

                if (HracExistuje != null) //vyvolani udalosti
                    HracExistuje();
        }

        private void ZautocNaHraca()
        {
            Console.WriteLine(@"Utok");
            pocetZobrazeni = 0;

            var url = new Uri("http://www.stargate-game.cz/utok.php?jakej=2&cil=" + hladanyHrac);
            wbUtok.Navigate(url);

            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(wbUtok.StatusText) && !wbUtok.StatusText.Contains("utok.php"))
                    break;
            }
           
            var pech = wbUtok.Document.GetElementsByTagName("input").GetElementsByName("jednot1");
            pech[0].SetAttribute("value", _pocetPechota);
            var EB = wbUtok.Document.GetElementsByTagName("input").GetElementsByName("jednot5");
            EB[0].SetAttribute("value", _pocetEB);
            var e = wbUtok.Document.GetElementById("zautocit");

            var c = wbUtok.Document.GetElementsByTagName("input");

            var vystx = 0;
            var vysty = 0;

            var fmx = wbUtok.Parent.Location.X;
            var fmy = wbUtok.Parent.Location.Y;
            var item = c[5];

            var wbx = wbUtok.Location.X;
            var wby = wbUtok.Location.Y;

            vystx = item.OffsetRectangle.X;
            vysty = item.OffsetRectangle.Y;

            var x = vystx + fmx + wbx + 10;
            var y = vysty + fmy + wby + 40;
            
            _form.TopMost = true;

            var screenBounds = Screen.PrimaryScreen.Bounds;
            DoMouseClick(x * 65535 / screenBounds.Width, y * 65535 / screenBounds.Height);
        }

        public void DoMouseClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, new UIntPtr());

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, new UIntPtr());
        }
    }
}
