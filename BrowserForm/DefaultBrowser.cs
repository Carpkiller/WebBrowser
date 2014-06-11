using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WebBrowser.BrowserForm
{
    public partial class DefaultBrowser : Form
    {
        public DefaultBrowser(string url, string planeta)
        {
            InitializeComponent();
            webBrowser1.Url = new Uri(url);
            webBrowser1.Refresh();

            while (true)
            {
                Application.DoEvents();
                if (!string.IsNullOrEmpty(webBrowser1.StatusText) && !webBrowser1.StatusText.Contains("utok.php?"))
                    break;
            }

            var text = webBrowser1.Document.Body.InnerHtml;
            var id = parsujId(text, planeta);
            webBrowser1.Document.GetElementsByTagName("select").GetElementsByName("naco")[0].SetAttribute("value", id);
        }

        private string parsujId(string InputHtml, string hladanaPlaneta)
        {
            var zoznam = new Dictionary<string, string>();

            var text = InputHtml.Substring(InputHtml.IndexOf("<OPTION"));

            while (true)
            {
                if (text.IndexOf("<OPTION") == -1)
                    break;
                
                text = text.Remove(0, text.IndexOf("value="));
                var index = text.IndexOf(">");
                var id = text.Substring(6, index-6);

                var index2 = text.IndexOf("<OPTION");
                if (index2 == -1)
                    index2 = text.IndexOf("</OPTION");

                var meno = text.Substring(index + 1, index2-index-1);

                if (text.IndexOf("<OPTION") > 0)
                    text = text.Remove(0, text.IndexOf("<OPTION"));

                zoznam.Add(meno, id);
            }
        
            string hladID;
            if (zoznam.TryGetValue(hladanaPlaneta, out hladID))
                return hladID;

            return null;
        }
    }
}