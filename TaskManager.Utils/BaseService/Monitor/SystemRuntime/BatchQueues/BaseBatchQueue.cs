using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TaskManager.Utils.Common;
using TaskManager.Utils.Db;using TaskManager.Utils.Common;


namespace TaskManager.Utils.BaseService.Monitor.SystemRuntime.BatchQueues
{
    public abstract class BaseBatchQueue<T>
    {
        protected List<T> Queue = new List<T>();
        protected List<T> TempQueue = new List<T>();
        private Thread thread;
        public virtual int MaxQueueCount { get; set; }
        public virtual int SleepTime { get; set; }
        public virtual int MaxSleepTime { get; set; }
        public virtual string BatchTable { get; set; }

        protected int _sleeptimecount = 0;
        protected object _lock = new object();

        public BaseBatchQueue()
        {
            thread = new Thread(Running);
            thread.IsBackground = true;
            thread.Start();
        }

        public virtual void Add(T t)
        {
            if (t != null)
            {
                lock (_lock) //后续采用自旋等算法优化,减少锁
                    Queue.Add(t);
            }
        }

        protected virtual void Running()
        {
            while (true)
            {
                try
                {
                    if (Queue.Count >= MaxQueueCount || MaxSleepTime < _sleeptimecount * SleepTime)
                    {
                        lock (_lock)
                        {
                            TempQueue = Queue.ToList();
                            Queue.Clear();
                            _sleeptimecount = 0;
                        }
                        if (TempQueue.Count > 0)
                        {

                            try
                            {
                                BatchCommit();
                            }
                            catch
                            { }
                            TempQueue.Clear();
                        }
                    }
                }
                catch
                {
                    TempQueue.Clear();//避免内存溢出
                    _sleeptimecount = 0;
                }

                if (SleepTime < 1000 * 60)
                    SleepTime = 1000 * 60;

                System.Threading.Thread.Sleep(SleepTime);

                _sleeptimecount++;
            }
        }

        protected virtual void BatchCommit()
        {
            if (!string.IsNullOrEmpty(TaskManager.Utils.Common.ConfigHelper.TimeWatchConnectionString))
            {
                var timewatchinfoTable = DataTableHelper.ConvertToDataTable<T>(TempQueue);
                using (var c = Db.DbConfig.CreateConn(Db.DbType.SQLSERVER, TaskManager.Utils.Common.ConfigHelper.TimeWatchConnectionString))
                {
                    c.Open();
                    c.BeginTransaction();
                    try
                    {
                        c.SqlBulkCopy(timewatchinfoTable, BatchTable + DateTime.Now.ToString("yyyyMMdd"), "", new List<ProcedureParameter>(), 0);
                        c.Commit();
                    }
                    catch (Exception exp)
                    {
                        c.Rollback();
                    }
                }
            }
        }
    }
}
