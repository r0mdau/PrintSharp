using System.ComponentModel;
using System.Threading;
using System.Web.Services;
using WebserviceAbstract;

namespace PrinterWebservice
{
    [WebService(Namespace = "http://127.0.0.1/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Printer : PrinterWebserviceAbstract<Printer.PrinterInternal>
    {
        public class PrinterInternal : PrinterAbstract<PrinterInternal>
        {
            private static Job _printingJob = new Job(0, 0, DocumentState.Done);

            protected override void TraiterQueue()
            {
                while (true)
                {
                    if (Queue.Count > 0 && _printingJob.Status == DocumentState.Done)
                    {
                        lock (Verrou)
                        {
                            var leJob = Queue.Dequeue();
                            leJob.Status = "PRINTING 0";
                            _printingJob = leJob;
                        }
                        Printing();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                // ReSharper disable once FunctionNeverReturns
            }

            private static void Printing()
            {
                // ReSharper disable once PossibleLossOfFraction
                double secondes = _printingJob.Taille/(KiloOctetsPerSeconde * 1000);
                double centpourcent = secondes;
                while (secondes >= 0)
                {
                    int pourcentage = (int) ((1 - (secondes/centpourcent))*100);
                    _printingJob.Status = "PRINTING " + pourcentage;
                    PrintingJob.Add(_printingJob);
                    Thread.Sleep(1000);
                    secondes--;
                }
                _printingJob.Status = DocumentState.Done;
                PrintingJob.Remove(_printingJob);
                DoneJob.Add(_printingJob);
            }

            public override string Status(int jobId)
            {
                Job job;
                lock (Verrou)
                {
                    job = GetJob(jobId);
                }
                return GetStatus(job);
            }
        }
    }
}