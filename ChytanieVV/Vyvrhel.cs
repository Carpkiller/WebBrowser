namespace WebBrowser.ChytanieVV
{
    public class Vyvrhel
    {
        public string Meno { get; set; }
        public string Sila { get; set; }
        public int PocetPlanet { get; set; }
        public bool SystemovyHrac { get; set; }

        public Vyvrhel(string meno, string sila, int pocetPlanet)
        {
            this.Meno = meno;
            this.Sila = sila;
            this.PocetPlanet = pocetPlanet;
            SystemovyHrac = false || (meno == "Tartarus" || meno=="Ashrak");
        }
    }
}
