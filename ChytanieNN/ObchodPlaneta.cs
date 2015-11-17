using System;
using System.Globalization;

namespace WebBrowser.ChytanieNN
{
    public class ObchodPlaneta
    {
        public string Typ { get; set; }
        public string Vhodnost { get; set; }
        public string PocetMiest { get; set; }
        public string Cena { get; set; }
        public string Koeficient { get; set; }

        public ObchodPlaneta(string typ, string vhodnost, string pocetMiest, string cena)
        {
            if (vhodnost == "~")
            {
                vhodnost = "0%";
            }
            Typ = typ;
            Vhodnost = vhodnost;
            PocetMiest = pocetMiest;
            Cena = cena;
            Koeficient = Skore().ToString(CultureInfo.InvariantCulture);
             Console.WriteLine(typ + " "+vhodnost+" "+pocetMiest+" "+cena +"  - "+Skore());
        }

        public double Skore()
        {
            double ret = 0;
            //ret += int.Parse(Cena);
            //ret += int.Parse(PocetMiest) * 500000;
            //ret -= int.Parse(Vhodnost.Replace("%","")) * 100000;

            ret = int.Parse(Cena)/1000000*KoeficientMesta()*KoeficientVhodnostSkore();
            return ret;
        }

        public double KoeficientVhodnostSkore()
        {
            switch (Vhodnost)
            {
                case "0%":
                {
                    return double.Parse(Config.Vhodnost0)/100;
                }
                case "+5%":
                {
                    return double.Parse(Config.Vhodnost5)/100;
                }
                case "+10%":
                {
                    return double.Parse(Config.Vhodnost10)/100;
                }
                case "-25%":
                {
                    return double.Parse(Config.Vhodnost25)/100;
                }
                case "-50%":
                {
                    return double.Parse(Config.Vhodnost50)/100;
                }
                default:
                    return double.Parse(Config.VhodnostDefault)/100;
            }
        }

        public double KoeficientMesta()
        {
            if (int.Parse(PocetMiest) >= 130)
            {
                return double.Parse(Config.PocetMiest130)/100;
            }
            if (int.Parse(PocetMiest) >= 110)
            {
                return double.Parse(Config.PocetMiest110)/100;
            }
            if (int.Parse(PocetMiest) >= 100)
            {
                return double.Parse(Config.PocetMiest100)/100;
            }
            if (int.Parse(PocetMiest) >= 90)
            {
                return double.Parse(Config.PocetMiest90)/100;
            }

            return double.Parse(Config.PocetMiestDefault)/100;
        }
    }
}