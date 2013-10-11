namespace EventPlayer.Communicator.Engine
{
    using System.Collections.Generic;
    using System.Text;

    using EventPlayer.Communicator.Models;

    public class JobEngine2
    {
        private readonly Dictionary<JobType, int> jobs;

        public JobEngine2()
        {
            this.jobs = new Dictionary<JobType, int>();
        }

        public void ExecuteJob(JobType jobType)
        {
            if (this.jobs.ContainsKey(jobType))
            {
                this.jobs.Add(jobType, 0);
            }

            this.jobs[jobType] += 1;
        }

        public int GetUsageFor(JobType jobType)
        {
            return this.jobs[jobType];
        }

        public string GetUsageReport()
        {
            var report = new StringBuilder();
            const string ReportItem = "Usage Type: {0} | Usages: {1}";

            report.Append(string.Format(ReportItem, this.GetUsageFor(JobType.RecoverNow)));
            report.Append(',');

            return report.ToString();
        }
    }
}