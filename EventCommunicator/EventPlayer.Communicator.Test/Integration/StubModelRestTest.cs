﻿namespace EventPlayer.Communicator.Test.Integration
{
    using System;

    using EventPlayer.Communicator.Models;
    using EventPlayer.Communicator.Models.Command;
    using EventPlayer.Communicator.Mvc.Controllers;
    using EventPlayer.Communicator.Utils;
    using EventPlayer.Model;

    using NUnit.Framework;

    [TestFixture]
    public class StubModelRestTest
    {
        private CommandController cmdController;

        [SetUp]
        public void Init()
        {
            this.cmdController = new CommandController();
            this.cmdController.ClearAllChanges();
        }

        [Test]
        public void RegisterAndRunProcess()
        {
            // init
            var expected = new StubModel
            {
                AggregateId = new AggregateId<StubModel>("1"),
                LatestVersion = 1,
                Value = 1
            };

            string errCommandStr = Serializer.JsonSerialize(new StubAddCommand { AddValue = -1 });
            string commandStr = Serializer.JsonSerialize(new StubAddCommand { AddValue = 1 });
            
            // run
            Assert.Throws<InvalidOperationException>(() => this.cmdController.Index(expected.AggregateIdVal, errCommandStr));
            this.cmdController.Index(expected.AggregateIdVal, commandStr);
            var actual = (StubModel)this.cmdController.Get(expected.AggregateIdVal).Data;

            Assert.AreEqual(expected.AggregateIdVal, actual.AggregateIdVal);
            Assert.AreEqual(expected.LatestVersion, actual.LatestVersion);
            Assert.AreEqual(expected.Value, actual.Value);
        }
    }
}