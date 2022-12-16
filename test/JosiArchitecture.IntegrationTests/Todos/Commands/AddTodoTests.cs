using Bogus;
using FluentAssertions;
using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using System.Net;
using System.Net.Http.Json;

namespace JosiArchitecture.UnitTests.Todos.Commands
{
    [Collection(IntegrationTestsCollection.Name)]
    public class AddTodoTests
    {
        private readonly IntegrationTestFixture _fixture;

        public AddTodoTests(IntegrationTestFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task AddTodoList_ReturnsStatusCodeCreated_WhenRequestIsValid()
        {
            // Arrange
            await _fixture.ResetDatabase();

            var title = new Faker().Random.Word();

            // Act
            var response = await _fixture.Client!.PostAsJsonAsync(
                "/todolists",
                new AddTodoListCommand
                {
                    Title = title
                },
                CancellationToken.None);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddTodoList_ReturnsStatusCodeBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            await _fixture.ResetDatabase();

            string? title = null;

            // Act
            var response = await _fixture.Client!.PostAsJsonAsync(
                "/todolists",
                new AddTodoListCommand
                {
                    Title = title
                },
                CancellationToken.None);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}