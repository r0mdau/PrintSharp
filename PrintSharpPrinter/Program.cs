using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PrintSharpPrinter
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread leThread = new Thread(startServer);
            leThread.Start();
            leThread.IsBackground = true;
            Application.Run(new PrinterWindow());

        }
        static void startServer()
        {
            FileTransfert filetransfert = new FileTransfert();
            filetransfert.StartServer();
        }
    }
}
