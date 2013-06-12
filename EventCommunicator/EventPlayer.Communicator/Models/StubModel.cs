namespace EventPlayer.Communicator.Models
{
    using EventPlayer.Model;

    public class StubModel : Aggregate<StubModel>
    {
        public long Value;
    }
}