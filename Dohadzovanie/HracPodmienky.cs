using System;

namespace WebBrowser.Dohadzovanie
{
    [Serializable()]
    public class HracPodmienky
    {
        public string Meno { get; set; }
        public int KritickaSila { get; set; }
        public int Jedn1_RO { get; set; }
        public int Jedn2_Uni { get; set; }
        public int Jedn3_Orb { get; set; }
        public int Jedn4_EB { get; set; }

        public HracPodmienky(string meno, int kritickaSila, int jedn1Ro, int jedn2Uni, int jedn3Orb, int jedn4Eb)
        {
            Meno = meno;
            KritickaSila = kritickaSila;
            Jedn1_RO = jedn1Ro;
            Jedn2_Uni = jedn2Uni;
            Jedn3_Orb = jedn3Orb;
            Jedn4_EB = jedn4Eb;
        }
    }
}
