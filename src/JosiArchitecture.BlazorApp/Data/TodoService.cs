using JosiArchitecture.Core.Todos.Queries.GetTodoList;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JosiArchitecture.BlazorApp.Data
{
    public class TodoListService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TodoListService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetTodoListResponse> GetTodoListAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<GetTodoListResponse>("http://xyz/todolist");
            return response;
        }
    }
}