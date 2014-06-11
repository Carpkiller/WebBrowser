using System;

namespace WebBrowser.ChytanieNN
{
    public class ObchodPlaneta
    {
        public string Typ { get; set; }
        public string Vhodnost { get; set; }
        public string PocetMiest { get; set; }
        public string Cena { get; set; }

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
           // Console.WriteLine(typ + " "+vhodnost+" "+pocetMiest+" "+cena +"  - "+Skore());
        }

        public double Skore()
        {
            double ret = 0;
            //ret += int.Parse(Cena);
            //ret += int.Parse(PocetMiest) * 500000;
            //ret -= int.Parse(Vhodnost.Replace("%","")) * 100000;

            ret = (int.Parse(Cena)/1000000)*KoeficientMesta()*KoeficientVhodnostSkore();
            return ret;
        }

        public double KoeficientVhodnostSkore()
        {
            switch (Vhodnost)
            {
                case "0%" :
                {
                   return 1;
                }
                case "+5%":
                {
                    return 0.95;
                }
                case "+10%":
                {
                    return 0.90;
                }
                case "-25%":
                {
                    return 1.05;
                }
                case "-50%":
                {
                    return 1.10;
                }
                default :
                    return 1;
            }
        }

        public double KoeficientMesta()
        {
            var ret = 0;
            if (int.Parse(PocetMiest) >= 130)
            {
                return 0.5;
            }
            if (int.Parse(PocetMiest) >= 110)
            {
                return 0.75;
            }
            if (int.Parse(PocetMiest) >= 100)
            {
                return 0.85;
            }
            if (int.Parse(PocetMiest) >= 100)
            {
                return 1;
            }
            if (int.Parse(PocetMiest) >= 90)
            {
                return 1.2;
            }

            return 1.5;
        }
    }
}
