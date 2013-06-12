namespace EventPlayer.Communicator.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using EventPlayer.Command;
    using EventPlayer.Communicator.Models;
    using EventPlayer.Communicator.Models.Event;
    using EventPlayer.Communicator.Mvc.Handler;
    using EventPlayer.Communicator.Utils;
    using EventPlayer.Event;
    using EventPlayer.Player;

    using ServiceStack.Redis;

    public class CommandController : System.Web.Mvc.Controller
    {
        [HttpPost]
        [HandleJsonException]
        public JsonResult Index(string modelId, string commandData)
        {
            this.ValidateInitialInput(modelId, commandData);

            var cmd = Serializer.JsonDeserialize<PlayCommand<StubModel>>(commandData);
            var evt = cmd.ExecuteOn(this.NewModel(modelId));

            this.SetChanges(modelId, evt);

            return this.Json("Change posted");
        }

        [HttpGet]
        public JsonResult Get(string modelId)
        {
            var model = this.NewModel(modelId);

            var changes = this.GetChangesFor(modelId);

            this.MakePlayer(changes).PlayFor(model);

            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetChanges(string modelId)
        {
            return this.Json(this.GetChangesFor(modelId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult ClearAllChanges()
        {
            using (var redisClient = new RedisClient())
            {
                var redis = redisClient.GetTypedClient<StubAddedEvent>();

                redis.SetSequence(0);
                redis.FlushAll();
            }

            return this.Json("Changes cleared");
        }

        #region Helpers
        private void ValidateInitialInput(string idVal, string commandStr)
        {
            if (string.IsNullOrEmpty(idVal) || string.IsNullOrEmpty(commandStr))
            {
                throw new NullReferenceException();
            }
        }
        
        private List<PlayEvent<StubModel>> GetChangesFor(string modelId)
        {
            using (var redisClient = new RedisClient())
            {
                var redis = redisClient.GetTypedClient<PlayEvent<StubModel>>();

                return redis.Lists["urn:changes:" + modelId].GetAll();
            }
        }

        private SimplePlayer<StubModel> MakePlayer(IEnumerable<PlayEvent<StubModel>> changes)
        {
            var evtPlayer = new SimplePlayer<StubModel>();
            foreach (var playEvent in changes)
            {
                evtPlayer.Load(playEvent);
            }

            return evtPlayer;
        }

        private void SetChanges(string modelId, PlayEvent<StubModel> evt)
        {
            using (var redisClient = new RedisClient())
            {
                var redis = redisClient.GetTypedClient<PlayEvent<StubModel>>();
                redis.Lists["urn:changes:" + modelId].Append(evt);
                Console.Write(evt.ToString());
            }
        }

        private StubModel NewModel(string modelId)
        {
            return new StubModel { AggregateIdVal = modelId };
        }
        #endregion
    }
}