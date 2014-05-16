using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintSharpPrinter
{
    static class PrinterState
    {
        public const string OFFLINE = "Offline";
        public const string ONLINE = "Online";
        public const string ERROR = "On error";
    }
    static class Printer
    {
        private static String name = "SPrint1";
        // one page = 100Ko
        private static int printSpeedPerMinute = 100;
        private static String state = PrinterState.OFFLINE;
    }
}
