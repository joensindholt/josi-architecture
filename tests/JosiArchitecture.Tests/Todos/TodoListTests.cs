using Bogus;
using FluentAssertions;
using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoLists;
using JosiArchitecture.Tests.Infrastructure;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
                new AddTodoListCommand
                {
                    Title = title
                });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var lists = await response.Content.ReadAsAsync<GetTodoListsResponse>();
            lists.TodoLists.Should().NotBeEmpty();
            lists.TodoLists.Should().AllSatisfy(l => l.Id.Should().BeGreaterThan(0));
            lists.TodoLists.Should().AllSatisfy(l => l.Title.Should().NotBeNullOrWhiteSpace());
        }
    }
}
