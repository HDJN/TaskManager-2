using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.RabbitMQ;
using ZF.Db;
using ZF.Log;
using TaskFramework.Taskmaneger.Model;
using TaskFramework.Taskmaneger.Dal;
using TaskFramework.SystemRun;

namespace TaskFramework
{
    /// <summary>
    /// 节点服务处理器
    /// </summary>
    public class NodeServiceProcessor
    {
        static NodeServiceProcessor()
        {
            ReceiveCommandLoop();
        }

        public static void Run()
        { 
        
        }
        /// <summary>
        /// 接收命令处理
        /// </summary>
        private static void ReceiveCommandLoop()
        {
            RabbitMQModel model = new RabbitMQModel();
            model.HostName = "localhost";
            model.UserName = "guest";
            model.Password = "guest";
            model.QueueName = "NodeService";
            RabbitMQHelper.Receive(model, (c) =>
            {
                if (!string.IsNullOrEmpty(c))
                {
                    Command cmd = LibJsonConvert.DeserializeObject<Command>(c);
                    CommandFactory.Execute(cmd);
                }
            });
        }
    }
}
