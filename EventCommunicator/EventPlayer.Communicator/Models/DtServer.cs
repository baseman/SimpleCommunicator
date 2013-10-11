namespace EventPlayer.Communicator.Models
{
    using EventPlayer.Model;

    public class DtServer : Aggregate<DtServer>
    {
        public DtServer(bool isRnServer)
        {
            IsRnServer = isRnServer;
        }

        public bool IsRnServer { get; private set; }
        public int Usages;
    }
}