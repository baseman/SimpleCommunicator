namespace EventPlayer.Communicator.Engine
{
    using System.Collections.Generic;
    using System.Linq;

    using EventPlayer.Communicator.Models;
    using EventPlayer.Communicator.Models.Command;
    using EventPlayer.Event;
    using EventPlayer.Player;

    public class JobEngine4
    {
        private readonly List<PlayEvent<DtServer>> evts;

        public JobEngine4()
        {
            this.evts = new List<PlayEvent<DtServer>>();
        }

        public void ExecuteJob(RnJobCmd job, DtServer server)
        {
            var evt = job.ExecuteOn(server);
            this.evts.Add(evt);
        }

        public int GetUsageFor<TCommand>() where TCommand : RnJobCmd
        {
            var player = new SimplePlayer<DtServer>();

            var rnEvts = this.evts.Where(x => typeof(TCommand) == x.GetType());

            foreach (var playEvent in rnEvts)
            {
                player.Load(playEvent);
            }
            
            var server = new DtServer(true);

            player.PlayFor(server);

            return server.Usages;
        }

        public string GetUsageReport<TCommand>() where TCommand : RnJobCmd
        {
            int usages = this.GetUsageFor<TCommand>();

            return string.Format(
                "Usage Type: {0} | Usages: {1}",
                typeof(TCommand),
                usages);
        }
    }
}