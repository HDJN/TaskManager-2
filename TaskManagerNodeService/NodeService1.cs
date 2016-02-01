using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Dal;
using TaskFramework.Taskmaneger;
using TaskFramework;
using ZF.Db;
using ZF.Log;
using System.Configuration;
using System.Threading;

namespace TaskManagerNodeService
{
    partial class NodeService1 : ServiceBase
    {
        private Thread thread;

        public NodeService1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogHelper.WriteInfo("服务启动");

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("NodeID") || !ConfigurationManager.AppSettings.AllKeys.Contains("TaskDataBaseConnectString"))
            {
                throw new Exception("请配置NodeID和TaskDataBaseConnectString");
            }
            GlobalConfig.NodeId = LibConvert.StrToInt64(System.Configuration.ConfigurationManager.AppSettings["NodeID"]);
            GlobalConfig.TaskDataBaseConnectString = System.Configuration.ConfigurationManager.AppSettings["TaskDataBaseConnectString"];

            thread = new Thread(new ThreadStart(NodeServiceProcessor.Run));
            TaskLogDal.Add(GlobalConfig.TaskDataBaseConnectString, new TaskFramework.Taskmaneger.Model.TaskLog() { LogMsg = "节点启动成功", NodeId = GlobalConfig.NodeId, TaskId = 0, CreateTime = DateTime.Now });
            thread.Start();
        }

        protected override void OnStop()
        {
            LogHelper.WriteInfo("服务结束");
            //TaskDal taskdal = new TaskDal();
            //CommandDal cmddal = new CommandDal();
            //cmddal.UpdateByTaskId(GlobalConfig.TaskDataBaseConnectString, GlobalConfig.NodeId, 0);
            //taskdal.UpdateStateById(GlobalConfig.TaskDataBaseConnectString, GlobalConfig.NodeId, 0);
            thread.Abort();
        }
    }
}
