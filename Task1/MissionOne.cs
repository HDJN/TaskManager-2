﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFramework;
using ZF.IOHelper;
using ZF.Log;

namespace Task1
{
    public class MissionOne : BaseTaskDLL
    {
        public override void Run()
        {
            LogHelper.WriteDebug("MissionOne运行一次");
        }
    }
}
