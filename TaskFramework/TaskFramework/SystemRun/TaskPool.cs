using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace TaskFramework.SystemRun
{
    /// <summary>
    /// 任务池  全局唯一实例
    /// </summary>
    public class TaskPool : IDisposable
    {
        //全局任务运行池
        private static Dictionary<string, NodeTaskRuntimeInfo> TaskRuntimePool = new Dictionary<string, NodeTaskRuntimeInfo>();

        private static TaskPool _taskpool;  //唯一实例

        private static object lock_task = new object();

        //Quarzt作业计划
        private static IScheduler _ische;

        static TaskPool()
        {
            _taskpool = new TaskPool();
            ISchedulerFactory sf = new StdSchedulerFactory();
            _ische = sf.GetScheduler();
            _ische.Start();
        }

        /// <summary>
        /// 任务池唯一实例
        /// </summary>
        /// <returns></returns>
        public static TaskPool Instance()
        {
            return _taskpool;
        }

        public bool Add(string taskid, NodeTaskRuntimeInfo nodetask)
        {
            lock (lock_task)
            {
                if (!TaskRuntimePool.ContainsKey(taskid))
                {
                    JobDetail job1 = new JobDetail(taskid, nodetask.TaskModel.CategoryId.ToString(), typeof(TaskJob));
                    Trigger triger1 = TriggerFactory.Create(nodetask);

                    _ische.ScheduleJob(job1, triger1);
                    TaskRuntimePool.Add(taskid, nodetask);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Remove(string taskid)
        {
            lock (lock_task)
            {
                if (TaskRuntimePool.ContainsKey(taskid))
                {
                    NodeTaskRuntimeInfo nodetask = TaskRuntimePool[taskid];
                    _ische.PauseTrigger(nodetask.TaskModel.Id.ToString(), nodetask.TaskModel.CategoryId.ToString());// 停止触发器  
                    _ische.UnscheduleJob(nodetask.TaskModel.Id.ToString(), nodetask.TaskModel.CategoryId.ToString());// 移除触发器  
                    _ische.DeleteJob(nodetask.TaskModel.Id.ToString(), nodetask.TaskModel.CategoryId.ToString());// 删除任务

                    TaskRuntimePool.Remove(taskid);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public NodeTaskRuntimeInfo Get(string key)
        {
            if (TaskRuntimePool.ContainsKey(key))
            {
                lock (lock_task)
                {
                    if (TaskRuntimePool.ContainsKey(key))
                    {
                        return TaskRuntimePool[key];
                    }
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<NodeTaskRuntimeInfo> List()
        {
            lock (lock_task)
            {
                return TaskRuntimePool.Values.ToList();
            }
        }

        public void Dispose()
        {
            if (_ische != null && !_ische.IsShutdown)
            {
                _ische.Shutdown();
            }
        }
    }
}
