using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WebserviceAbstract;

namespace PrintSharpClient
{
    public partial class MainView : Form
    {
        private const string ServerUri = @"http://localhost:40129/Server.asmx";
        private readonly Client _client = new Client(ServerUri);

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

        private void CheckStatus(int jobId)
        {
            var status = DocumentState.Waiting;

            while (status != DocumentState.Done)
            {
                status = _client.Status(jobId);
                Log(string.Format("Job {0} : {1}", jobId, status));
                Thread.Sleep(1000);
            }
        }

        private void BtnPrintClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pathToFile.Text))
            {
                var jobId = _client.Print(File.ReadAllText(pathToFile.Text).Length, pathToFile.Text, 1);

                var thread = new Thread(() => CheckStatus(jobId)) {IsBackground = true};
                thread.Start();

                Log("Impression démarrée");
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
            if (_client.Ping()) Log("Ping réussi vers " + ServerUri);
            else Log("Ping échoué vers " + ServerUri);
        }

        private static readonly object Lock = new object();

        private void Log(string message)
        {
            if (InvokeRequired)
            {
                lock (Lock)
                {
                    Invoke(new Action<string>(Log), new object[] {message});
                    return;
                }
            }
            inputLog.Text += message + Environment.NewLine;
            inputLog.ScrollToCaret();
        }
    }
}