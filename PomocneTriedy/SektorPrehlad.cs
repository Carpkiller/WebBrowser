using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.PomocneTriedy
{
    public class SektorPrehlad
    {
        public string Sektor { get; set; }
        public int PocetPlanet { get; set; }
        public string Nadvlada { get; set; }
        public DateTime PoslednaAktualizacia { get; set; }

        public SektorPrehlad(string sektor, int pocetPlanet, string nadvlada, DateTime poslednaAktualizacia)
        {
            Sektor = sektor;
            PocetPlanet = pocetPlanet;
            Nadvlada = nadvlada;
            PoslednaAktualizacia = poslednaAktualizacia;
        }
    }
}
