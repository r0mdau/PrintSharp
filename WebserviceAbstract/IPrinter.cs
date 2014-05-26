namespace WebserviceAbstract
{
    public interface IPrinter
    {
        string Status(int jobId);
        int Print(int taille, string nom, int copies);
        bool Ping();
    }
}