using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace JosiArchitecture.Tests.Infrastructure
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();
            var contentAsT = JsonConvert.DeserializeObject<T>(contentAsString);
            return contentAsT;
        }
    }
}
