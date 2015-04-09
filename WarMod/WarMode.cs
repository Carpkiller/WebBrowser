using System;
using System.Windows.Forms;

namespace WebBrowser.WarMod
{
    public partial class WarMode : Form
    {
        public WarMode()
        {
            InitializeComponent();
        }

        public int RefreshovaciCas { get; set; }
        public int MnozstvoNaqu { get; set; }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            RefreshovaciCas = int.Parse(textBoxCas.Text);
            MnozstvoNaqu = int.Parse(textBoxMnozstvoNaqu.Text);
            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }
    }
}
