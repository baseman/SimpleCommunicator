namespace EventPlayer.Communicator.Engine
{
    using System.Collections.Generic;
    using System.Linq;

    using EventPlayer.Communicator.Models.Command;

    public class JobEngine3
    {
        private readonly List<RnJob> jobs;

        public JobEngine3()
        {
            this.jobs = new List<RnJob>();
        }

        public void ExecuteJob(RnJob job)
        {
            this.jobs.Add(job);
        }

        public int GetUsageFor<TJob>() where TJob : RnJob
        {
            return this.jobs.Count(x => x.GetType() == typeof(TJob));
        }

        public string GetUsageReport<TJob>() where TJob : RnJob
        {
            int usages = this.GetUsageFor<TJob>();
            
            return string.Format(
                "Usage Type: {0} | Usages: {1}", 
                typeof(TJob), 
                usages);
        }
    }
}