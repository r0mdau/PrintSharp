using System;
using WebserviceAbstract;
using System.Collections.Generic;
using System.Threading;

namespace PrintSharpPrinter
{
    internal static class PrinterState
    {
        public const string OFFLINE = "Offline";
        public const string ONLINE = "Online";
        public const string ERROR = "On error";
    }

    internal static class DocumentState
    {
        public const string WAITING = "WAITING";
        public const string DONE = "DONE";
        public const string NOTFOUND = "NOTFOUND";
    }

    public class Printer : IPrinter
    {
        private static String name = "SPrint1";
        private static int printSpeedPerMinute = 100; // one page = 100Ko
        private static String state = PrinterState.OFFLINE;

        private static int jobs = 0;
        static readonly object verrou = new object();

        private static readonly Queue<Job> queue = new Queue<Job>();
        private static Job printingJob = new Job(0, 0, DocumentState.DONE, DocumentState.DONE);

        public struct Job
        {
            public int jobId; 
            public int taille;
            public string status;
            public string name;

            public Job(int id, int tail, string sts, string nam)
            {
                jobId = id;
                taille = tail;
                status = sts;
                name = nam;
            }
        }

        public Printer()
        {
            state = PrinterState.ONLINE;
            Thread thread1 = new Thread(lancerImpression);
            thread1.Start();
            thread1.IsBackground = true;
        }

        public string Status(int jobId)
        {   
            Job job;
            lock (verrou)
            {
                job = this.getJob(jobId);
            }
            return this.getStatus(job);
        }

        public int Print(int taille, string nom, int copies = 1)
        {
            lock (verrou)
            {
                Job leJob = new Job(++jobs, taille, DocumentState.WAITING, nom);
                queue.Enqueue(leJob);
            }
            return jobs;
        }

        public bool Ping()
        {
            return state != PrinterState.OFFLINE;
        }

        private string getStatus(Job leJob)
        {
            return leJob.status;
        }

        private Job getJob(int id)
        {
            Job leJob = new Job(0, 0, DocumentState.NOTFOUND, DocumentState.NOTFOUND);            
            foreach (Job tmp_job in queue)
            {
                if (tmp_job.jobId == id)
                    leJob = tmp_job;
            }
            if (printingJob.jobId == id)
                leJob = printingJob;
            return leJob;
        }

        private void lancerImpression()
        {
            Job leJob;
            while (true)
            {
                if (queue.Count > 0 && printingJob.status == DocumentState.DONE)
                {
                    lock (verrou)
                    {
                        leJob = queue.Dequeue();
                        leJob.status = "PRINTING 0";
                        printingJob = leJob;
                    }
                    this.printing();
                }
                else
                {
                    Thread.Sleep(2000);
                    continue;
                }
            }
        }

        private void printing()
        {
            double secondes = printingJob.taille / printSpeedPerMinute;
            double centpourcent = secondes;
            while (secondes >= 0)
            {
                int pourcentage = (int) ((1 - (secondes / centpourcent)) * 100);
                printingJob.status = "PRINTING " + pourcentage;
                Thread.Sleep(1000);
                secondes--;
            }
            printingJob.status = DocumentState.DONE;      
        }
    }
}
