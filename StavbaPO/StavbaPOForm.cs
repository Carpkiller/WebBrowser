using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace WebBrowser.StavbaPO
{
    public partial class StavbaPOForm : Form
    {
        private bool Stavaj;
        private string PrvaPlaneta;
        private int refreshovaciCas;
        private Thread refreshovacieVlakno;
        private bool koniec = false;
        private List<string> zoznamPlanet;
        private HtmlElement dalsiaPlaneta;
        private HtmlElement postavTlacitko;
        private bool cakaj;

        public delegate void ZmenaPlanetyHandler();
        public event ZmenaPlanetyHandler ZmenaPlanety;

        public StavbaPOForm()
        {
            InitializeComponent();
            Stavaj = false;
            refreshovaciCas = 3;
            cakaj = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {            
            postavTlacitko = webBrowser1.Document.GetElementsByTagName("input")[6];
            var comInfo = parsujAktualnaPlaneta();
            zoznamPlanet = parsujPlanety(parsujHtml());
            var aktualnaPlaneta = zistiAktualnuPlanetu(zoznamPlanet,comInfo);
            PrvaPlaneta = parsujPlanety(parsujHtml())[0];

            Console.WriteLine(aktualnaPlaneta);

            var pocetBS = textBoxBS.Text;
            var pocetSDI = textBoxSDI.Text;
            var pocetPO = textBoxPO.Text;
            var maxPocetPo = zistiMaxPocetPO();

            if (int.Parse(pocetPO) > int.Parse(maxPocetPo) || checkBox1.Checked)
            {
                pocetPO = maxPocetPo;
            }

            if (Stavaj && aktualnaPlaneta.Contains(PrvaPlaneta))
            {
                Stavaj = false;
            }
            var c = postavenePodlaPodmienok(aktualnaPlaneta);
            Console.WriteLine(c);
            Console.WriteLine("stavaj   "+Stavaj);
            if (Stavaj)
            {
                var podmienka = postavenePodlaPodmienok(aktualnaPlaneta);
                Console.WriteLine("podmienka   " + podmienka);
                if (podmienka) // ma stavat
                {
                    
                    while (true)
                    {
                        Application.DoEvents();
                        if (!string.IsNullOrEmpty(webBrowser1.StatusText) &&
                            !webBrowser1.StatusText.Contains("stavby.php"))
                            break;
                    }
                    Thread.Sleep(refreshovaciCas * 1000);
                    int krok = 0;

                    if (!skontrolujBS())
                    {
                        krok++;
                        webBrowser1.Document.GetElementById("bs").SetAttribute("value", pocetBS);
                    }
                    if (!skontrolujSDI())
                    {
                        krok++;
                        webBrowser1.Document.GetElementById("sdi").SetAttribute("value", pocetSDI);
                    }
                    if (!skontrolujPO())
                    {
                        krok++;
                        webBrowser1.Document.GetElementById("po").SetAttribute("value", pocetPO);
                    }

                    postavTlacitko = webBrowser1.Document.GetElementsByTagName("input")[6];
                    var ee = webBrowser1.Document.GetElementsByTagName("input");

                    if (krok > 0)
                    {
                        // klik na stavat
                        if (webBrowser1.Document.Body.InnerText.Contains("bylo postaveno") || webBrowser1.Document.Body.InnerText.Contains("není dostatek místa"))
                        {
                            PosunNaDalsiuPlanetu();
                        }
                        else
                        {
                            postavTlacitko.InvokeMember("Click");

                            while (true)
                            {
                                Application.DoEvents();
                                if (!string.IsNullOrEmpty(webBrowser1.StatusText) &&
                                    !webBrowser1.StatusText.Contains("stavby.php"))
                                    break;
                            }
                            Console.WriteLine("Staviame stavby");
                        }
                    }
                    if (krok ==0)
                    {
                        PosunNaDalsiuPlanetu();      
                    }
                }

                if (!podmienka)  // nema stavat
                {                    
                    PosunNaDalsiuPlanetu();
                    //cakaj = true; // uvolnenie vlakna, posun na dlasiu planetu
                }
            }
        }

        private bool postavenePodlaPodmienok(string aktualnaPlaneta)
        {
            var a = skontrolujBS();
            var aa = skontrolujSDI();
            var ass = skontrolujPO();
            var asss = niejeSpecialnaPlaneta(aktualnaPlaneta);
            Console.WriteLine(a +" "+ aa+" "+ass+" "+asss);
            return /*skontrolujBS() && skontrolujSDI()  && skontrolujPO() && */!niejeSpecialnaPlaneta(aktualnaPlaneta);
        }

        private bool niejeSpecialnaPlaneta(string aktualnaPlaneta)
        {
            return aktualnaPlaneta.Contains("(CP)") ||
                   aktualnaPlaneta.Contains("(PP)") || 
                   aktualnaPlaneta.Contains("(DP)");
        }

        private bool skontrolujSDI()
        {
            var pocetSDI = textBoxSDI.Text;

            var value = webBrowser1.Document.Body.GetElementsByTagName("input")[8].GetAttribute("value").Replace(".", "");

            return int.Parse(value) >= int.Parse(pocetSDI);
        }

        private bool skontrolujBS()
        {
            var pocetBS = textBoxBS.Text;

            var value = webBrowser1.Document.Body.GetElementsByTagName("input")[7].GetAttribute("value").Replace(".", "");

            return int.Parse(value) >= int.Parse(pocetBS);
        }

        private bool skontrolujPO()
        {
            var pocetPO = textBoxPO.Text;
            var maxPocetPo = zistiMaxPocetPO();

            pocetPO = int.Parse(pocetPO) > int.Parse(maxPocetPo) || checkBox1.Checked ? maxPocetPo : pocetPO;
            var value = webBrowser1.Document.Body.GetElementsByTagName("input")[9].GetAttribute("value").Replace(".","");
            
            return int.Parse(value) == int.Parse(pocetPO);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("--stavaj   " + Stavaj);
            if (!Stavaj)
            {
                button1.Text = "Zastavit";
            //    SpustRefreshDohadzovanie();
                Stavaj = true;
                PosunNaDalsiuPlanetu();
            }else if (Stavaj)
            {
                button1.Text = "Start";
                Stavaj = false;
            }
            //Stavaj = false;
        }

        private void SpustRefreshDohadzovanie()
        {
            refreshovacieVlakno = new Thread(WorkThreadFunction);
            refreshovacieVlakno.Start();
        }

        private void WorkThreadFunction()
        {
            try
            {
                Stavaj = true;
                while (Stavaj)
                {
                    PosunNaDalsiuPlanetu(); 
                    Thread.Sleep(refreshovaciCas * 1000);
                    while (cakaj)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception pri behu vlakna " + ex);
            }
        }

        private void PosunNaDalsiuPlanetu()
        {
            dalsiaPlaneta = webBrowser1.Document.GetElementsByTagName("a")[4];  //6
            Console.WriteLine("posun dalej   " + Stavaj);
            dalsiaPlaneta.InvokeMember("Click");
            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(webBrowser1.StatusText) &&
                    !webBrowser1.StatusText.Contains("stavby.php"))
                    break;
            }
        }

        private string zistiMaxPocetPO()
        {
            if (webBrowser1.Document.Body.InnerText.Contains("bylo postaveno"))
            {
                var text = webBrowser1.Document.Body;
                text.InnerHtml = text.InnerHtml.Replace("<SPAN class=postaveno>", "");

                var pocet1 = text.GetElementsByTagName("span")[9].InnerText;
              //  var pocet1 = webBrowser1.Document.Body.GetElementsByTagName("span")[9].InnerText;

                return pocet1.Replace(" ", "");
            }
            var pocet = webBrowser1.Document.Body.GetElementsByTagName("span")[9].InnerText;

            return pocet.Replace(" ","");
        }

        private string parsujAktualnaPlaneta()
        {
            var a = webBrowser1.Document.Body.GetElementsByTagName("div");
            var comInfo = string.Empty;

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].InnerText.Contains(" » ") && a[i].InnerText.Contains("« "))
                {
                    comInfo = a[i].InnerText;
                }
            }
            return comInfo;
        }

        private HtmlDocument parsujHtml()
        {
            var html = webBrowser1.Document.Body.InnerHtml;
            var index = html.IndexOf("<TABLE");
            html = html.Substring(index);
            index = html.IndexOf("</TABLE>");
            html = html.Remove(index + 8);
            var inputHtml = html;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(inputHtml);
            return doc;
        }

        private string zistiAktualnuPlanetu(List<string> zoznamPlanet, string comInfo)
        {
        //    var planeta = comInfo;
            foreach (var item in zoznamPlanet)
            {
                var planeta = item;
                if (planeta.Contains(" "))
                {
                    var index = planeta.IndexOf(" ");
                    if (index > 0)
                    {
                        planeta = planeta.Substring(0, index);
                    }
                }
                if (comInfo.Contains(planeta))
                {
                    return item;
                }
            }
            return null;
        }

        private List<string> parsujPlanety(HtmlDocument doc)
        {
            var list = new List<string>();
            var poc = doc.DocumentNode.ChildNodes[0].ChildNodes[3].ChildNodes.Count / 2;
            var a = doc.DocumentNode.ChildNodes[0].ChildNodes[3].ChildNodes[0 * 2 + 1].ChildNodes[1].InnerText;

            for (int i = 0; i < doc.DocumentNode.ChildNodes[0].ChildNodes[3].ChildNodes.Count / 2; i++)
            {
                list.Add(doc.DocumentNode.ChildNodes[0].ChildNodes[3].ChildNodes[i*2+1].ChildNodes[1].InnerText);
            }

            return list;
        }


    }
}
