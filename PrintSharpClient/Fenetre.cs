using System;
using System.Net.NetworkInformation;
using System.Text;
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
                InitialDirectory = Environment.GetEnvironmentVariable("USERPROFILE"),
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
                Log("En cours d'impression : " + pathToFile.Text);
                var file = new FileToSend(pathToFile.Text, 100);
                FileTransfert.SendFile(file.JsonData());
            }
            else
            {
                Log("Veuillez sélectionner un fichier avant de lancer l'impression");
            }
        }

        private void Log(String message)
        {
            inputLog.Text += message + Environment.NewLine;
            inputLog.SelectionStart = inputLog.Text.Length;
            inputLog.ScrollToCaret();
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPingClick(object sender, EventArgs e)
        {
            var pingSender = new Ping();
            var options = new PingOptions {DontFragment = true};
            byte[] buffer = Encoding.ASCII.GetBytes("wazaajaitrentedeuxbitsdansmonsac");
            const int timeout = 120;
            string ip = !string.IsNullOrEmpty(pathToFile.Text) ? pathToFile.Text : "8.8.8.8";
            try
            {
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);

                if (reply == null || reply.Status != IPStatus.Success) return;

                Log("Address: " + reply.Address);
                Log("RoundTrip time: " + reply.RoundtripTime);
                Log("Time to live: " + reply.Options.Ttl);
                Log("Don't fragment: " + reply.Options.DontFragment);
                Log("Buffer size: " + reply.Buffer.Length);
            }
            catch (PingException)
            {
                Log("Erreur : adresse ip incorrecte !");
            }
        }
    }
}