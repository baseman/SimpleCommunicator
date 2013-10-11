namespace EventPlayer.Communicator.Models.Command
{
    using EventPlayer.Command;
    using EventPlayer.Communicator.Models.Event;
    using EventPlayer.Event;
    using EventPlayer.Model;

    public class RnJobCmd : PlayCommand<DtServer>
    {
        private readonly RnJob validateJob;

        public RnJobCmd()
        {
            this.validateJob = new RnJob();
        }

        protected override void Validate(DtServer model)
        {
            this.validateJob.Validate(model);
        }

        protected override PlayEvent<DtServer> GetEvent(AggregateId<DtServer> id)
        {
            return new RnJobExecutedEvt(id);
        }
    }
}