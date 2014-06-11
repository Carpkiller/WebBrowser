using System;

namespace WebBrowser.PomocneTriedy
{
    public class SektorPlanety
    {
        public string Meno { get; set; }
        public string Pozicia { get; set; }
        public string Majitel { get; set; }
        public string Typ { get; set; }
        public string Sektor { get; set; }
        public DateTime? DatumVlozenia { get; set; }
        public int PocetZmien { get; set; }

        public SektorPlanety(string nazov, string pozicia, string majitel, DateTime datum, string typ, string sektor, string pocetZmien)
        {
            Meno = nazov;
            Pozicia = pozicia;
            Majitel = majitel;
            Typ = typ;
            Sektor = sektor;
            DatumVlozenia = datum;
            PocetZmien = int.Parse(pocetZmien);
        }
    }
}
