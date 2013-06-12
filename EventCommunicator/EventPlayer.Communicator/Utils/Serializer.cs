namespace EventPlayer.Communicator.Utils
{
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class Serializer
    {

        public static T JsonDeserialize<T>(string data)
        {
            var serializer = new JsonSerializer
                                 {
                                     TypeNameHandling = TypeNameHandling.Objects
                                 };

            using (var memory = new StringReader(data))
            using (var json = new JsonTextReader(memory))
            {
                return serializer.Deserialize<T>(json);
            }
        }

        public static string JsonSerialize<T>(T data)
        {
            var serializer = new JsonSerializer
                                 {
                                     TypeNameHandling = TypeNameHandling.Objects,
                                 };

            serializer.Converters.Add(new IsoDateTimeConverter());

            using (var strWriter = new StringWriter())
            {
                using (var writer = new JsonTextWriter(strWriter))
                {
                    serializer.Serialize(writer, data);
                }

                return strWriter.GetStringBuilder().ToString();
            }
        }
    }
}