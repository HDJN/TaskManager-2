using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Log;

namespace Task1
{
    public abstract class BaseTask : MarshalByRefObject
    {
        public void StartRun()
        {
            Run();
            LogHelper.WriteInfo("BaseTaskDLL StartRun");
        }
        public abstract void Run();
        public void Dispose()
        {

        }
    }
    public class Task1 : BaseTask
    {

        public override void Run()
        {
            LogHelper.WriteInfo("运行一次");
        }
    }
}
