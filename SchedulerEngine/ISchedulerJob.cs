using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public interface ISchedulerJob
    {
        bool Start();
        bool Stop();
        SchedulerModel Information { get; }
    }
}
