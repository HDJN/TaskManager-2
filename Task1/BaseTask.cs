using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.IOHelper;
using ZF.Log;

namespace Task1
{
    public abstract class BaseTask : MarshalByRefObject
    {
        public void StartRun()
        {
            Run();
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
            IOHelper.Write("E:\\test.txt", "运行一次");
        }
    }
}
