using System;
using System.Threading;
using System.Windows.Forms;

namespace PrintSharpPrinter
{
    internal static class Program
    {
        /// <summary>
        ///     Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var leThread = new Thread(startServer);
            leThread.Start();
            leThread.IsBackground = true;
            Application.Run(new PrinterWindow());
        }

        private static void startServer()
        {
            var filetransfert = new FileTransfert();
            filetransfert.StartServer();
        }
    }
}