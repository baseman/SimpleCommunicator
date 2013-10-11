namespace EventPlayer.Communicator.Models.Command
{
    using System;

    using EventPlayer.Communicator.Models.Command.Design;

    public class RnJob : IValidateJob
    {
        public void Validate(DtServer model)
        {
            if (model.IsRnServer)
            {
                throw new InvalidOperationException("You can't add a Recover Now job to a non-recover now server");
            }
        }
    }
}