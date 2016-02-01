using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Log;

namespace TaskFramework
{
    public abstract class BaseTaskDLL : MarshalByRefObject, IDisposable
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
}
