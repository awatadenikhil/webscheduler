using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public interface ISchedulerDataService
    {
        List<SchedulerModel> GetAll();
        SchedulerModel? Get(int schedulerId);
        SchedulerModel Add(SchedulerModel schedulerInfo);
        bool Update(SchedulerModel schedulerInfo);
        bool Remove(int schedulerId);
    }
}
