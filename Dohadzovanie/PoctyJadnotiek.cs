namespace WebBrowser.Dohadzovanie
{
    public class PoctyJadnotiek
    {
        public string Meno { get; set; }
        public string Pechota { get; set; }
        public string Uni { get; set; }
        public string Orbit { get; set; }
        public string Elitaci { get; set; }

        public PoctyJadnotiek()
        {
        }

        public PoctyJadnotiek(string meno, string pechota, string uni, string orbit, string elitaci)
        {
            Meno = meno;
            Pechota = pechota;
            Uni = uni;
            Orbit = orbit;
            Elitaci = elitaci;
        }
    }
}
