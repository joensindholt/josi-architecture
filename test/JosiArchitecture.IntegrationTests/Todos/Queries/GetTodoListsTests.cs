using FluentAssertions;
using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoLists;
using JosiArchitecture.IntegrationTests;
using System.Net;
using System.Net.Http.Json;

namespace JosiArchitecture.UnitTests.Todos.Queries
{
    public class GetTodoListsTests : IntegrationTest
    {
        [Fact]
        public async Task GetTodoLists_StatusCodeOkAndReturnsListOfTodoLists()
        {
            // Arrange
            await Client.PostAsJsonAsync("/todolists", new AddTodoListCommand
            {
                Title = "Test"
            });

            // Act
            var response = await Client.GetAsync("/todolists");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var lists = await response.Content.ReadAsAsync<GetTodoListsResponse>();
            lists.Should().NotBeNull();
            lists!.TodoLists.Should().NotBeNullOrEmpty();
            lists!.TodoLists.Should().Satisfy(l => l.Id > 0);
            lists!.TodoLists.Should().Satisfy(l => l.Title == "Test");
        }
    }
}