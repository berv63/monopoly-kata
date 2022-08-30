using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Monopoly.Accessors.Helpers
{
    public static class RequestSerializer
    {
        public static StringContent SerializeRequest<T>(this T req) where T : class
        {
            return new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
        }
    }
}