using FluentAssertions;
using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoLists;
using JosiArchitecture.IntegrationTests;
using System.Net;
using System.Net.Http.Json;

namespace JosiArchitecture.UnitTests.Todos.Queries
{
    [Collection(IntegrationTestsCollection.Name)]
    public class GetTodoListsTests
    {
        private readonly IntegrationTestFixture _fixture;

        public GetTodoListsTests(IntegrationTestFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task GetTodoLists_StatusCodeOkAndReturnsListOfTodoLists()
        {
            // Arrange
            await _fixture.ResetDatabase();

            await _fixture.Client.PostAsJsonAsync("/todolists", new AddTodoListCommand
            {
                Title = "Test"
            });

            // Act
            var response = await _fixture.Client.GetAsync("/todolists");

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