using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public class SchedulerDataJsonService : ISchedulerDataService
    {
        private readonly string _filePath = "schedule_information.json";

        // Get all scheduler jobs from the JSON file
        public List<SchedulerModel> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<SchedulerModel>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<SchedulerModel>>(json) ?? new List<SchedulerModel>();
        }

        // Get a specific scheduler job by ID
        public SchedulerModel? Get(int schedulerId)
        {
            var jobs = GetAll();
            return jobs.FirstOrDefault(j => j.SchedulerId == schedulerId);
        }

        // Add a new scheduler job to the JSON file
        public SchedulerModel Add(SchedulerModel schedulerInfo)
        {
            var jobs = GetAll();
            schedulerInfo.SchedulerId = jobs.Any() ? jobs.Max(j => j.SchedulerId) + 1 : 1; // Auto-generate ID
            jobs.Add(schedulerInfo);
            SaveToFile(jobs);
            return schedulerInfo;
        }

        // Update an existing scheduler job in the JSON file
        public bool Update(SchedulerModel schedulerInfo)
        {
            var jobs = GetAll();
            var existingJob = jobs.FirstOrDefault(j => j.SchedulerId == schedulerInfo.SchedulerId);
            if (existingJob == null)
            {
                return false; // Job not found
            }

            // Update the job properties
            existingJob.NameSpace = schedulerInfo.NameSpace;
            existingJob.ClassName = schedulerInfo.ClassName;
            existingJob.AssemblyName = schedulerInfo.AssemblyName;
            existingJob.MinuteInterval = schedulerInfo.MinuteInterval;
            existingJob.LastExecution = schedulerInfo.LastExecution;
            existingJob.IsActive = schedulerInfo.IsActive;

            SaveToFile(jobs);
            return true;
        }

        // Remove a scheduler job from the JSON file
        public bool Remove(int schedulerId)
        {
            var jobs = GetAll();
            var jobToRemove = jobs.FirstOrDefault(j => j.SchedulerId == schedulerId);
            if (jobToRemove == null)
            {
                return false; // Job not found
            }

            jobs.Remove(jobToRemove);
            SaveToFile(jobs);
            return true;
        }

        // Helper method to save the list of jobs to the JSON file
        private void SaveToFile(List<SchedulerModel> jobs)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jobs, options);
            File.WriteAllText(_filePath, json);
        }
    }

}
