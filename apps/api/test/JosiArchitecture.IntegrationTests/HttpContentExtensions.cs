using Newtonsoft.Json;

namespace JosiArchitecture.IntegrationTests
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(contentAsString)!;
            }
            catch (JsonReaderException e)
            {
                throw new Exception($"Could not read json as ´{typeof(T)}´:\n{contentAsString}", e);
            }

        }
    }
}