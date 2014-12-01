using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebBrowser.Dohadzovanie;

namespace WebBrowser
{
    public partial class DohadzovanieForm : Form
    {
        private Jadro jadro;
        private HlavneOkno form;
        private int poc = 0;
        private int refreshovaciCas;
        private Thread refreshovacieVlakno;
        private Thread kontrolneVlakno;
        private int pocetLoad = 0;
        private List<HracPodmienky> listPodm;
        private List<Hrac> listHracov;
        private bool koniec = false;
        private string mode;
        private int casSpomalenia;
        private bool kontrolaSily;
        private Hrac kontrolovanyHrac;
        private HtmlElement inputJednotky;
        private HtmlElement inputMeno;
        private HtmlElement tlacitkoOdoslat;

        private TextBox log;

        public DohadzovanieForm()
        {
            InitializeComponent();
        }

        public DohadzovanieForm(Jadro jadro, HlavneOkno form1, string mode, List<HracPodmienky> schema, int refreshCas, int casSpom)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            log = textBoxLog;
            kontrolaSily = false;
            this.jadro = jadro;
            form = form1;
            listPodm = schema;
            if (schema == null)
            {
                listPodm = new List<HracPodmienky>();
            }
            textBox1.Text = refreshCas.ToString();
            textBox2.Text = casSpom.ToString();
            webBrowser1.Url = new Uri("http://www.stargate-game.cz/jednotky.php?vyber=3");
            webBrowser2.Url = new Uri("http://www.stargate-game.cz/vesmir.php?page=1&id_rasa=6&sort=2");
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            listHracov = jadro.parsujHracov(webBrowser2.Document.Window.Document.Body.InnerHtml);
            dataGridView1.DataSource = listHracov;
            if (mode=="1")
            {
                textBoxLog.AppendText("Start dohadzovania po znovulognuti.... " + Environment.NewLine);
                SpustRefreshDohadzovanie(textBox1.Text);
                button2.Text = "Vypnut Dohadzovanie";
                poc = 1;
            }
            if (kontrolaSily){
                ZistiMnozstvoSily(kontrolovanyHrac);
                kontrolaSily = false;
            }

        }

        private void webBrowser1_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            inputJednotky = webBrowser1.Document.GetElementById("jed1");
            inputMeno = webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("ra_z_jmeno")[0];
            tlacitkoOdoslat = webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("ra_z")[0];
        }

        private void ZistiMnozstvoSily(Hrac kontrolovanyHras)
        {
            var hrac = najdiHraca(kontrolovanyHras.Meno);
            var zmenaSily = int.Parse(hrac.AktualnaSila.Replace(" ", "")) - int.Parse(kontrolovanyHras.AktualnaSila.Replace(" ", ""));
            jadro.AiDohadzovac.listDohodov.Add(new DohadzovaciePocty(kontrolovanyHras.Meno, zmenaSily));
        }

        private Hrac najdiHraca(string kontrolovanyHrac)
        {
            foreach (var item in listHracov)
            {
                if (item.Meno == kontrolovanyHrac)
                {
                    return item;
                }
            }
            return null;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (poc == 0)
            {
                textBoxLog.AppendText("Start dohadzovania .... "+Environment.NewLine);
                SpustRefreshDohadzovanie(textBox1.Text);
                button2.Text = "Vypnut Dohadzovanie";
                poc = 1;
            }
            else
            {
                ZastavRefresh();
                textBoxLog.AppendText("Koniec dohadzovania .... " + Environment.NewLine+Environment.NewLine);
                button2.Text = "Start dohadzovania";
                Console.WriteLine(pocetLoad + "  ---  " + DateTime.Now);
                poc = 0;
            }
        }

        private void SpustRefreshDohadzovanie(string text)
        {
            webBrowser2.Refresh();
            refreshovaciCas = Convert.ToInt32((text));
            refreshovacieVlakno = new Thread(WorkThreadFunction);
            refreshovacieVlakno.Start();
        }

        public void WorkThreadFunction()
        {
            try
            {
                koniec = false;
                while (!koniec)
                {
                //    Console.WriteLine(koniec);
                    Thread.Sleep(refreshovaciCas*1000);
                    webBrowser2.Refresh();
                    pocetLoad = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception pri behu vlakna "+ex);
            }
        }

        public void ZastavRefresh()
        {
            pocetLoad = 2;
        //    Console.WriteLine(pocetLoad + "  ---  " + DateTime.Now);
            koniec = true;
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(webBrowser2.StatusText) && pocetLoad == 0 && !webBrowser2.StatusText.Contains("vesmir.php"))
            {
            //  Console.WriteLine(DateTime.Now +"   "+pocetLoad);
                listHracov = jadro.parsujHracov(webBrowser2.Document.Window.Document.Body.InnerHtml);
                textBoxLog.AppendText(DateTime.Now + Environment.NewLine);
                pocetLoad = 1;
                if (webBrowser2.Document.Url.AbsolutePath.Contains("vesmir.php"))
                {
                    dataGridView1.DataSource = null;
                    var datasource = listHracov;
                    dataGridView1.DataSource = datasource;
                    jadro.SkontrolujHracov(datasource, this, listPodm, checkBox1.Checked,0);
                    if (kontrolaSily)
                    {
                        ZistiMnozstvoSily(kontrolovanyHrac);
                        kontrolaSily = false;
                        kontrolneVlakno.Abort();
                    }
                }
                else
                {   //  opatovne prihlasenie
                    //koniec = true;
                    //jadro.aktualnaSchema = listPodm;
                    //jadro.casMedziDohozmi = refreshovaciCas;
                    //jadro.casSpomalenia = casSpomalenia;
                    //jadro.VykonajPrihlasenie(form.username.Text,form.password.Text);
                    //this.Close();
                }
            }
        }

        public void DohodHraca(PoctyJadnotiek pocty)
        {
            if (DohozMozny())
            {

                casSpomalenia = dlzkaSpomalenia(textBox2.Text);

                webBrowser1.Document.GetElementById("jed1").SetAttribute("value", pocty.Pechota);
                webBrowser1.Document.GetElementById("jed2").SetAttribute("value", pocty.Uni);
                webBrowser1.Document.GetElementById("jed4").SetAttribute("value", pocty.Orbit);
                webBrowser1.Document.GetElementById("jed5").SetAttribute("value", pocty.Elitaci);
                webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("ra_z_jmeno")[0].SetAttribute(
                    "value", pocty.Meno);
                var d = webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("ra_z");

                Thread.Sleep(casSpomalenia);
                d[0].InvokeMember("Click");

                textBoxLog.AppendText(DateTime.Now + "  Dohodenie hraca " + pocty.Meno + " " + Environment.NewLine);
                textBoxLog.AppendText("  jednotky :  pechota - " + pocty.Pechota + " , uni - " + pocty.Uni + " , orbity - " + pocty.Orbit +
                                      " , EB - " + pocty.Elitaci + Environment.NewLine);
                Console.WriteLine("Dohodenie ... ");

                jadro.CasPoslednehoDohozu = DateTime.Now;
                refreshovaciCas = Convert.ToInt32((textBox1.Text));
            }
            else
            {
                refreshovaciCas = 1;
            }
        }

        private bool DohozMozny()
        {
            var teraz = DateTime.Now;
            var rozdiel = teraz - jadro.CasPoslednehoDohozu;
            
            if (rozdiel.Seconds>9)
            {
                return true;
            }
            return false;
        }

        private int dlzkaSpomalenia(string p)
        {
            var rand = new Random();
            var inputInt = int.Parse(p)*1000;

            var maxCislo = (int)Math.Ceiling(inputInt*1.2);
            var minCislo = (int)Math.Ceiling(inputInt * 0.9);

            if (maxCislo==0)
            {
                maxCislo = 200;
            }

            return rand.Next(minCislo, maxCislo);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            InfoHrac info = new InfoHrac(dataGridView1.CurrentRow.Cells[0].Value.ToString(),listPodm);
            if (info.ShowDialog()==DialogResult.OK)
            {
                jadro.PridajHraca(info.hrac,listPodm, out listPodm);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = saveFileDialog1.FileName;
                try
                {
                    using (Stream stream = File.Open(file, FileMode.Create))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, listPodm);
                    }
                }
                catch (IOException)
                {
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    using (Stream stream = File.Open(file, FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();

                        listPodm = (List<HracPodmienky>)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                }
            }
        }

        private void DohadzovanieForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (refreshovacieVlakno != null)
            {
                if (refreshovacieVlakno.IsAlive)
                {
                    koniec = true;
                }
            }
        }

        public void NaplanujKontroluSily(string meno)
        {
            kontrolovanyHrac = najdiHraca(meno);
            kontrolneVlakno = new Thread(WorkThreadFunctionKontrolne);
            kontrolneVlakno.Start();
        }

        public void WorkThreadFunctionKontrolne()
        {
     //       try            {
                Thread.Sleep(23 * 1000);
                while (!DohozMozny())
                {
                }

                inputJednotky.SetAttribute("value", "100000");
                inputMeno.SetAttribute(
                    "value", kontrolovanyHrac.Meno);

                tlacitkoOdoslat.InvokeMember("Click");

               // zapisLog(DateTime.Now + "  Kontrolny dohod hraca " + kontrolovanyHrac.Meno + " " + Environment.NewLine + "  jednotky :  pechota - " + "100000" + Environment.NewLine);

                Console.WriteLine("Dohodenie ... ");

                jadro.CasPoslednehoDohozu = DateTime.Now;
                
                kontrolovanyHrac = najdiHraca(kontrolovanyHrac.Meno);
                
            kontrolaSily = true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception pri behu kontrolneho vlakna " + ex);
            //}
        }

        private void zapisLog(string p)
        {
            log.AppendText(p);
        }
    }
}
