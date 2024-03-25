using Newtonsoft.Json;

namespace Extensions
{
    public static class Constants
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };
    }
}