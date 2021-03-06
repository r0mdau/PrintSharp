using System;
using System.Linq;
using WebserviceAbstract;
using System.Collections.Generic;
using System.Threading;

namespace PrintSharpPrinter
{
    internal static class PrinterState
    {
        public const string Offline = "Offline";
        public const string Online = "Online";
    }

    internal static class DocumentState
    {
        public const string Waiting = "WAITING";
        public const string Done = "DONE";
        public const string Notfound = "NOTFOUND";
    }

    public class Printer : IPrinter
    {
        private const int KiloOctetsPerSeconde = 100;
        private static String _state = PrinterState.Offline;

        private static int _jobs;
        static readonly object Verrou = new object();

        private static readonly Queue<Job> Queue = new Queue<Job>();
        private static Job _printingJob = new Job(0, 0, DocumentState.Done);
        private static readonly List<Job> DoneJob = new List<Job>();

        private struct Job
        {
            public readonly int JobId;
            public readonly int Taille;
            public string Status;

            public Job(int id, int tail, string sts)
            {
                JobId = id;
                Taille = tail;
                Status = sts;
            }
        }

        public Printer()
        {
            _state = PrinterState.Online;
            var thPrinting = new Thread(LancerImpression);
            thPrinting.Start();
            thPrinting.IsBackground = true;
        }

        public string Status(int jobId)
        {   
            Job job;
            lock (Verrou)
            {
                job = GetJob(jobId);
            }
            return GetStatus(job);
        }

        public int Print(int taille, string nom, int copies = 1)
        {
            lock (Verrou)
            {
                var leJob = new Job(++_jobs, taille, DocumentState.Waiting);
                Queue.Enqueue(leJob);
            }
            return _jobs;
        }

        public bool Ping()
        {
            return _state != PrinterState.Offline;
        }

        private static string GetStatus(Job leJob)
        {
            return leJob.Status;
        }

        private static Job GetJob(int id)
        {
            var leJob = new Job(0, 0, DocumentState.Notfound);            
            foreach (var tmpJob in Queue.Where(tmpJob => tmpJob.JobId == id))
            {
                leJob = tmpJob;
            }
            foreach (var tmpJob in DoneJob.Where(tmpJob => tmpJob.JobId == id))
            {
                leJob = tmpJob;
            }
            if (_printingJob.JobId == id)
                leJob = _printingJob;
            return leJob;
        }

        private static void LancerImpression()
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
            double secondes = _printingJob.Taille / KiloOctetsPerSeconde;
            var centpourcent = secondes;
            while (secondes >= 0)
            {
                var pourcentage = (int) ((1 - (secondes / centpourcent)) * 100);
                _printingJob.Status = "PRINTING " + pourcentage;
                Thread.Sleep(1000);
                secondes--;
            }
            _printingJob.Status = DocumentState.Done;
            DoneJob.Add(_printingJob);
        }
    }
}
