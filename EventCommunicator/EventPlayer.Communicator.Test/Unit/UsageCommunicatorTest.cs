namespace EventPlayer.Communicator.Test.Unit
{
    using EventPlayer.Communicator.Engine;
    using EventPlayer.Communicator.Models;

    using NUnit.Framework;

    [TestFixture]
    public class UsageCommunicatorTest
    {
        private JobEngine jobEngine;

        [Test]
        public void CreateJob()
        {
            // init
            const int ExpectedUsage = 1;

            // run
            this.jobEngine.ExecuteJob(JobType.RecoverNow);
            int actualUsage = this.jobEngine.GetUsageFor(JobType.RecoverNow);
            Assert.AreEqual(ExpectedUsage, actualUsage);
        }
    }
}