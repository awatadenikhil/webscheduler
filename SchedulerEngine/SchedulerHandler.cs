using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public class SchedulerHandler
    {
        private readonly List<SchedulerJob> _jobs = new List<SchedulerJob>();
        private ISchedulerDataService _schedulerDataService = new SchedulerDataJsonService(); // new SchedulerDataService();
        private int _nextId = 1;

        public List<SchedulerModel> GetAll()
        {
            return _jobs.Select(j => j.Information).ToList();
        }

        public SchedulerModel? Get(int schedulerId)
        {
            return _jobs.FirstOrDefault(j => j.Information.SchedulerId == schedulerId)?.Information;
        }

        public void Add(SchedulerModel schedulerInfo)
        {
            schedulerInfo.SchedulerId = _nextId++;
            schedulerInfo.LastExecution = DateTime.MinValue;
            schedulerInfo.IsActive = true;
            _schedulerDataService.Add(schedulerInfo);
            var job = new SchedulerJob(schedulerInfo);            
            job.Start();
            _jobs.Add(job);
        }

        public void Edit(SchedulerModel schedulerInfo)
        {
            var job = _jobs.FirstOrDefault(j => j.Information.SchedulerId == schedulerInfo.SchedulerId);
            if (job != null && _schedulerDataService.Update(schedulerInfo))
            {                
                job.Stop();
                job.Information.AssemblyName = schedulerInfo.AssemblyName;
                job.Information.NameSpace = schedulerInfo.NameSpace;
                job.Information.ClassName = schedulerInfo.ClassName;
                job.Information.MinuteInterval = schedulerInfo.MinuteInterval;
                job.Information.LastExecution = DateTime.MinValue;
                job.Start();
            }
        }

        public void Remove(int schedulerId)
        {
            var job = _jobs.FirstOrDefault(j => j.Information.SchedulerId == schedulerId);
            if (job != null && _schedulerDataService.Remove(schedulerId))
            {
                job.Stop();
                _jobs.Remove(job);
            }
        }

        public void StartAll()
        {
            foreach (var item in _schedulerDataService.GetAll())
            {
                _jobs.Add(new SchedulerJob(item));
            };
            foreach (var job in _jobs)
            {
                job.Start();
            }
        }

        public void StopAll()
        {
            foreach (var job in _jobs)
            {
                job.Stop();
            }
        }
    }
}
