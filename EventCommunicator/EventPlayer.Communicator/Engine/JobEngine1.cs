namespace EventPlayer.Communicator.Engine
{
    using System.Text;

    using EventPlayer.Communicator.Models;

    public class JobEngine1
    {
        private int recoverNow;

        public JobEngine1()
        {
            this.recoverNow = 0;
        }

        public void ExecuteJob(JobType jobType)
        {
            if (jobType == JobType.RecoverNow)
            {
                this.recoverNow++;
            }
        }

        public int GetUsageFor(JobType jobType)
        {
            return this.recoverNow;
        }

        public string GetUsageReport()
        {
            var report = new StringBuilder();
            const string ReportItem = "Usage Type: {0} | Usages: {1}";

            report.Append(string.Format(ReportItem, JobType.RecoverNow, this.GetUsageFor(JobType.RecoverNow)));
            report.Append(',');

            return report.ToString();
        }
    }
}