namespace EventPlayer.Communicator.Models.Command
{
    using System;

    using EventPlayer.Command;
    using EventPlayer.Communicator.Models.Event;
    using EventPlayer.Event;
    using EventPlayer.Model;

    public class StubAddCommand : PlayCommand<StubModel>
    {
        public int AddValue;

        protected override void Validate(StubModel model)
        {
            if (this.AddValue < 0)
            {
                throw new InvalidOperationException("Adding a negative value ain't addin' byes");
            }
        }

        protected override PlayEvent<StubModel> GetEvent(AggregateId<StubModel> id)
        {
            return new StubAddedEvent(id, this.AddValue);
        }
    }
}