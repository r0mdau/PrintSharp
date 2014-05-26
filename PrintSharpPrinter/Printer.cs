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

    public class Printer : IPrinter
    {
        private static String name = "SPrint1";
        private static int printSpeedPerMinute = 100; // one page = 100Ko
        private static String state = PrinterState.OFFLINE;

        private static int jobs = 0;
        static readonly object verrou = new object();

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

        private static readonly Queue<Job> queue = new Queue<Job>();

        public Printer()
        {
            state = PrinterState.ONLINE;
        }

        public string Status(int jobId)
        {
            return this.getStatus(this.getJob(jobId));
        }

        public int Print(int taille, string nom, int copies = 1)
        {
            lock (verrou)
            {
                Job leJob = new Job(++jobs, taille, "WAITING", nom);
                queue.Enqueue(leJob);
            }
            Thread thread1 = new Thread(() => lancerImpression(queue));
            thread1.Start();
            thread1.IsBackground = true;
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
            Job leJob = new Job(0, 0, "NOTFOUND", "NOTFOUND");
            lock (verrou)
            {
                foreach (Job tmp_job in queue)
                {
                    if (tmp_job.jobId == id)
                        leJob = tmp_job;
                }
            }
            return leJob;
        }

        private void lancerImpression(Queue<Job> queue)
        {            
            Job leJob;
            lock (verrou)
            {
               leJob = queue.Dequeue();
               leJob.status = "PRINTING 0";
            }
            this.threadSleeping(leJob);
        }

        private void threadSleeping(Job leJob)
        {
            int secondes = leJob.taille / printSpeedPerMinute;
            while (secondes > 0)
            {
                leJob.status = "PRINTING " + (1 - (secondes * 10));
                Thread.Sleep(1000);
                secondes--;
            }
        }
    }
}