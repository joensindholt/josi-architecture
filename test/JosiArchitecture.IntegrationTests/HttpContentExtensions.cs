using Newtonsoft.Json;

namespace JosiArchitecture.IntegrationTests
{
    public static class HttpContentExtensions
    {
        public static async Task<T?> ReadAsAsync<T>(this HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();

            if (contentAsString == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(contentAsString);
        }
    }
}