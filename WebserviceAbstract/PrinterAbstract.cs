﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WebserviceAbstract
{
    public static class DocumentState
    {
        public const string Waiting = "WAITING";
        public const string Done = "DONE";
        public const string Notfound = "NOTFOUND";
    }
    public abstract class PrinterAbstract<T> where T : PrinterAbstract<T>, new()
    {
        protected const int KiloOctetsPerSeconde = 100;
        
        private static int _jobs;
        private readonly static T Instance;

        protected static readonly object Verrou = new object();

        protected static readonly Queue<Job> Queue = new Queue<Job>();
        protected static readonly List<Job> DoneJob = new List<Job>();
        protected static readonly List<Job> PrintingJob = new List<Job>();

        static PrinterAbstract()
        {
            Instance = new T();
        }

        protected struct Job
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

        protected PrinterAbstract()
        {
            var thPrinting = new Thread(TraiterQueue);
            thPrinting.Start();
            thPrinting.IsBackground = true;
        }

        private static string GetStatus(Job leJob)
        {
            return leJob.Status;
        }

        public static string Status(int jobId)
        {
            Job job;
            lock (Verrou)
            {
                job = GetJob(jobId);
            }
            return GetStatus(job);
        }

        protected abstract void TraiterQueue();

        protected static Job GetJob(int jobId)
        {
            var leJob = new Job(0, 0, DocumentState.Notfound);
            foreach (var tmpJob in Queue.Where(tmpJob => tmpJob.JobId == jobId))
            {
                leJob = tmpJob;
            }
            foreach (var tmpJob in DoneJob.Where(tmpJob => tmpJob.JobId == jobId))
            {
                leJob = tmpJob;
            }
            foreach (var tmpJob in PrintingJob.Where(tmpJob => tmpJob.JobId == jobId))
            {
                leJob = tmpJob;
            }
            return leJob;
        }

        public static int Print(int taille, string nom, int copies)
        {
            lock (Verrou)
            {
                var leJob = new Job(++_jobs, taille * copies, DocumentState.Waiting);
                Queue.Enqueue(leJob);
                return _jobs;
            }
        }
    }
}