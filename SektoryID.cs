using System.Collections.Generic;

namespace WebBrowser
{
    public class SektoryID
    {
        public string Sektor { get; set; }
        public string Id { get; set; }

        public SektoryID(string sektor, string id)
        {
            Sektor = sektor;
            Id = id;
        }

        public static Dictionary<int, string> ListId = new Dictionary<int, string>
        {
            {0,"Vsetko"},
            {1,"Odpad"},
            {151,"Antik"},
            {96,"Aschen"},
            {167,"Asgard"},
            {130,"Atanik"},
            {44,"Bedrosian"},
            {143,"Euronďan"},
            {113,"Furling"},
            {176,"Goauld"},
            {87,"Jaffa"},
            {17,"Kelowňan"},
            {198,"Lucián"},

            {148,"Nox"},
            {60,"Oanessan"},
            {66,"Ori"},
            {196,"Orion"},
            {15,"Reetou"},
            {49,"Replik"},
            {20,"Tagreřan"},
            {116,"Tauri"},
            {197,"Tokra"},
            {77,"Unas"},
            {200,"Tollán"}
        }; 
    }
}
