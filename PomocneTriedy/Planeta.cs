using System;
using System.Collections.Generic;

namespace WebBrowser.PomocneTriedy
{
    public class Planeta : IEquatable<Planeta>
    {
        public string Meno { get; set; }
        public string Pozicia { get; set; }
        public string Id { get; set; }
        public string Majitel { get; set; }
        public string Typ { get; set; }
        public string Sektor { get; set; }
        public bool FlagAktualny { get; set; }
        public string Vlozil { get; set; }
        public DateTime? DatumVlozenia { get; set; }
        public int PocetZmien { get; set; }

        public Planeta()
        {
            
        }

        public Planeta(string meno, string pozicia, string id, string majitel, string typ, string sektor, bool flagAktualny, string vlozil, DateTime? datumVlozenia)
        {
            Meno = meno;
            Pozicia = pozicia;
            Id = id;
            Majitel = majitel;
            Typ = typ;
            Sektor = sektor;
            FlagAktualny = flagAktualny;
            Vlozil = vlozil;
            DatumVlozenia = datumVlozenia;
        }

        public Planeta(string meno, string pozicia, string id, string majitel, string typ, string sektor, bool flagAktualny, string vlozil, DateTime? datumVlozenia, int pocetZmien)
        {
            Meno = meno;
            Pozicia = pozicia;
            Id = id;
            Majitel = majitel;
            Typ = typ;
            Sektor = sektor;
            FlagAktualny = flagAktualny;
            Vlozil = vlozil;
            DatumVlozenia = datumVlozenia;
            PocetZmien = pocetZmien;
        }

        public Planeta(string nazov, string pozicia, string majitel, DateTime datum, string typ, string sektor, string pocetZmien)
        {
            Meno = nazov;
            Pozicia = pozicia;
            Majitel = majitel;
            Typ = typ;
            Sektor = sektor;
            DatumVlozenia = datum;
            PocetZmien = int.Parse(pocetZmien);
        }

        public bool Equals(Planeta other)
        {
            if (other == null)
                return false;
     //       Console.WriteLine(other.ToString()+" ----  "+this.ToString());
           // var c = this.Pozicia.Equals(other.Pozicia);
           // var d = this.Pozicia.Equals(other.Pozicia) && this.FlagAktualny == true;
            return Majitel.Equals(other.Majitel) && Meno.Equals(other.Meno) && FlagAktualny;
        }

        public override string ToString()
        {
            return Id + " , " + Majitel + " , " + Meno + " , " + Pozicia+" , "+Sektor;
        }
    }

    public class PlanetaEqualityComparer : IEqualityComparer<Planeta>
    {
        public bool Equals(Planeta x, Planeta y)
        {
            //       Console.WriteLine(other.ToString()+" ----  "+this.ToString());
            // var c = this.Pozicia.Equals(other.Pozicia);
            // var d = this.Pozicia.Equals(other.Pozicia) && this.FlagAktualny == true;
            return x.Pozicia.Equals(y.Pozicia) && x.Sektor.Equals(y.Sektor);
        }

        public int GetHashCode(Planeta obj)
        {
            return obj.GetHashCode();
        }
    }
    
}
