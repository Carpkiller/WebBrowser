using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBrowser.ChytanieNN;

namespace WebBrowser
{
    public class LogHelper
    {
        public static void UlozPlanetyObchod(List<ObchodPlaneta> listPlanet)
        {
            var inputString = System.DateTime.Now.TimeOfDay.ToString()+Environment.NewLine;
            double min = 999999999;
            int index = 0;

            foreach (var obchodPlaneta in listPlanet)
            {
                var koeficient = (double.Parse(obchodPlaneta.Cena) / double.Parse(listPlanet.First().Cena));
                //    Console.WriteLine("---  "+koeficient);
                inputString += String.Format("{0}  {1}  {2}  {3}  {4}  {5}", obchodPlaneta.Typ, obchodPlaneta.Vhodnost, obchodPlaneta.PocetMiest,
                    obchodPlaneta.Cena, obchodPlaneta.Skore()*koeficient, Environment.NewLine);
                //Console.WriteLine(obchodPlaneta.Typ + " " + obchodPlaneta.Vhodnost + " " + obchodPlaneta.PocetMiest + " " + obchodPlaneta.Cena + "  - " + obchodPlaneta.Skore() * koeficient);
                if (obchodPlaneta.Skore() * koeficient < min)
                {
                    min = obchodPlaneta.Skore() * koeficient;
                    index = listPlanet.IndexOf(obchodPlaneta);
                }
            }
            inputString += "Vybrata planeta " + index;
            //Console.WriteLine("-*--- " + index);

            try
            {
                if (!Directory.Exists("Logy"))
                    Directory.CreateDirectory("Logy");

                var fileName = "Logy\\LogNN__" + DateTime.Now.TimeOfDay.ToString().Replace('.', '_').Replace(':','_') + ".txt";
                Console.WriteLine(fileName);
                var fileStream = new StreamWriter(fileName);

                fileStream.Write(inputString);

                fileStream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
