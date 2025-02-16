using CommonClassLibrary;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerEngine
{
    public class SchedulerJob(SchedulerModel schedulerModel) : ISchedulerJob, IHostedService
    {
        private readonly SchedulerModel _schedulerModel = schedulerModel;
        private Timer? _timer;

        public SchedulerModel Information => _schedulerModel;

        public bool Start()
        {
            _schedulerModel.IsActive = true;
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(_schedulerModel.MinuteInterval));
            return true;
        }

        
        private void DoWork(object? state)
        {
            try
            {
                // Create an instance of the job class using reflection
                var type = Type.GetType($"{_schedulerModel.NameSpace}.{_schedulerModel.ClassName}, {_schedulerModel.AssemblyName}");
                if (type == null || !typeof(INikhJob).IsAssignableFrom(type))
                {
                    throw new InvalidOperationException("Invalid namespace or class name.");
                }

                var instance = Activator.CreateInstance(type) as INikhJob;
                if (instance == null)
                {
                    throw new InvalidOperationException("Failed to create job instance.");
                }

                // Execute the job
                bool result = instance.Run();
                _schedulerModel.LastExecution = DateTime.Now;
            }
            catch (Exception ex)
            {
                Stop();
                Console.WriteLine($"Error in DoWork: {ex.Message}");
            }
        }

        public bool Stop()
        {
            _schedulerModel.IsActive = false;
            _timer?.Change(Timeout.Infinite, 0);
            _timer?.Dispose();
            return true;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Stop();
            return Task.CompletedTask;
        }
    }
}
