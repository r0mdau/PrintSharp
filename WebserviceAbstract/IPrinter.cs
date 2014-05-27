namespace WebserviceAbstract
{
    public static class PrinterState
    {
        public const string Offline = "Offline";
        public const string Online = "Online";
    }

    public static class DocumentState
    {
        public const string Waiting = "WAITING";
        public const string Done = "DONE";
        public const string Notfound = "NOTFOUND";
    }
    public interface IPrinter
    {
        string Status(int jobId);
        int Print(int taille, string nom, int copies);
        bool Ping();
    }
}