using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Model;

namespace TaskFramework.SystemRun
{
    /// <summary>
    /// 任务运行时信息
    /// </summary>
    public class NodeTaskRuntimeInfo
    {
        /// <summary>
        /// 任务所在的应用程序域
        /// </summary>
        public AppDomain Domain;
        /// <summary>
        /// 任务信息
        /// </summary>
        public TaskModel TaskModel;
        /// <summary>
        /// 任务当前版本信息
        /// </summary>
        //public tb_version_model TaskVersionModel;
        /// <summary>
        /// 应用程序域中任务dll实例引用
        /// </summary>
        public BaseTaskDLL TaskDLL;

        public TaskLock Tasklock;
        
    }
}
