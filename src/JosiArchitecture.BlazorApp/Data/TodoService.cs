using JosiArchitecture.Core.Todos.Queries.GetTodos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JosiArchitecture.BlazorApp.Data
{
    public class TodoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TodoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetTodosResponse> GetTodosAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GetTodosResponse>("http://xyz/todos");
            return response;
        }
    }
}