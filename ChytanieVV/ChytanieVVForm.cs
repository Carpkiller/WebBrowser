using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WebBrowser.ChytanieVV
{
    public partial class ChytanieVVForm : Form
    {
        private Jadro _jadro;
        private List<Vyvrhel> listVyvrhelov; 

        public ChytanieVVForm(Jadro jadro)
        {
            _jadro = jadro;
            InitializeComponent();

            webBrowser1.Url = new Uri("http://www.stargate-game.cz/vesmir.php?page=1&id_rasa=11");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            listVyvrhelov = _jadro.ParsujVyvrhelov(webBrowser1.Document.Window.Document.Body.InnerHtml);
            dataGridView1.DataSource = listVyvrhelov;
        }


    }
}
