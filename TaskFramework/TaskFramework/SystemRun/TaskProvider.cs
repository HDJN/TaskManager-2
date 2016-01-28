using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Dal;
using TaskFramework.Taskmaneger.Model;
using ZF.Log;
using ZF.IOHelper;
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
            NodeTaskRuntimeInfo nodetask = TaskPool.Instance().Get(taskid.ToString());
            if (nodetask != null)
            { 
                LogHelper.WriteInfo(""+taskid+"任务已经开启");
                return false;
            }
            TaskDal taskdal = new TaskDal();
            nodetask.TaskModel = taskdal.GetById(GlobalConfig.TaskDataBaseConnectString, taskid.ToString());
            try
            {
                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConfig.TaskDLL, nodetask.TaskModel.Id.ToString());  //任务dll在节点service存放地址
                string taskpath = Path.Combine(nodetask.TaskModel.TaskClassPath);   //任务dll上传所在地址

                IOHelper.CopyDirectory(taskpath, filepath);  //复制

                var appdomain = AppDomain.CreateDomain(Path.Combine(filepath, nodetask.TaskModel.TaskClassNamespace));
                BaseTaskDLL taskdll = (BaseTaskDLL)appdomain.CreateInstanceFromAndUnwrap(Path.Combine(filepath, nodetask.TaskModel.TaskClassNamespace), nodetask.TaskModel.TaskClassNamespace);

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
    }
}
