using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFramework
{
    public class GlobalConfig
    {
        public static string TaskDataBaseConnectString { get; set; }

        /// <summary>
        /// 节点服务Id
        /// </summary>
        public static long NodeId { get; set; }
        /// <summary>
        /// 任务存放目录
        /// </summary>
        public static string TaskDLL { get { return "taskdll"; } }
    }
}
