using System.Windows.Forms;

namespace WebBrowser.Export
{
    public partial class ExportForm : Form
    {
        public ExportForm(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
        }
    }
}
