using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser.Dohadzovanie
{
    public class DohadzovanieAI
    {
       public List<DohadzovaciePocty> listDohodov { get; set; } 

        public DohadzovanieAI()
        {
            listDohodov = new List<DohadzovaciePocty>();
        }


        public PoctyJadnotiek CalculateDohoz(int sila, HracPodmienky hracInput, bool check, out bool result)
        {
            result = false;
            var output = new PoctyJadnotiek()
            {
                Meno = hracInput.Meno
            };

            if (!check)
            {
                output.Pechota = hracInput.Jedn1_RO.ToString();
                output.Uni = hracInput.Jedn2_Uni.ToString();
                output.Orbit = hracInput.Jedn3_Orb.ToString();
                output.Elitaci = hracInput.Jedn4_EB.ToString();
            }
            else
            {
                var hrac = hracInput.Meno;

                if (!listDohodov.Contains(new DohadzovaciePocty(hrac,0)))
                {
                    var potrebnaSila = hracInput.KritickaSila - sila;
                    var pocetPechoty = ((potrebnaSila/2000000) + 1)*100000;
                    output.Pechota = pocetPechoty.ToString();
                    result = true;
                }
                else
                {
                    var potrebnaSila = hracInput.KritickaSila - sila;
                    var pocetPechoty = ((potrebnaSila / listDohodov[listDohodov.LastIndexOf(new DohadzovaciePocty(hrac,0))].SilaDohodu) + 1) * 100000;
                    output.Pechota = pocetPechoty.ToString();
                }
            }
            return output;
        }

    }
}
