using Newtonsoft.Json;

namespace Proj.Console.Utils
{
    public class JsonHelper
    {

        public static string ObjectToJson(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }


    }
}
