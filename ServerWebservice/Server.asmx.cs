﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Services;
using WebserviceAbstract;

namespace ServerWebservice
{
    [WebService(Namespace = "http://127.0.0.1/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Server : PrinterWebserviceAbstract<Server.ServerInternal>
    {
        public class ServerInternal : PrinterAbstract<ServerInternal>
        {
            private static readonly Dictionary<Client, int> JobQueues = new Dictionary<Client, int>();
            private static readonly ConcurrentDictionary<int, KeyValuePair<Client, int>> Bindings = new ConcurrentDictionary<int, KeyValuePair<Client, int>>();

            static ServerInternal()
            {
                JobQueues.Add(new Client(@"http://localhost:40128/Printer.asmx"), 0);
                JobQueues.Add(new Client(@"http://localhost:40138/Printer2.asmx"), 0);
            }

            private static readonly object LogLock = new object();
            
            private static readonly string LogDir =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Log");
            private static readonly string LogPath =
                Path.Combine(LogDir, DateTime.Now.ToString("yymmddhhmmss") + ".log");
            
            private static void Log(string message)
            {
                lock (LogLock)
                {
                    if(!Directory.Exists(LogDir))
                        Directory.CreateDirectory(LogDir);
                    File.WriteAllText(LogPath, File.Exists(LogPath) ? File.ReadAllText(LogPath) : "" + Environment.NewLine + message);
                }
            }

            public override string Status(int jobId)
            {
                if (!Bindings.ContainsKey(jobId)) return DocumentState.Notfound;
                var status = Bindings[jobId].Key.Status(Bindings[jobId].Value);
                return status == DocumentState.Notfound ? DocumentState.Waiting : status;
            }

            protected override void TraiterQueue()
            {
                while (true)
                {
                    if (Queue.Count > 0)
                    {
                        var job = Queue.Dequeue();
                        PrintingJob.Add(job);
                        Log("Impression du fichier numéro : " + job.JobId);
                        
                        var leastBusy = LeastBusy();
                        JobQueues[leastBusy] += job.Taille;

                        var printingOrder = new KeyValuePair<Client, int>(leastBusy, leastBusy.Print(job.Taille));
                        Bindings.TryAdd(job.JobId, printingOrder);
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
                // ReSharper disable once FunctionNeverReturns
            }

            private static Client LeastBusy()
            {
                var leastBusy = JobQueues.Keys.First();
                int[] lesserCharge = {int.MaxValue};

                foreach (var queue in JobQueues.Where(queue => queue.Value < lesserCharge[0]))
                {
                    lesserCharge[0] = queue.Value;
                    leastBusy = queue.Key;
                }
                Log("Imprimante la moins chargée : " + leastBusy);
                return leastBusy;
            }
        }
    }
}