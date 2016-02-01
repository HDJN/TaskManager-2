using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFramework.Taskmaneger.Model
{
    public class TaskLog
    {
        public long Id { get; set; }
        public string LogMsg { get; set; }
        public long TaskId { get; set; }
        public long NodeId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
