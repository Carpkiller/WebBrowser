using System;
namespace WebBrowser.Dohadzovanie
{
    public class DohadzovaciePocty : IEquatable<DohadzovaciePocty>
    {
        public string Meno { get; set; }
        public int SilaDohodu { get; set; }

        public DohadzovaciePocty(string meno, int sila)
        {
            Meno = meno;
            SilaDohodu = sila;
        }

        public bool Equals(DohadzovaciePocty o)
        {
            var oo = this.Meno == o.Meno;
            return this.Meno==o.Meno;
        }
    }
}
