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
                case "0%":
                {
                    return double.Parse(Config.Vhodnost0);
                }
                case "+5%":
                {
                    return double.Parse(Config.Vhodnost5);
                }
                case "+10%":
                {
                    return double.Parse(Config.Vhodnost10);
                }
                case "-25%":
                {
                    return double.Parse(Config.Vhodnost25);
                }
                case "-50%":
                {
                    return double.Parse(Config.Vhodnost50);
                }
                default:
                    return double.Parse(Config.VhodnostDefault);
            }
        }

        public double KoeficientMesta()
        {
            var ret = 0;
            if (int.Parse(PocetMiest) >= 130)
            {
                return double.Parse(Config.PocetMiest130);
            }
            if (int.Parse(PocetMiest) >= 110)
            {
                return double.Parse(Config.PocetMiest110);
            }
            if (int.Parse(PocetMiest) >= 100)
            {
                return double.Parse(Config.PocetMiest100);
            }
            if (int.Parse(PocetMiest) >= 90)
            {
                return double.Parse(Config.PocetMiest90);
            }

            return double.Parse(Config.PocetMiestDefault);
        }
    }
}