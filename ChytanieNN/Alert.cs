using System.Windows.Forms;

namespace WebBrowser.ChytanieNN
{
    public partial class Alert : Form
    {
        public Alert(string text, HtmlElement htmlItem)
        {
            InitializeComponent();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
         //   webBrowser1.Document = new HtmlDocument();
           // webBrowser1.Document.Body.AppendChild(htmlItem);

            //var desktopWorkingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            //this.Left = desktopWorkingArea.Width - this.Width;
            //this.Top = desktopWorkingArea.Height - this.Height;

        }
    }
}
