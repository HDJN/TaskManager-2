using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZF.Log;
using ZF.RabbitMQ;

namespace TaskManagerNodeService
{
    public class Common
    {
        public static void Run()
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
                    LogHelper.WriteInfo(c);
                }
            });
        }
    }
}
