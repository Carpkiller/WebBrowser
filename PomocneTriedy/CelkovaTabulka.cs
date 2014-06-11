namespace WebBrowser.PomocneTriedy
{
    public class CelkovaTabulka
    {
        public string NazovRasy { get; set; }
        public string PocetHracov { get; set; }
        public int PocetCP { get; set; }
        public int PocetPP { get; set; }
        public int PocetPlanet { get; set; }
        public int Populacia { get; set; }
        public int Rozloha { get; set; }

        public CelkovaTabulka(string nazov, int pocetPlanet, string pocetHracov, int pocetCp, int pocetPp, int populacia, int rozloha)
        {
            NazovRasy = nazov;
            PocetPlanet = pocetPlanet;
            PocetHracov = pocetHracov;
            PocetCP = pocetCp;
            PocetPP = pocetPp;
            Populacia = populacia;
            Rozloha = rozloha;
        }
    }
}
