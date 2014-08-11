namespace WebBrowser.Dohadzovanie
{
    public class Hrac
    {
        public override bool Equals(object o)
        {
            var e = string.Equals(Meno,((Hrac) o).Meno);
            return string.Equals(Meno, ((Hrac)o).Meno);
        }

        public string Meno { get; set; }
        public string PocetPlanet { get; set; }
        public string AktualnaSila { get; set; }
        public string ID { get; set; }


        public Hrac(string meno, string pocetPlanet, string aktualnaSila)
        {
            Meno = meno;
            PocetPlanet = pocetPlanet;
            AktualnaSila = aktualnaSila;
        }

        public Hrac(string meno, string pocetPlanet, string aktualnaSila, string id)
        {
            Meno = meno;
            PocetPlanet = pocetPlanet;
            AktualnaSila = aktualnaSila;
            ID = id;
        }
    }
}
