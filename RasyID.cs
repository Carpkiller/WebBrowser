using System.Collections.Generic;

namespace WebBrowser
{
    public class RasyId
    {
        public string Nazov { get; set; }
        public string Id { get; set; }

        public RasyId(string nazov, string id)
        {
            Nazov = nazov;
            Id = id;
        }

        public static readonly Dictionary<string, string> ListId = new Dictionary<string, string>
        {
            {"Goauldi","4"},
            {"Jaffové","12"},
            {"Tau´ri","2"},
            //{"Tagreřané","25"},
            {"Asgardi","1"},
            //{"Ori","14"},
            //{"Euronďané","32"},
            {"Reetou","19"},
            {"Tokrové","7"},
            //{"Atanikové","18"},
            {"Replikátoři","9"},
            {"Luciáni","21"},

            {"Unasové","30"},
            {"Kelowňané","24"},
            //{"Oanessané","3"},
            {"Noxové","15"},
            {"Antikové","8"},
            {"Tolláni","6"},
            //{"Bedrosiani","20"},
            {"Ascheni","5"},
            //{"Furlingové","23"},

            {"Vyvrhel","11"}
        }; 
    }
}
