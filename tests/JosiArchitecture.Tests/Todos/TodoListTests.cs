using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using JosiArchitecture.Core.Todos.Commands;
using JosiArchitecture.Core.Todos.Queries;
using JosiArchitecture.Tests.Infrastructure;
using Xunit;

namespace JosiArchitecture.Tests.Todos
{
    public class TodoListTests : IntegrationTest
    {
        [Fact]
        public async Task GetTodoLists()
        {
            var response = await Client.GetAsync("/todo-lists");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AddTodoList()
        {
            // Arrange
            var title = new Faker().Random.Word();

            // Act
            var response = await Client.PostAsJsonAsync(
                "/todo-lists",
                new AddTodoListRequest
                {
                    Title = title
                });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var list = await response.Content.ReadAsAsync<GetTodoListResponse>();
            list.Id.Should().BeGreaterThan(0);
            list.Title.Should().Be(title);
        }
    }
}