using System;
using System.Collections.Generic;
using System.Linq;

namespace WebBrowser.ChytanieNN
{
    public class SkoreCalculator
    {
        private List<ObchodPlaneta> listPlanet;

        public SkoreCalculator(IEnumerable<ObchodPlaneta> listPlanet)
        {
            this.listPlanet = (List<ObchodPlaneta>) listPlanet;
            //pocitajSkore();
        }

        public int pocitajSkore()
        {
            double min = 999999999;
            int index=0;
            foreach (var obchodPlaneta in listPlanet)
            {
                var koeficient = (double.Parse(obchodPlaneta.Cena)/double.Parse(listPlanet.First().Cena));
            //    Console.WriteLine("---  "+koeficient);
                Console.WriteLine(obchodPlaneta.Typ + " " + obchodPlaneta.Vhodnost + " " + obchodPlaneta.PocetMiest + " " + obchodPlaneta.Cena + "  - " + obchodPlaneta.Skore() * koeficient);
                if (obchodPlaneta.Skore() * koeficient < min)
                {
                    min = obchodPlaneta.Skore()*koeficient;
                    index = listPlanet.IndexOf(obchodPlaneta);
                }
            }
            Console.WriteLine("-*--- "+index);
            return index;
        }
    }
}
