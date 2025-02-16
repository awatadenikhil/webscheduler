using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public class SchedulerModel
    {
        public int SchedulerId { get; set; }

        public string AssemblyName { get; set; }
        public string NameSpace { get; set; }
        public string ClassName { get; set; }
        public int MinuteInterval { get; set; }
        public DateTime? LastExecution { get; set; }
        public bool IsActive { get; set; }
    }
}
