namespace EventPlayer.Communicator.Models.Event
{
    using EventPlayer.Event;
    using EventPlayer.Model;

    public class RnJobExecutedEvt : PlayEvent<DtServer>
    {
        public RnJobExecutedEvt(AggregateId<DtServer> id)
            : base(id)
        {
        }

        protected override void ApplyChangesTo(DtServer model)
        {
            model.Usages += 1;
        }
    }
}