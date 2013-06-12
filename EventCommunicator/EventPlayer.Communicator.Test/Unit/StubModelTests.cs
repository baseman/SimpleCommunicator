namespace EventPlayer.Communicator.Test.Unit
{
    using System;

    using EventPlayer.Communicator.Models;
    using EventPlayer.Communicator.Models.Command;
    using EventPlayer.Communicator.Models.Event;
    using EventPlayer.Event;
    using EventPlayer.Model;
    using EventPlayer.Player;

    using NUnit.Framework;

    [TestFixture]
    public class StubModelTests
    {
        public void AddCommand()
        {
            // init
            const int CurrentVersion = 0;
            const int ExpectedVersion = CurrentVersion + 1;
            const int AddValue = 1;

            var model = new StubModel
                            {
                                AggregateId = new AggregateId<StubModel>("1"),
                                LatestVersion = CurrentVersion,
                                Value = 1
                            };

            long expectedVal = model.Value + AddValue;

            var expectedEvt = new StubAddedEvent(new AggregateId<StubModel>("1"), AddValue) { Version = ExpectedVersion };
            // ----note the 'ed'---------^^


            // run

            // validation exception
            Assert.Throws<InvalidOperationException>(() => new StubAddCommand { AddValue = -1 }.ExecuteOn(model));

            // successful command
            var actualEvt = new StubAddCommand { AddValue = AddValue }.ExecuteOn(model);
            this.assertEvents(expectedEvt, actualEvt);

            // playback
            var player = new SimplePlayer<StubModel>();
            player.Load(actualEvt);
            player.PlayFor(model);

            // end value after playback
            Assert.AreEqual(expectedVal, model.Value);
        }

        private void assertEvents(PlayEvent<StubModel> expectedEvt, PlayEvent<StubModel> actualEvt)
        {
            Assert.AreEqual(expectedEvt.Id.Value, actualEvt.Id.Value);
            Assert.AreEqual(expectedEvt.Version, actualEvt.Version);
        }
    }
}
