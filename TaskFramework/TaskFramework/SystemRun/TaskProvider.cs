using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Dal;
using TaskFramework.Taskmaneger.Model;
using ZF.Log;
using ZF.IOHelper;
using ZF.DeCompression;
using System.IO;
using System.Reflection;

namespace TaskFramework.SystemRun
{
    public class TaskProvider
    {
        /// <summary>
        /// 任务开启
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Start(long taskid)
        {
            LogHelper.WriteInfo("启动" + taskid + "任务");
            try
            {
                NodeTaskRuntimeInfo nodetask = TaskPool.Instance().Get(taskid.ToString());
                if (nodetask != null)
                {
                    LogHelper.WriteInfo("" + taskid + "任务已经开启");
                    return false;
                }
                TaskDal taskdal = new TaskDal();
                nodetask = new NodeTaskRuntimeInfo();
                nodetask.Tasklock = new TaskLock();
                nodetask.TaskModel = taskdal.GetById(GlobalConfig.TaskDataBaseConnectString, taskid.ToString());
                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConfig.TaskDLL, nodetask.TaskModel.Id.ToString());  //任务dll在节点service存放地址
                string srcpath = Path.Combine(nodetask.TaskModel.TaskClassPath, nodetask.TaskModel.TaskFileName);
                DeCompressionHelper.UnCompress(srcpath, filepath);

                AppDomainSetup setup = new AppDomainSetup();
                setup.ShadowCopyFiles = "true";
                setup.ApplicationBase = System.IO.Path.GetDirectoryName(filepath);

                var appdomain = AppDomain.CreateDomain(Path.Combine(filepath, nodetask.TaskModel.TaskClassNamespace), null, setup);
                BaseTaskDLL taskdll = (BaseTaskDLL)appdomain.CreateInstanceFromAndUnwrap(Path.Combine(filepath, nodetask.TaskModel.TaskFileName), nodetask.TaskModel.TaskClassNamespace);
                
                nodetask.TaskDLL = taskdll;

                TaskPool.Instance().Add(taskid.ToString(), nodetask);

                LogHelper.WriteInfo("节点开启成功");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                return false;
            }
        }

        public bool Stop(long taskid)
        {
            NodeTaskRuntimeInfo nodetask = TaskPool.Instance().Get(taskid.ToString());
            if (nodetask == null)
            {
                LogHelper.WriteInfo("" + taskid + "任务没有在运行");
                return false;
            }
            if (Dispose(taskid, nodetask))
            {
                try
                {
                    TaskDal taskdal = new TaskDal();
                    TaskModel model = taskdal.GetById(GlobalConfig.TaskDataBaseConnectString, taskid.ToString());
                    model.TaskState = 0;
                    model.TaskStopTime = DateTime.Now;
                    taskdal.EditTask(GlobalConfig.TaskDataBaseConnectString, model);
                    return true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex);
                    return false;
                }
            }
            return false;
        }

        public bool Dispose(long taskid, NodeTaskRuntimeInfo nodetask)
        {
            if (nodetask != null && nodetask.Domain != null)
            {
                try
                {
                    TaskPool.Instance().Remove(taskid.ToString());
                    AppDomain.Unload(nodetask.Domain);
                    return true;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex);
                    return false;
                }
            }
            return false;
        }
    }
}
