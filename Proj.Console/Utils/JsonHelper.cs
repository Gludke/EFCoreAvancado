using Newtonsoft.Json;

namespace Proj.Console.Utils
{
    public class JsonHelper
    {
        private JsonSerializerSettings _settings;

        public JsonHelper()
        {
            _settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public string ObjectToJson(object obj) => JsonConvert.SerializeObject(obj, _settings);

    }
}
