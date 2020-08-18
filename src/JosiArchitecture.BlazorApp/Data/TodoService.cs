using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JosiArchitecture.Core.Todos.Queries;
using static JosiArchitecture.Core.Todos.Queries.GetTodosResponse;

namespace JosiArchitecture.BlazorApp.Data
{
    public class TodoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TodoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<TodoResponse>> GetTodosAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GetTodosResponse>("http://xyz/todos");
            return response.Todos;
        }
    }
}
