using System;
using System.Windows.Forms;

namespace PrintSharpClient
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void BtnSelectFileClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathToFile.Text = dialog.FileName;
            }
        }

        private void BtnPrintClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pathToFile.Text))
            {
            }
            else
            {
                Log("Veuillez sélectionner un fichier avant de lancer l'impression");
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPingClick(object sender, EventArgs e)
        {
        }

        private void Log(String message)
        {
            inputLog.Text += message + Environment.NewLine;
            inputLog.SelectionStart = inputLog.Text.Length;
            inputLog.ScrollToCaret();
        }
    }
}