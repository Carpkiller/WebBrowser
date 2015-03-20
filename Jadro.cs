using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using WebBrowser.ChytanieNN;
using WebBrowser.ChytanieTrolov;
using WebBrowser.ChytanieVV;
using WebBrowser.Dohadzovanie;
using WebBrowser.Hladanie;
using WebBrowser.OnlineDatabaza;
using WebBrowser.PomocneTriedy;

// ReSharper disable InconsistentNaming
namespace WebBrowser
{
    public class Jadro
    {
        private System.Windows.Forms.WebBrowser Wb { get; set; }
        private HlavneOkno Form { get; set; }
        private int AktualnySektor;
        private List<Planeta> AktualnyListPlanet;
        public List<SektorPrehlad> PrehladSektorovList { get; set; }
        public List<CelkovaTabulka> ListPlanetpar;
        public string User { get; set; }
        private readonly String _dbConnection;
        private int _pocetSpusteni;
        public int NovePlanety = 0;
        public String TypPlanety = "";
        private int _pocetCyklov;
        public DateTime CasPoslednehoDohozu { get; set; }
        public DohadzovanieAI AiDohadzovac { get; set; }
        public bool Povolene { get; set; }

        public delegate void ZmenaStavuHandler();

        public event ZmenaStavuHandler Zmeneno;

        public delegate void ZmenaStavuPoradiaHandler();

        public event ZmenaStavuPoradiaHandler ZmenaPoradia;
        public event ZmenaStavuPoradiaHandler ZmenaPoradiaHladania;

        public delegate void AktualizujAktualnySektorHandler();
        public event AktualizujAktualnySektorHandler AktualizujAktualnySektor;

        public delegate void UkonceniePraceVlakna();
        public event UkonceniePraceVlakna KoniecVlakna;

        public delegate void ZmenaSektoruHandler();
        public event ZmenaSektoruHandler ZmenaSektora;

        public delegate void ZmenaPlanetyHandler();
        public event ZmenaPlanetyHandler ZmenaPlanety;

        public delegate void UkoncenieHladaniePlanetRasyHandler();
        public event UkoncenieHladaniePlanetRasyHandler UkoncenieHladaniePlanetRasy;

        private Thread _refreshovacieVlakno;
        public Thread HladacieVlakno { get; set; }
        private Thread _vlakno;
        private int _refreshovaciCas;
        public string Heslo;
        public int CasMedziDohozmi;
        public int CasSpomalenia;
        private Thread hladacieVlakno;
        public List<HracPodmienky> AktualnaSchema { get; set; }
        public int AktualnaHodnota { get; set; }
        public List<Planeta> KoniecVlaknaList { get; set; }
        public List<SektorPlanety> NajdenePlanety { get; set; }
        public Planeta HladanaPlaneta { get; set; }

        public Jadro(System.Windows.Forms.WebBrowser wb, HlavneOkno form1)
        {
            Wb = wb;
            Form = form1;
            AktualnySektor = 0;
            _dbConnection = "Data Source=dba.sqlite";
            AktualnyListPlanet = LoadPlanety();
            PrehladSektorovList = LoadPrehladSektorov();
            CasPoslednehoDohozu = DateTime.Now;
            Povolene = false;
        }

        public Jadro()
        {
            // TODO: Complete member initialization
        }

        private List<SektorPrehlad> LoadPrehladSektorov()
        {
            var list = new List<SektorPrehlad>();

            var sql = "select count(nazov),sektor,datumVlozenia datum from planety where datum > datetime('" +
                      Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") + "') and flagAktualny = '1' group by sektor;";

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var sektor = reader.GetString(1);
                                var pocet = reader.GetInt32(0);
                                var datum = DateTime.ParseExact(reader.GetString(2), "yyyy-MM-dd HH:mm:ss",
                                    CultureInfo.InvariantCulture);

                                list.Add(new SektorPrehlad(sektor, pocet, "--", datum));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }

        private List<Planeta> LoadPlanety()
        {
            var list = new List<Planeta>();
            var sql =
                "select ID,nazov,majitel,pozicia,idPlanety,flagAktualny,vlozil,datetime(datumVlozenia),typ,sektor from planety;";
            var sql1 = "select * from planety where flagAktualny = '1' and datumVlozenia > datetime('" +Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") + "') ;";
            var sql2 = "select nazov from planety;";

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql1, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var nazov = reader["nazov"].ToString();
                                var majitel = reader["majitel"].ToString();
                                var pozicia = reader["pozicia"].ToString();
                                var id = reader["idPlanety"].ToString();
                                var flag = bool.Parse(reader["flagAktualny"].ToString());
                                var vlozil = reader["vlozil"].ToString();
                                var c = reader.GetString(9);
                                var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                var typ = reader["typ"].ToString();
                                var sektor = reader["sektor"].ToString();

                                list.Add(new Planeta(nazov, pozicia, id, majitel, typ, sektor, flag, vlozil, datum));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("no such table: planety"))
                {
                    using (TransactionScope tran = new TransactionScope())
                    {
                        using (SQLiteConnection DbConnection = new SQLiteConnection(_dbConnection))
                        {
                            DbConnection.Open();

                            sql =
                                "CREATE TABLE planety ([ID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,[idPlanety] VARCHAR(12)  NULL," +
                                "[nazov] VARCHAR(50)  NULL,[pozicia] VARCHAR(15)  NULL,[majitel] VARCHAR(35)  NULL,[typ] VARCHAR(25)  NULL," +
                                "[sektor] VARCHAR(10)  NULL,[flagAktualny] BOOLEAN  NULL,[vlozil] VARCHAR(35)  NULL,[datumVlozenia] DATE  NULL)";

                            using (SQLiteCommand command = new SQLiteCommand(sql, DbConnection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        tran.Complete();
                    }

                    using (TransactionScope tran = new TransactionScope())
                    {
                        using (SQLiteConnection DbConnection = new SQLiteConnection(_dbConnection))
                        {
                            DbConnection.Open();
                            sql = "CREATE TABLE Zoradenie ([ID] INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL , " +
                                  "[NazovRasy] VARCHAR, [PocetHracov] VARCHAR, [CP] INTEGER, [PP] INTEGER, " +
                                  "[Planety] INTEGER, [Populacia] INTEGER, [Rozloha] INTEGER, [DatumVlozenia] DATETIME DEFAULT CURRENT_TIMESTAMP)";

                            using (SQLiteCommand command = new SQLiteCommand(sql, DbConnection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        tran.Complete();
                    }
                    using (TransactionScope tran = new TransactionScope())
                    {
                        sql = "create table sektory (nazov varchar(5),datumVlozenia date);";
                        using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                        {
                            cnn.Open();
                            using (SQLiteCommand command = new SQLiteCommand(sql, cnn))
                            {
                                command.ExecuteNonQuery();
                            }
                            tran.Complete();
                        }
                    }
                }
            }
            return list;
        }

        private void SpracujData(string innerHtml, string outerHtml)
        {
            var listPlanet = ParsujPlanety(innerHtml, outerHtml);
            var listNaUlozenie = CheckPlanety(listPlanet);
            var listNaZmazanie = CheckPlanetyZmazanie(listPlanet);
            var sektorCislo = outerHtml.Substring(outerHtml.IndexOf("sektor=") + 7);

            var query = string.Empty;
            var zaciatok = true;

            for (int index = 0; index < listNaUlozenie.Count; index++)
            {
                var planeta = listNaUlozenie[index];
                if (zaciatok)
                {
                    query +=
                        "INSERT INTO Planety(idPlanety,vlozil,datumVlozenia,pozicia,nazov, majitel, sektor, flagAktualny)" +
                        " SELECT '" + planeta.Id + "' AS idPlanety, '" + planeta.Vlozil + "' AS vlozil, '" +
                        planeta.DatumVlozenia.Value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) +
                        "' AS datumVlozenia, '" + planeta.Pozicia + "' AS pozicia, '" + planeta.Meno +
                        "' AS nazov, '" + planeta.Majitel + "' AS majitel, '" + planeta.Sektor +
                        "' AS sektor, " + "1 AS flagAktualny ";
                    zaciatok = false;
                }
                else
                {
                    query += "UNION SELECT '" + planeta.Id + "','" + planeta.Vlozil + "','" +
                             planeta.DatumVlozenia.Value.ToString("yyyy-MM-dd HH:mm:ss",
                                 CultureInfo.InvariantCulture) + "','" +
                             planeta.Pozicia + "','" + planeta.Meno + "','" + planeta.Majitel + "','" +
                             planeta.Sektor + "', 1 ";
                }

                if (index%500 == 0)
                {
                    try
                    {
                        using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                        {
                            cnn.Open();
                            using (var mycommand = new SQLiteCommand(query, cnn))
                            {
                                mycommand.ExecuteNonQuery();
                            }
                            cnn.Close();
                        }
                        Console.WriteLine("Ukladanie do DB - sektor -> " + sektorCislo + " ,pocet zaznamov : " + listNaUlozenie.Count);
                    }
                    catch (Exception fail)
                    {
                        MessageBox.Show(fail.Message);
                    }
                    query = string.Empty;
                    zaciatok = true;
                }
            }

            try
            {
                using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (var mycommand = new SQLiteCommand(query, cnn))
                    {
                        mycommand.ExecuteNonQuery();
                    }
                    cnn.Close();
                }
                Console.WriteLine("Ukladanie do DB -- sektor -> " + sektorCislo + " ,pocet zaznamov : " + listNaUlozenie.Count);
            }
            catch (Exception fail)
            {
                MessageBox.Show(fail.Message);
            }

            if (listNaUlozenie.Count > 0)
            {
                PrehladSektorovList = LoadPrehladSektorov();
                if (ZmenaSektora != null) //vyvolani udalosti
                    ZmenaSektora();
            }
        }

        public void UlozData(string innerHtml, string outerHtml)
        {
            if (outerHtml.Contains("vesmir.php?page=13&id_sektor"))
            {
                Task.Factory.StartNew(() => SpracujData(innerHtml, outerHtml));
            }
            if (outerHtml.Contains("vesmir.php?page=3"))
            {
                //   ZmenaPoctuPlanet();
                ListPlanetpar = parsujRasy(innerHtml);
                ZmenaPoctuPlanet();
                var query = string.Empty;

                for (int index = 0; index < ListPlanetpar.Count; index++)
                {
                    var zaznam = ListPlanetpar[index];
                    if (index == 0)
                    {
                        query +=
                            "INSERT INTO Zoradenie(NazovRasy,PocetHracov,CP,PP,Planety,Populacia ,Rozloha )" +
                            " SELECT '" + zaznam.NazovRasy + "' AS NazovRasy, '" + zaznam.PocetHracov +
                            "' AS PocetHracov, " + zaznam.PocetCP + " AS CP, " +
                            zaznam.PocetPP + " AS PP, " + zaznam.PocetPlanet + " AS Planety, " + zaznam.Populacia +
                            " AS Populacia, " +
                            zaznam.Rozloha + " AS Rozloha ";
                    }
                    else
                    {
                        query += "UNION SELECT '" + zaznam.NazovRasy + "','" + zaznam.PocetHracov + "'," +
                                 zaznam.PocetCP + "," +
                                 zaznam.PocetPP + "," + zaznam.PocetPlanet + "," + zaznam.Populacia + "," +
                                 zaznam.Rozloha + " ";
                    }

                }

                try
                {
                    using (SQLiteConnection cnn = new SQLiteConnection(_dbConnection))
                    {
                        cnn.Open();
                        //var ee = cnn.State;
                        using (SQLiteCommand mycommand = new SQLiteCommand(query, cnn))
                        {
                            mycommand.ExecuteNonQuery();
                            Console.WriteLine("Ukladanie do DB");
                        }
                        cnn.Close();
                    }
                }
                catch (Exception fail)
                {
                    MessageBox.Show(fail.Message);
                }
            }
            if (outerHtml.Contains("stargate-game.cz/info.php"))
            {
                if (innerHtml.Contains("(<A class=rasa href=\"/vesmir.php?page=1&amp;id_rasa=6\">Tollán</A>)"))
                {
                    Povolene = true;
                }
                if (_pocetSpusteni == 0)
                {
                    SpravaHesiel.SpravaHesiel.UlozHeslo(User, Heslo);
                }
                NaskenujStatistiku();
            }

        }

        private void NaskenujStatistiku()
        {
            if (_pocetSpusteni == 0)
            {
                var wb1 = new System.Windows.Forms.WebBrowser();
                wb1.Navigate(new Uri("http://www.stargate-game.cz/vesmir.php?page=3"));
                wb1.DocumentCompleted +=
                    new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
                _pocetSpusteni++;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var o =
                ((System.Windows.Forms.WebBrowser) (sender)).Document.Body.InnerHtml;
            UlozData(o, "vesmir.php?page=3");
        }

        private List<CelkovaTabulka> parsujRasy(string innerHtml)
        {
            var listPlanetpar = new List<CelkovaTabulka>();
            var koniec = true;

            var startPozicia = innerHtml.IndexOf("WIDTH");
            innerHtml = innerHtml.Substring(startPozicia);
            while (koniec)
            {
                var index = innerHtml.IndexOf("blank>");
                var koncIndex = innerHtml.IndexOf("</A>");
                var rasa = innerHtml.Substring(index + 6, koncIndex - index - 6);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf("</TD>", index);
                var hraci = innerHtml.Substring(index + 4, koncIndex - index - 4);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf(" <", index + 1);
                var cp = innerHtml.Substring(index + 4, koncIndex - index - 4);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf("<", index + 1);
                var pp = innerHtml.Substring(index + 4, koncIndex - index - 4);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf(" <", index);
                var planety = innerHtml.Substring(index + 4, koncIndex - index - 4);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf("</TD>", index);
                var popka = innerHtml.Substring(index + 4, koncIndex - index - 4);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf("</TD>", index);
                index = innerHtml.IndexOf("<TD>", koncIndex);
                koncIndex = innerHtml.IndexOf("</TD>", index);
                var rozloha = innerHtml.Substring(index + 4, koncIndex - index - 4);

                innerHtml = innerHtml.Substring(koncIndex);

                if (innerHtml.Length < 225)
                {
                    koniec = false;
                }

                listPlanetpar.Add(new CelkovaTabulka(rasa, int.Parse(planety.Replace(" ", "")), hraci, int.Parse(cp),
                    int.Parse(pp),
                    int.Parse(popka.Replace(" ", "")), int.Parse(rozloha.Replace(" ", ""))));
            }

            return listPlanetpar;
        }

        private List<Planeta> CheckPlanety(IEnumerable<Planeta> listPlanety)
        {
            var list = new List<Planeta>();
            var listPlan = LoadPlanety();

            foreach (var planeta in listPlanety)
            {
                if (!listPlan.Contains(planeta))
                {
                    list.Add(planeta);
                }
            }
            return list;
        }

        private List<Planeta> CheckPlanetyZmazanie(List<Planeta> listPlanety)
        {
            var list = new List<Planeta>();
            var pomList = LoadPlanety();
            var listPlan = pomList.Where(x => x.Sektor == listPlanety.FirstOrDefault().Sektor && x.DatumVlozenia > Config.ZaciatokVeku).ToList();

            foreach (var planeta in listPlan)
            {
                if (!listPlanety.Contains(planeta))
                {
                    list.Add(planeta);
                }
            }
            return list;
        }

        private List<Planeta> ParsujPlanety(string innerHtml, string outerHtml)
        {
            var listPlanetpar = new List<Planeta>();
            var koniec = true;

            var startPozicia = innerHtml.IndexOf("Mapa vesmíru");
            innerHtml = innerHtml.Substring(startPozicia);
            var sektorIndex = innerHtml.IndexOf("Sektor");
            var sektorKoniec = innerHtml.IndexOf("</H2>");
            var sektor = innerHtml.Substring(sektorIndex + 7, sektorKoniec - sektorIndex - 7);
            var sektorCislo = outerHtml.Substring(outerHtml.IndexOf("sektor=") + 7);

            Console.WriteLine("Start : " + DateTime.Now.TimeOfDay);

            while (koniec)
            {
                var index = innerHtml.IndexOf("planeta=");
                var id = innerHtml.Substring(index + 8, 7);
                index = innerHtml.IndexOf("title=");
                var koncIndex = innerHtml.IndexOf(" - ");
                var planeta = innerHtml.Substring(index + 15, koncIndex - index - 15);
                index = innerHtml.IndexOf("shape=circle");
                var majitel = innerHtml.Substring(koncIndex + 3, index - koncIndex - 5);
                index = innerHtml.IndexOf("coords");
                koncIndex = innerHtml.IndexOf('"', index + 9);
                var pozicia = innerHtml.Substring(index + 8, (koncIndex - index - 8));
                innerHtml = innerHtml.Substring(koncIndex + 2);
                index = innerHtml.IndexOf("</MAP>", koncIndex);
                if (index < 25)
                {
                    koniec = false;
                }

                listPlanetpar.Add(new Planeta(planeta, pozicia, id, majitel, null, sektor, true, User, DateTime.Now));
            }

            Console.WriteLine("Koniec : " + DateTime.Now.TimeOfDay);
            AktualizujSektoryDb(sektorCislo);

            return listPlanetpar;
        }

        private void AktualizujSektoryDb(string sektor)
        {
            var sql = "update sektory set datumVlozenia =datetime('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "') where nazov='" + sektor + "';";
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        mycommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetDalsiSektor(int i)
        {
            var result = "";
            var url = Wb.Document.Window.Frames["hlavni"].Document.Url.ToString();
            if (i == 1)
            {
                var sektor = zistiSektor(url) + 1;
                if (sektor == 201)
                {
                    sektor = 0;
                }
                var novySektor = url.Remove(url.IndexOf("sektor") + 7) + sektor.ToString();
                result = novySektor;
            }
            if (i == -1)
            {
                var sektor = zistiSektor(url) - 1;
                if (sektor == 0)
                {
                    sektor = 200;
                }
                var novySektor = url.Remove(url.IndexOf("sektor") + 7) + sektor.ToString();
                result = novySektor;
            }

            return result;
        }

        private int zistiSektor(string toString)
        {
            var pozicia = toString.IndexOf("sektor");
            int sektor = 0;
            int.TryParse(toString.Substring(pozicia + 7), out sektor);

            return sektor;
        }

        public void ZmenaPoctuPlanet()
        {
            var sql = "select sum(planety) from Zoradenie where ID in (select ID from zoradenie group by nazovRasy);";
            var pocet = 0;

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(_dbConnection))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pocet = reader.GetInt32(0);
                            }
                        }
                    }
                }

                int aktPocet = ListPlanetpar.Sum(celkovaTabulka => celkovaTabulka.PocetPlanet);

                NovePlanety = (aktPocet - pocet);

                if (Zmeneno != null) //vyvolani udalosti
                    Zmeneno();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void SpustRefresh(string text)
        {
            Wb.Refresh();
            _refreshovaciCas = Convert.ToInt32((text));
            _refreshovacieVlakno = new Thread(WorkThreadFunction);
            _refreshovacieVlakno.Start();
        }

        public void WorkThreadFunction()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(_refreshovaciCas);
                    Wb.Refresh();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ZastavRefresh()
        {
            _refreshovacieVlakno.Abort();
        }

        public List<Hrac> parsujHracov(string html)
        {
            var index = html.IndexOf("<TBODY");
            html = html.Substring(index);
            index = html.IndexOf("DIV><!--");
            html = html.Remove(index - 10);
            var inputHtml = "<TABLE>" + html + "</TABLE>";
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(inputHtml);

            var list = new List<Hrac>();

            var poc = doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes.Count/2;

            for (int i = 0; i < doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes.Count/2; i++)
            {
                list.Add(
                    new Hrac(
                        doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes[i*2 + 1].ChildNodes[3].InnerText.Replace
                            (" ", ""),
                        doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes[i*2 + 1].ChildNodes[5].InnerText.Replace
                            (" ", ""),
                        doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes[i*2 + 1].ChildNodes[9].InnerText,
                        doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes[i * 2 + 1].ChildNodes[3].InnerHtml.Substring(doc.DocumentNode.ChildNodes[0].ChildNodes[0].ChildNodes[i * 2 + 1].ChildNodes[3].InnerHtml.IndexOf("id_hrac=")+8,7)));
            }

            return list;
        }

        public void VytvorDohadzovaca()
        {
            AiDohadzovac = new DohadzovanieAI();
        }

        public void SkontrolujHracov(IEnumerable<Hrac> datasource, object dohadzovanieForm, List<HracPodmienky> listPodm,
            bool check, int i)
        {

            foreach (var hrac in datasource)
            {
                foreach (var podmHrac in listPodm)
                {
                    if (hrac.Meno == podmHrac.Meno)
                    {
                        var sila = int.Parse(hrac.AktualnaSila.Replace(" ", ""));
                        if (i == 0) // na dohoz
                        {
                            if (sila < podmHrac.KritickaSila)
                            {
                                bool result;
                                var dohoz = AiDohadzovac.CalculateDohoz(sila, podmHrac, check, out result);
                                ((DohadzovanieForm) (dohadzovanieForm)).DohodHraca(dohoz);
                                if (result)
                                {
                                    ((DohadzovanieForm) (dohadzovanieForm)).NaplanujKontroluSily(podmHrac.Meno);
                                }
                            }
                        }
                        if (i == 2) // na utok
                        {
                            if (sila > podmHrac.KritickaSila)
                            {
                                ((ChytanieTrolovForm) (dohadzovanieForm)).ZastavRefresh();
                                ((ChytanieTrolovForm) (dohadzovanieForm)).Utok();
                            }
                        }
                    }
                }
            }

        }

        private bool DohozMozny()
        {
            var teraz = DateTime.Now;
            var rozdiel = teraz - CasPoslednehoDohozu;

            if (rozdiel.Seconds > 9)
            {
                return true;
            }
            return false;
        }

        public void PridajHraca(HracPodmienky hrac, List<HracPodmienky> listInput, out List<HracPodmienky> listPodm)
        {
            for (int i = 0; i < listInput.Count; i++)
            {
                if (listInput[i].Meno == hrac.Meno)
                {
                    listInput.RemoveAt(i);
                }
            }

            listInput.Add(hrac);

            listPodm = listInput;
        }

        public List<SektorPlanety> ZobrazSektor(string sektor)
        {
            var list = new List<SektorPlanety>();

            var sql =
                "select nazov,majitel,pozicia,datetime(datumVlozenia) as datumVlozenia,typ,sektor,count(majitel) as pocetZmien from planety where sektor = '" +
                sektor + "' and flagAktualny = '1' and datumVlozenia > datetime('" +Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") + "') group by pozicia;";

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var nazov = reader["nazov"].ToString();
                                var majitel = reader["majitel"].ToString();
                                var pozicia = reader["pozicia"].ToString();
                                var c = reader.GetString(3);
                                var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                var typ = reader["typ"].ToString();
                                //var sektorPlan = reader["sektor"].ToString();
                                var pocetZmien = reader["pocetZmien"].ToString();

                                list.Add(new SektorPlanety(nazov, pozicia, majitel, datum, typ, sektor, pocetZmien));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;

        }

        internal object ParsujPlanetyHraca(string hladSektor, string hladPozicia)
        {
            List<Planeta> list = new List<Planeta>();

            var sql =
                "select idPlanety,nazov,majitel,pozicia,typ,sektor,datetime(datumVlozenia) as datumVlozenia, vlozil from planety where sektor = '" +
                hladSektor + "' and pozicia= '" + hladPozicia + "' and flagAktualny = '1' ;";

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader["idPlanety"].ToString();
                                var nazov = reader["nazov"].ToString();
                                var majitel = reader["majitel"].ToString();
                                var pozicia = reader["pozicia"].ToString();
                                var sektor = reader["sektor"].ToString();
                                var vlozil = reader["vlozil"].ToString();
                                var c = reader["datumVlozenia"].ToString();
                                var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                var typ = reader["typ"].ToString();

                                list.Add(new Planeta(nazov, pozicia, id, majitel, typ, sektor, true, vlozil, datum));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }

        public void ZistiTypPlanety(Planeta planeta)
        {
            HladanaPlaneta = planeta;
            var idPlanety = planeta.Id;
            _pocetCyklov = 0;

            var wb1 = new System.Windows.Forms.WebBrowser();
            wb1.Navigate(new Uri("http://www.stargate-game.cz/vesmir/mapa/planeta.php?planeta=" + idPlanety));

            //wb1.DocumentCompleted += webBrowser2_DocumentCompleted;

            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(wb1.StatusText) && !wb1.StatusText.Contains("planeta.php?planeta="))
                    break;
            }

            if (wb1.Document.Body.InnerText.Contains("\r\nPlaneta: \r\nMajitel: \r\nRasa majitele: \r\nSíla majitele: \r\n\r\n\r\n\r\n\r\n\r\nTyp planety: \r\n\r\n"))
            {
                Console.WriteLine(@"Neexistuje");

                var sql = "update planety set flagAktualny = '0' where sektor='" + planeta.Sektor + "' and pozicia = '" + planeta.Pozicia + "';";
                TypPlanety = "Neexistuje";

                try
                {
                    using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                    {
                        cnn.Open();
                        using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                        {
                            mycommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                if (ZmenaPlanety != null) //vyvolani udalosti
                    ZmenaPlanety();
            }
            else
            {
                //MessageBox.Show(wb1.Document.Body.InnerText);
                var c = wb1.Document.GetElementsByTagName("input");
                var d = c.GetElementsByName("submit");

                c[1].InvokeMember("Click");
                Thread.Sleep(1000);

                while (true)
                {
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(wb1.StatusText) && !wb1.StatusText.Contains("planeta.php?planeta="))
                        break;
                }

                var text = wb1.Document.Body.InnerText;

                if (text.Contains("Nemáte dost družic nebo útoků"))
                {
                    MessageBox.Show("Nemáte dost družic nebo útoků", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var aa = text.IndexOf("Výsledek průzkumu:");
                    var bb = text.IndexOf("Typ planety:");
                    var cc = text.IndexOf("Výsledek průzkumu:") - text.IndexOf("Typ planety:") - 2;

                    if (text.IndexOf("Výsledek průzkumu:") != -1)
                    {
                        TypPlanety = text.Substring(text.IndexOf("Výsledek průzkumu:"),
                            text.IndexOf("Typ planety:") - text.IndexOf("Výsledek průzkumu:") - 6);
                    }
                    else
                    {
                        TypPlanety = string.Empty;
                    }
                    

                    if (ZmenaPlanety != null) //vyvolani udalosti
                        ZmenaPlanety();
                }
            }
        }

        public List<Planeta> ZmenPlanetyDb(string typPlanety, Planeta listPlanetHraca)
        {
            var list = new List<Planeta>();
            //var sektorPlanety = listPlanetHraca[listPlanetHraca.Count - 1].Sektor;
            //var poziciaPlanety = listPlanetHraca[listPlanetHraca.Count - 1].Pozicia;

            var sektorPlanety = HladanaPlaneta.Sektor;
            var poziciaPlanety = HladanaPlaneta.Pozicia;

            var sql =
                "update planety set typ='" + typPlanety + "', datumVlozenia =datetime('"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)+"') where sektor='" + sektorPlanety + "' and pozicia = '" +
                poziciaPlanety + "';";
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        mycommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


            var sql1 =
                "select idPlanety,nazov,majitel,pozicia,typ,sektor,datetime(datumVlozenia) as datumVlozenia, vlozil from planety where sektor = '" +
                sektorPlanety + "' and pozicia= '" + poziciaPlanety + "' and flagAktualny = '1' ;";

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql1, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader["idPlanety"].ToString();
                                var nazov = reader["nazov"].ToString();
                                var majitel = reader["majitel"].ToString();
                                var pozicia = reader["pozicia"].ToString();
                                var sektor = reader["sektor"].ToString();
                                var vlozil = reader["vlozil"].ToString();
                                var c = reader["datumVlozenia"].ToString();
                                var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                var typ = reader["typ"].ToString();

                                list.Add(new Planeta(nazov, pozicia, id, majitel, typ, sektor, true, vlozil, datum));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            // ---------------------------------
            //VlozZaznamOnlineDB(HladanaPlaneta);

            return list;
        }

        public void NajdiPlanetuTask(string nazovPlanety, HladanieForm hladanieForm)
        {
            var list = new List<Planeta>();

            HladacieVlakno = new Thread(() => NajdiPlanetuHelpTask(nazovPlanety, hladanieForm, out list));
            HladacieVlakno.SetApartmentState(ApartmentState.STA);
            HladacieVlakno.Start();

            AktualnaHodnota = 0;

        }

        private void NajdiPlanetuHelpTask(string nazovPlanety, HladanieForm hladanieForm, out List<Planeta> list)
        {
            list = NajdiPlanetu(nazovPlanety);
            KoniecVlaknaList = list;
            //if (KoniecVlakna != null) //vyvolani udalosti
            //    KoniecVlakna();
            hladanieForm.KoniecVlakana();
        }

        internal List<Planeta> NajdiPlanetu(string nazovPlanety)
        {
            var list = new List<Planeta>();

            var sql =
                "select nazov,majitel,pozicia,datetime(datumVlozenia) as datumVlozenia,typ,sektor, idPlanety,flagAktualny from planety where nazov = '" +
                nazovPlanety + "' and flagAktualny = '1' order by datumVlozenia asc;";

            try
            {
                using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (var mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (var reader = mycommand.ExecuteReader())
                        {
                            if (reader.HasRows == true)
                            {

                                while (reader.Read())
                                {
                                    var nazov = reader["nazov"].ToString();
                                    var majitel = reader["majitel"].ToString();
                                    var pozicia = reader["pozicia"].ToString();
                                    var c = reader.GetString(3);
                                    var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    var typ = reader["typ"].ToString();
                                    var sektor = reader["sektor"].ToString();
                                    var id = reader["idPlanety"].ToString();
                                    var vlozil = reader["idPlanety"].ToString();
                                    var exist = reader.GetBoolean(7);

                                    list.Add(new Planeta(nazov, pozicia, id, majitel, typ, sektor, exist, vlozil, datum));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            if (list.Count != 0)
            {
                var existuje = ExistujePlaneta(list[0].Id);
                if (!existuje)
                {
                    Console.WriteLine("Neexistuje");
                    list = VyhladajOnlinePlanetu(nazovPlanety);
                }
                else
                {
                    Console.WriteLine("Existuje");
                }
            }
            else
            {
                Console.WriteLine("NEExistuje");
                list = VyhladajOnlinePlanetu(nazovPlanety);
            }


            return list;
        }

        private List<Planeta> VyhladajOnlinePlanetu(string nazovPlanety)
        {
            var listSektorov = new List<string>();
            var sql = "select nazov from sektory order by datumVlozenia asc;";

            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (SQLiteDataReader reader = mycommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    var nazov = reader["nazov"].ToString();

                                    listSektorov.Add(nazov);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("no such table: sektory"))
                {
                    using (TransactionScope tran = new TransactionScope())
                    {
                        var sqlCreateTable = "create table sektory (nazov varchar(5),datumVlozenia date);";
                        var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection));
                        cnn.Open();

                        SQLiteCommand command = new SQLiteCommand(sqlCreateTable, cnn);
                        command.ExecuteNonQuery();

                        var sqlInsertTable = "INSERT INTO sektory Select '1' AS nazov, datetime('NOW') AS datumVlozenia";

                        for (int i = 2; i <= 200; i++)
                        {
                            sqlInsertTable += " Union select '" + i + "' , datetime('NOW')";
                        }
                        sqlInsertTable += ";";

                        command = new SQLiteCommand(sqlInsertTable, cnn);
                        command.ExecuteNonQuery();
                        tran.Complete();
                    }
                }
                else
                {
                    throw new Exception(e.Message);
                }
            }

            while (true)
            {
                if (!listSektorov.Any())
                {
                    break;
                }
                var sektor = listSektorov[0];
                var list = OverPolohuPlanetVSektore(sektor, 3000);
                listSektorov.RemoveAt(0);
                AktualnaHodnota ++;

                Console.WriteLine(" ---------------- " + AktualnaHodnota);

                if (ZmenaPoradiaHladania != null) //vyvolani udalosti
                    ZmenaPoradiaHladania();

                if (list.Select(x => x.Meno == nazovPlanety).Any())
                {
                    break;
                }
            }

            return NajdiPlanetu(nazovPlanety);
        }

        private IEnumerable<Planeta> OverPolohuPlanetVSektore(string sektor, int casSpomalenia)
        {
            try
            {
            var wb = new System.Windows.Forms.WebBrowser();
            var url = new Uri("http://www.stargate-game.cz/vesmir.php?page=13&id_sektor=" + sektor, true);
            wb.Navigate(url);

            int x = 0;
            while (x == 0)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(wb.StatusText) && !wb.StatusText.Contains("page=13&id_sektor="))
                    break;
            }

            var list = ParsujPlanety(wb.Document.Body.InnerHtml, wb.Document.Url.ToString());

            if (casSpomalenia == 0)
                SpracujData(wb.Document.Body.InnerHtml, wb.Document.Url.ToString());
            else
                UlozData(wb.Document.Body.InnerHtml, wb.Document.Url.ToString());

            Thread.Sleep(casSpomalenia);

            return list;

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return new List<Planeta>();
            }
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var wb = (System.Windows.Forms.WebBrowser) sender;
            UlozData(wb.Document.Window.Frames["hlavni"].Document.Body.InnerHtml,
                wb.Document.Window.Frames["hlavni"].Document.Url.ToString());
        }

        private bool ExistujePlaneta(string id)
        {
            var wb = new System.Windows.Forms.WebBrowser();
            var url = new Uri("http://www.stargate-game.cz/vesmir/mapa/planeta.php?planeta=" + id);
            wb.Navigate(url);

            Console.WriteLine(wb.StatusText);

            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(wb.StatusText))
                    break;
            }

            if (wb.Document.Body.InnerText.Length == 180)
            {
                return false;
            }

            return true;
        }

        public void VykonajPrihlasenie(string username, string password)
        {
            Wb.Navigate("http://www.stargate-game.cz");

            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(Wb.StatusText))
                    break;
            }

            Wb.Document.GetElementById("log-jmeno").SetAttribute("value", username);
            Wb.Document.GetElementById("log-heslo").SetAttribute("value", password);
            var c = Wb.Document.GetElementsByTagName("input");
            var d = c.GetElementsByName("prihlasit");

            d[1].InvokeMember("Click");
            Console.WriteLine("Login ... ");

            var uc = new DohadzovanieForm(this, Form, "1", AktualnaSchema, _refreshovaciCas, CasSpomalenia)
            {
                Visible = true
            };
            uc.Show();
        }

        internal List<ObchodPlaneta> ParsujPlanetyObchod(string html)
        {
            var list = new List<ObchodPlaneta>();
            var text = html;

            while (html.IndexOf("Nebyla nalezena žádná nabídka.") != -1)
            {

                text = text.Remove(0, text.IndexOf("<TD><STRONG style") + 36);
                var index = text.IndexOf("</STRONG>");
                var typ = text.Substring(0, index);

                text = text.Remove(0, text.IndexOf("<SPAN style=\"COLOR"));
                text = text.Remove(0, text.IndexOf("\">") + 2);
                var vhodnost = text.Substring(0, text.IndexOf("</SPAN>")).Replace(" ", "");

                text = text.Remove(0, text.IndexOf("<TD>") + 4);
                var mesta = text.Substring(0, text.IndexOf("</TD>")).Replace(" ", "");
                mesta = mesta.Substring(mesta.IndexOf('/') + 1);

                text = text.Remove(0, text.IndexOf("<TD>", 5) + 4);
                var popka = text.Substring(0, text.IndexOf("</TD>")).Replace(" ", "");

                text = text.Remove(0, text.IndexOf("<TD>", 5) + 4);
                var vyrobne = text.Substring(0, text.IndexOf("</TD>")).Replace(" ", "");

                text = text.Remove(0, text.IndexOf("<TD>", 5) + 4);
                var cena = text.Substring(0, text.IndexOf("</TD>")).Replace(" ", "");

                if (text.IndexOf("<TD><STRONG style") == -1)
                {
                    break;
                }

                list.Add(new ObchodPlaneta(typ, vhodnost, mesta, cena));
            }

            //VypocitajSkore(list);
            return list;
        }

        public int VypocitajSkore(List<ObchodPlaneta> list)
        {
            var najlepsiaPlaneta = new SkoreCalculator(list).pocitajSkore();
            return najlepsiaPlaneta;
        }

        internal bool CheckKupuPlanety(IEnumerable<ObchodPlaneta> listPlanet, string p)
        {
            return listPlanet.Any(obchodPlaneta => int.Parse(obchodPlaneta.Cena) < int.Parse(p));
        }

        public List<Vyvrhel> ParsujVyvrhelov(string innerHtml)
        {
            var list = new List<Vyvrhel>();
            var text = innerHtml.Substring(innerHtml.IndexOf("</THEAD>"));
            var pocetPlanet = 0;

            while (pocetPlanet != 1)
            {
                var c = text.IndexOf("<A class");
                text = text.Remove(0, text.IndexOf("\">", text.IndexOf("<A class")) + 2);
                var index = text.IndexOf("</A>");
                var meno = text.Substring(0, index);

                text = text.Remove(0, text.IndexOf("</TD>"));
                text = text.Remove(0, text.IndexOf("<TD>") + 4);
                pocetPlanet = int.Parse(text.Substring(0, text.IndexOf("</TD>")));

                text = text.Remove(0, text.IndexOf("<TD>") + 4);
                var sila = text.Substring(0, text.IndexOf("</TD>")).Replace(" ", "");

                text = text.Remove(0, text.IndexOf("</TR>"));

                if (pocetPlanet > 1)
                {
                    list.Add(new Vyvrhel(meno, sila, pocetPlanet));
                }

                if (text.IndexOf("<A class", StringComparison.Ordinal) == -1) break;
            }

            return list;
        }

        public void AktualizujDatum(DateTime value)
        {
            try
            {
                using (var fileStream = new StreamWriter("UserConfig.dat"))
                {
                    fileStream.Write(value);
                }
            }
            catch (Exception)
            {
            }
        }

        public DateTime NacitajConfig()
        {
            try
            {
                DateTime date;
                using (var fileStream = new StreamReader("UserConfig.dat"))
                {
                    date = DateTime.Parse(fileStream.ReadLine());
                }
                return date;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public List<Planeta> NajdiPlanetyHraca(string hrac)
        {
            var list = new List<Planeta>();

            var sql =
                "select *,(select count(majitel) as pocet from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia )," +
                "(select majitel from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia ) as poslMajitel  from " +
                "(select nazov,majitel,pozicia as poziciaa,datetime(datumVlozenia) as datumVlozenia,typ,sektor as sektorr, idPlanety from planety where majitel = '" +
                hrac + "' and flagAktualny = '1' and datumVlozenia > datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") +
                "') group by pozicia order by datumVlozenia desc) where poslMajitel = '"+hrac+"';";

            try
            {
                using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (var mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (var reader = mycommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var nazov = reader["nazov"].ToString();
                                    var majitel = reader["majitel"].ToString();
                                    var pozicia = reader["poziciaa"].ToString();
                                    var c = reader.GetString(3);
                                    var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    var typ = reader["typ"].ToString();
                                    var sektor = reader["sektorr"].ToString();
                                    var id = reader["idPlanety"].ToString();
                                    var vlozil = reader["idPlanety"].ToString();
                                    var pocetZmien = reader.GetInt32(7);

                                    list.Add(new Planeta(nazov, pozicia, id, majitel, typ, sektor, true, vlozil, datum, pocetZmien));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }

        public List<string> NacitajRasy()
        {
            var list = new List<string>();

            const string sql = "SELECT distinct(NazovRasy) FROM Zoradenie;";

            try
            {
                using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (var mycommand = new SQLiteCommand(sql, cnn))
                    {
                        using (var reader = mycommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    var nazov = reader[1].ToString();

                                    list.Add(nazov);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }

        public Dictionary<string, string> NacitajIdHracov(string rasa)
        {
            var wb = new System.Windows.Forms.WebBrowser();
            string id;
            RasyId.ListId.TryGetValue(rasa, out id);

            if (!string.IsNullOrEmpty(id))
            {
                var url = new Uri("http://www.stargate-game.cz/vesmir.php?page=1&id_rasa=" + id);
                wb.Navigate(url);

                while (true)
                {
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(wb.StatusText) && !wb.StatusText.Contains("vesmir.php?page=1&id_rasa="))
                        break;
                }

                if (wb.Document != null) if (wb.Document.Body != null) ParsujIdHracov(wb.Document.Body.InnerHtml);
            }
            return null;
        }

        private void ParsujIdHracov(string innerHtml)
        {
            var list = new List<Vyvrhel>();
            var text = innerHtml.Substring(innerHtml.IndexOf("</THEAD>", StringComparison.Ordinal));
            var pocetPlanet = 0;

            while (pocetPlanet != 1)
            {

                text = text.Remove(0,
                    text.IndexOf("?id_hrac", text.IndexOf("<A class", StringComparison.Ordinal),
                        StringComparison.Ordinal) + 9);
                var index = text.IndexOf("\">", StringComparison.Ordinal);

                text = text.Remove(0, index + 2);
                index = text.IndexOf("</A>", StringComparison.Ordinal);

                var meno = text.Substring(0, index);

                text = text.Remove(0, text.IndexOf("</TD>", StringComparison.Ordinal));
                text = text.Remove(0, text.IndexOf("<TD>", StringComparison.Ordinal) + 4);
                pocetPlanet = int.Parse(text.Substring(0, text.IndexOf("</TD>", StringComparison.Ordinal)));

                text = text.Remove(0, text.IndexOf("<TD>", StringComparison.Ordinal) + 4);
                var sila = text.Substring(0, text.IndexOf("</TD>", StringComparison.Ordinal)).Replace(" ", "");

                text = text.Remove(0, text.IndexOf("</TR>", StringComparison.Ordinal));

                if (pocetPlanet > 1)
                {
                    list.Add(new Vyvrhel(meno, sila, pocetPlanet));
                }
            }
        }

        public void AktualizujConfig(object text, int riadok)
        {
            try
            {
                if (!File.Exists("UserConfig.dat"))
                    File.Create("UserConfig.dat").Close();

                var txtLines = new List<string>();
                var s = new StreamReader("UserConfig.dat");
                string line;
                while ((line = s.ReadLine()) != null)
                {
                    txtLines.Add(line);
                }
                s.Close();

                if (txtLines.ElementAtOrDefault(riadok) != null)
                {
                    txtLines.RemoveAt(riadok);
                    txtLines.Insert(riadok, text.ToString());
                }
                else
                {
                    for (int i = 0; i < riadok; i++)
                    {
                        if (txtLines.ElementAtOrDefault(i) == null)
                        {
                            txtLines.Add(null);
                        }
                    }
                    txtLines.Add(text.ToString());
                }

                var fileStream = new StreamWriter("UserConfig.dat");

                foreach (var txtLine in txtLines)
                {
                    fileStream.WriteLine(txtLine);
                }

                fileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void NaskenujVesmir()
        {
            try
            {
                _vlakno = new Thread(SkenovacieVlakno);
                _vlakno.SetApartmentState(ApartmentState.STA);
                _vlakno.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void SkenovacieVlakno()
        {
            int poc = 0;
            for (int i = 1; i <= 200; i++)
            {
                AktualnaHodnota = i;

                var sektor = i.ToString(CultureInfo.InvariantCulture);
                var list = OverPolohuPlanetVSektore(sektor, 3000);

                Console.WriteLine(@" Aktualny sektor :  " + i);
                poc += list.Count();

                if (ZmenaPoradia != null) //vyvolani udalosti
                    ZmenaPoradia();

                if (AktualizujAktualnySektor != null)
                    AktualizujAktualnySektor();
            }

            MessageBox.Show(@"Pocet aktualizovanych planet : " + poc, "", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public void ZastavHladanie()
        {
            _vlakno.Abort();

            AktualnaHodnota = 200;

            if (AktualizujAktualnySektor != null)
                AktualizujAktualnySektor();
        }

        public void VypisPlanetyRasy(string rasa, int sektor)
        {
            var listHracov = new List<string>();

            var wb = new System.Windows.Forms.WebBrowser();
            string idRasy;
            RasyId.ListId.TryGetValue(rasa, out idRasy);

            if (!string.IsNullOrEmpty(idRasy))
            {
                var url = new Uri("http://www.stargate-game.cz/vesmir.php?page=1&id_rasa=" + idRasy + "&sort=2");
                wb.Navigate(url);

                while (true)
                {
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(wb.StatusText) && !wb.StatusText.Contains("vesmir.php?page=1&id_rasa="))
                        break;
                }

                if (wb.Document != null)
                    if (wb.Document.Body != null)
                        listHracov = ParsujVyvrhelov(wb.Document.Body.InnerHtml).Select(x => x.Meno).ToList();
            }

            if (listHracov.Any())
            {
                Thread t = new Thread(() => VyhladajPlanetyRasa(listHracov, sektor));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
        }

        private void VyhladajPlanetyRasa(IEnumerable<string> listHracov, int hladSektor)
        {
            NajdenePlanety = new List<SektorPlanety>();
            var hraci = listHracov.Aggregate("(", (current, s) => current + ("'" + s + "', "));
            hraci += ")";
            hraci = hraci.Replace(", )", ")");

            try
            {
                using (var cmd = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cmd.Open();
                    using (var mycommand = new SQLiteCommand("CREATE INDEX Iplanety2index ON planety(majitel, sektor, pozicia, datumVlozenia );", cmd))
                    {
                        mycommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine(@"Chyba pri vytvarani indexu");
            }

            string sql;

            if (hladSektor == 0)
            {
                sql =
                    "select *,(select count(majitel) as pocet from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia )," +
                    "(select majitel from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia ) as poslMajitel from " +
                    "(select nazov,majitel,pozicia as poziciaa,datetime(datumVlozenia) as datumVlozenia,typ,sektor as sektorr, idPlanety from planety where majitel in" +
                    hraci + " and datumVlozenia > datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") +
                    "') and flagAktualny = '1' group by pozicia order by datumVlozenia desc) where poslMajitel in "+hraci+";";
            }
            else
            {
                string nazovSektora;
                nazovSektora = SektoryID.ListId.TryGetValue(hladSektor, out nazovSektora)
                    ? nazovSektora
                    : hladSektor.ToString(CultureInfo.InvariantCulture);

                sql =
                    "select *,(select count(majitel) as pocet from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia ), " +
                    "(select majitel from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia ) as poslMajitel from " +
                    "(select nazov,majitel,pozicia as poziciaa,datetime(datumVlozenia) as datumVlozenia,typ,sektor as sektorr, idPlanety from planety where majitel in" +
                    hraci + " and datumVlozenia > datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") +
                    "') and sektor = '" + nazovSektora + "' and flagAktualny = '1' group by pozicia order by datumVlozenia desc) where poslMajitel in " + hraci + ";";
            }

            try
            {
                var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection));
                cnn.Open();
                using (var mycommand = new SQLiteCommand(sql, cnn))
                {
                    using (var reader = mycommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var nazov = reader["nazov"].ToString();
                                var majitel = reader["majitel"].ToString();
                                var pozicia = reader["poziciaa"].ToString();
                                var c = reader.GetString(3);
                                var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                var typ = reader["typ"].ToString();
                                var sektor = reader["sektorr"].ToString();
                                var pocetZmien = reader.GetInt32(7).ToString(CultureInfo.InvariantCulture);

                                NajdenePlanety.Add(new SektorPlanety(nazov, pozicia, majitel, datum, typ, sektor, pocetZmien));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            try
            {
                using (var cmd = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cmd.Open();
                    using (var mycommand = new SQLiteCommand("drop INDEX Iplanety2index;", cmd))
                    {
                        mycommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine(@"Chyba pri dropovani indexu");
            }

            if (UkoncenieHladaniePlanetRasy!=null)
            {
                UkoncenieHladaniePlanetRasy();
            }
        }

        public bool CheckSpojenie()
        {
            var databaza = new DBConnect();
            return databaza.CheckConnectionState();
        }

        public void UploadOnlineData(DateTime zaciatokVeku)
        {
            var poslednyUpload = zaciatokVeku;

            if (CheckSpojenie())
            {
                var sql = "select * from planety where datumVlozenia > STR_TO_DATE('" +
                          poslednyUpload.ToString("yyyy-MM-dd HH:mm:ss") + "', '%Y-%m-%d %H:%i:%s' );";
                
                var databaza = new DBConnect();
                var prijatePlanety = databaza.Select(sql);

                var list = new List<Planeta>();
                var sqlLocal = "select nazov,majitel,pozicia,datetime(datumVlozenia) as datumVlozenia,typ,sektor, idPlanety, vlozil from planety where typ is not null and flagAktualny = '1' and datumVlozenia > datetime('" + poslednyUpload.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                try
                {
                    using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                    {
                        cnn.Open();
                        using (var mycommand = new SQLiteCommand(sqlLocal, cnn))
                        {
                            using (var reader = mycommand.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {

                                    while (reader.Read())
                                    {
                                        var nazov = reader["nazov"].ToString();
                                        var majitel = reader["majitel"].ToString();
                                        var pozicia = reader["pozicia"].ToString();
                                        var c = reader.GetString(3);
                                        var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                        var typ = reader["typ"].ToString();
                                        var sektor = reader["sektor"].ToString();
                                        var id = reader["idPlanety"].ToString();
                                        var vlozil = reader["vlozil"].ToString();

                                        list.Add(new Planeta(nazov, pozicia, id, majitel, typ, sektor, true, vlozil, datum));
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                var listNaVlozenie = list.Where(x => !prijatePlanety.Contains(x, new PlanetaEqualityComparer())).ToList();

                var ukladanie = databaza.Insert(listNaVlozenie);

                MessageBox.Show(@"Celkovy pocet planet "+list.Count+Environment.NewLine+@"Pocet dotazov " + listNaVlozenie.Count+Environment.NewLine+@"Pocet vlozenych "+ukladanie,@"Info",MessageBoxButtons.OK,MessageBoxIcon.Information);

                AktualizujConfig(DateTime.Now,2);
            }
        }

        public void StiahniOnlineData(DateTime datum)
        {
            var poslednyDownload = datum;

            if (CheckSpojenie())
            {
                var sql = "select * from planety where datumVlozenia > STR_TO_DATE('" +
                          poslednyDownload.ToString("yyyy-MM-dd HH:mm:ss") + "', '%Y-%m-%d %H:%i:%s' ) and typ is not null;";

                var databaza = new DBConnect();
                var prijatePlanety = databaza.Select(sql);

                //var list = new List<Planeta>();
                //var sqlLocal =
                //    "select nazov,majitel,pozicia,datetime(datumVlozenia) as datumVlozenia,typ,sektor, idPlanety from planety where datumVlozenia > datetime('" +
                //    poslednyDownload.ToString("yyyy-MM-dd hh:mm:ss") + "');";

                var listNaVlozenie = prijatePlanety;
                //var listNaVlozenie =prijatePlanety.Where(x => !list.Contains(x, new PlanetaEqualityComparer())).ToList();
                //var listNaUpdate = prijatePlanety.Where(x => !list.Contains(x, new PlanetaEqualityComparer())).ToList();

                foreach (var planeta in listNaVlozenie)
                {
                    var query = "update planety set typ='" + planeta.Typ + "', datumVlozenia =datetime('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "') where sektor='" + planeta.Sektor +
                                "' and pozicia = '" + planeta.Pozicia + "';";
                    int pocetUpdateRows = 0;
                    try
                    {
                        using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                        {
                            cnn.Open();
                            using (var mycommand = new SQLiteCommand(query, cnn))
                            {
                                pocetUpdateRows = mycommand.ExecuteNonQuery();
                            }
                        }
                        Console.WriteLine(@"Update planeta " + planeta.Meno + @" sektor : " + planeta.Sektor);
                    }
                    catch (Exception fail)
                    {
                        MessageBox.Show(fail.Message, "Exception pri ukladani novych planet");
                    }

                    if (pocetUpdateRows == 0)
                    {
                        OverPolohuPlanetVSektore(planeta.Sektor, 0);

                        query = "update planety set typ='" + planeta.Typ + "', datumVlozenia =datetime('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "') where sektor='" + planeta.Sektor +
                                "' and pozicia = '" + planeta.Pozicia + "';";
                        try
                        {
                            using (var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                            {
                                cnn.Open();
                                using (var mycommand = new SQLiteCommand(query, cnn))
                                {
                                    mycommand.ExecuteNonQuery();
                                }
                            }
                            Console.WriteLine(@"Update po vkladani planeta " + planeta.Meno + @" sektor : " +
                                              planeta.Sektor);
                        }
                        catch (Exception fail)
                        {
                            MessageBox.Show(fail.Message, "Exception pri ukladani novych planet, ak predtym ...");
                        }
                    }
                }

                MessageBox.Show(@"Pocet vlozenych " + listNaVlozenie.Count,@"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            
            AktualizujConfig(DateTime.Now, 3);
        }

        public List<string> NaplnSektory()
        {
            var list = new List<string>();

            for (int i = 0; i <= 200; i++)
            {
                string nazovSektora;
                list.Add(SektoryID.ListId.TryGetValue(i, out nazovSektora)
                    ? nazovSektora
                    : i.ToString(CultureInfo.InvariantCulture));
            }

            return list;
        }

        public string ExportPlanetString(object dataSource, bool meno, bool sektor, bool typ)
        {
            var list = ((List<SektorPlanety>) dataSource).OrderBy(x => x.Majitel);

            if (typ)
            {
                return list.Where(x => x.Typ != string.Empty).Aggregate("", (current, planeta) => current + (planeta.Sektor + " - " + planeta.Majitel + " - " + planeta.Meno + (!string.IsNullOrEmpty(planeta.Typ) ? " ,typ planety - " + planeta.Typ : string.Empty) + Environment.NewLine));
            }

            return list.Aggregate("", (current, planeta) => current + (planeta.Sektor + " - " + planeta.Majitel + " - " + planeta.Meno + (!string.IsNullOrEmpty(planeta.Typ) ? " ,typ planety - " + planeta.Typ : string.Empty) + Environment.NewLine));
        }

        public void HladajHB(string cas, string cena)

        {
            if (hladacieVlakno == null)
            {
                hladacieVlakno = new Thread(() => HladajHb(cas, cena));
                hladacieVlakno.SetApartmentState(ApartmentState.STA);
                hladacieVlakno.Start();
            }
            else
            {
                hladacieVlakno.Abort();
            }
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

        public List<Planeta> NajdiZmenenePlanety(string povodnaRasa, string sucasnaRasa)
        {
            var listHracovPovodna = VypisHracovRasy(povodnaRasa);
            var listHracovSucasna = VypisHracovRasy(sucasnaRasa);
            var hraciPov = listHracovPovodna.Aggregate("(", (current, s) => current + ("'" + s + "', "));
            hraciPov += ")";
            hraciPov = hraciPov.Replace(", )", ")");
            var hraciSuc = listHracovSucasna.Aggregate("(", (current, s) => current + ("'" + s + "', "));
            hraciSuc += ")";
            hraciSuc = hraciSuc.Replace(", )", ")");

            var list = new List<Planeta>();

            //var sql =
            //    "select * from planety p inner join (select nazov,majitel,pozicia as poziciaa,datetime(datumVlozenia) as datumVlozenia," +
            //    "typ,sektor as sektorr, idPlanety from planety where majitel in  " + hraciPov +
            //    " and datumVlozenia >datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") +
            //    "') and flagAktualny = '1') ss on (ss.poziciaa = p.pozicia and ss.sektorr=p.sektor)" +
            //    "where p.majitel in " + hraciSuc;

            var sql =
                "select *, "+
                "(select count(majitel) as pocet from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia )," +
                "(select nazov as nazovv from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia ) as nazov," +
                "(select majitel from planety where sektor = sektorr and poziciaa = pozicia and flagAktualny = '1' group by pozicia ) as poslMajitel  from " +
                "(select majitel,pozicia as poziciaa,datetime(datumVlozenia) as datumVlozenia,typ,sektor as sektorr, idPlanety from planety where majitel in " +
                hraciSuc + " and flagAktualny = '1' and datumVlozenia > datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") +
                "') group by pozicia order by datumVlozenia desc) where poslMajitel in " + hraciPov + ";";
            try
            {
                var cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection));
                cnn.Open();
                using (var mycommand = new SQLiteCommand(sql, cnn))
                {
                    using (var reader = mycommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                var nazov = reader["nazov"].ToString();
                                var majitel = reader["poslMajitel"].ToString();
                                var pozicia = reader["poziciaa"].ToString();
                                var c = reader.GetString(2);
                                var datum = DateTime.ParseExact(c, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                var typ = reader["typ"].ToString();
                                var sektor = reader["sektorr"].ToString();
                                var pocetZmien = reader.GetInt32(6).ToString(CultureInfo.InvariantCulture);
                                var predch = reader["majitel"].ToString();

                                list.Add(new Planeta(nazov, pozicia, majitel, datum, typ, sektor, pocetZmien, predch));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return list;
        }

        private List<string> VypisHracovRasy(string nazovRasy)
        {
            var listHracov = new List<string>();

            var wb = new System.Windows.Forms.WebBrowser();
            string idRasy;
            RasyId.ListId.TryGetValue(nazovRasy, out idRasy);

            if (!string.IsNullOrEmpty(idRasy))
            {
                var url = new Uri("http://www.stargate-game.cz/vesmir.php?page=1&id_rasa=" + idRasy + "&sort=2");
                wb.Navigate(url);

                while (true)
                {
                    Application.DoEvents();
                    if (!string.IsNullOrEmpty(wb.StatusText) && !wb.StatusText.Contains("vesmir.php?page=1&id_rasa="))
                        break;
                }

                if (wb.Document != null)
                    if (wb.Document.Body != null)
                        listHracov = ParsujVyvrhelov(wb.Document.Body.InnerHtml).Select(x => x.Meno).ToList();
            }

            return listHracov;
        }

        public bool OdstranStareZaznamy()
        {
            var sql = "INSERT INTO planety_stare SELECT * FROM planety where datumVlozenia < datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            var sqlDelete = "DELETE from planety where datumVlozenia < datetime('" + Config.ZaciatokVeku.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            try
            {
                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sql, cnn))
                    {
                        mycommand.ExecuteReader();
                    }
                }

                using (SQLiteConnection cnn = new SQLiteConnection(new SQLiteConnection(_dbConnection)))
                {
                    cnn.Open();
                    using (SQLiteCommand mycommand = new SQLiteCommand(sqlDelete, cnn))
                    {
                        mycommand.ExecuteReader();
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("no such table: planety_stare"))
                {
                    using (TransactionScope tran = new TransactionScope())
                    {
                        using (SQLiteConnection DbConnection = new SQLiteConnection(_dbConnection))
                        {
                            DbConnection.Open();

                            sql =
                                "CREATE TABLE planety_stare ([ID] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,[idPlanety] VARCHAR(12)  NULL," +
                                "[nazov] VARCHAR(50)  NULL,[pozicia] VARCHAR(15)  NULL,[majitel] VARCHAR(35)  NULL,[typ] VARCHAR(25)  NULL," +
                                "[sektor] VARCHAR(10)  NULL,[flagAktualny] BOOLEAN  NULL,[vlozil] VARCHAR(35)  NULL,[datumVlozenia] DATE  NULL)";

                            using (SQLiteCommand command = new SQLiteCommand(sql, DbConnection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        tran.Complete();
                    }
                }

                return false;
            }

            return true;
        }
    }
}

// ReSharper restore InconsistentNaming