using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public class SchedulerDataService : ISchedulerDataService
    {
        private List<SchedulerModel> _scheduleModel = new List<SchedulerModel>();

        public SchedulerDataService() {
            _scheduleModel.Add(new SchedulerModel() { AssemblyName= "JobTest1", NameSpace= "JobTest1", ClassName= "Class1", MinuteInterval=1});
        }

        public SchedulerModel Add(SchedulerModel schedulerInfo)
        {
            _scheduleModel.Add(schedulerInfo);
            return schedulerInfo;
        }

        public SchedulerModel? Get(int schedulerId)
        {
           return _scheduleModel.FirstOrDefault(m => m.SchedulerId == schedulerId);
        }

        public List<SchedulerModel> GetAll()
        {
            return _scheduleModel;
        }

        public bool Remove(int schedulerId)
        {
            var scheduler = _scheduleModel.FirstOrDefault(m => m.SchedulerId == schedulerId);
            if (scheduler != null)
            {
                _scheduleModel.Remove(scheduler);
                return true;
            }
            return false;
        }

        public bool Update(SchedulerModel schedulerInfo)
        {
            var scheduler = _scheduleModel.FirstOrDefault(m => m.SchedulerId == schedulerInfo.SchedulerId);
            if (scheduler != null)
            {
                scheduler.NameSpace = schedulerInfo.NameSpace;
                scheduler.ClassName = schedulerInfo.ClassName;
                scheduler.AssemblyName = schedulerInfo.AssemblyName;
                scheduler.MinuteInterval = schedulerInfo.MinuteInterval;
                return true;
            }
            return false;
        }
    }
}
