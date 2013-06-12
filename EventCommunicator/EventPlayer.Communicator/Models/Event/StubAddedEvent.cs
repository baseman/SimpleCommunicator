namespace EventPlayer.Communicator.Models.Event
{
    using EventPlayer.Event;
    using EventPlayer.Model;

    public class StubAddedEvent : PlayEvent<StubModel>
    {
        public int AddValue { get; set; }

        public StubAddedEvent(AggregateId<StubModel> id, int addValue)
            : base(id)
        {
            this.AddValue = addValue;
        }

        protected override void ApplyChangesTo(StubModel model)
        {
            model.Value += this.AddValue;
        }
    }
}