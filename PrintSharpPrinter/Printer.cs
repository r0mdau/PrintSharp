using System;
using WebserviceAbstract;

namespace PrintSharpPrinter
{
    internal static class PrinterState
    {
        public const string OFFLINE = "Offline";
        public const string ONLINE = "Online";
        public const string ERROR = "On error";
    }

    public class Printer : IPrinter
    {
        private static String name = "SPrint1";
        // one page = 100Ko
        private static int printSpeedPerMinute = 100;
        private static String state = PrinterState.OFFLINE;

        private static readonly Printer _instance;

        static Printer()
        {
            _instance = new Printer();
        }

        public static Printer Instance()
        {
            return _instance;
        }

        public string Status(int jobId)
        {
            throw new NotImplementedException();
        }

        public int Print(int taille, string nom, int copies)
        {
            throw new NotImplementedException();
        }

        public bool Ping()
        {
            throw new NotImplementedException();
        }
    }
}