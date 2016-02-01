using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskFramework.Taskmaneger.Model;
using ZF.Log;

namespace TaskFramework.SystemRun
{
    public class CommandFactory
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandInfo"></param>
        public static void Execute(Command commandInfo)
        {
            LogHelper.WriteInfo("Execute" + commandInfo.CommandType.ToString());
            switch (commandInfo.CommandType)
            {
                case CommandType.Start: StartTask(commandInfo); break;
                case CommandType.Stop: StopTask(commandInfo); break;
                default: LogHelper.WriteInfo("" + commandInfo.Id + "未识别的命令"); break;
            }
        }

        private static void StartTask(Command cmd)
        {
            LogHelper.WriteInfo("开始启动任务");
            TaskProvider tp = new TaskProvider();
            tp.Start(cmd.TaskId);
        }

        private static void StopTask(Command cmd)
        {
            TaskProvider tp = new TaskProvider();
            tp.Stop(cmd.TaskId);
        }
    }
}
