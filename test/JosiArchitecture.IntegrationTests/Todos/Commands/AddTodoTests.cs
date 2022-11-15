using Bogus;
using FluentAssertions;
using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using System.Net;
using System.Net.Http.Json;
using System.Threading;

namespace JosiArchitecture.UnitTests.Todos.Commands
{
    public class AddTodoTests : IntegrationTest
    {
        [Fact]
        public async Task AddTodoList_ReturnsStatusCodeCreated_WhenRequestIsValid()
        {
            // Arrange
            var title = new Faker().Random.Word();

            // Act
            var response = await Client.PostAsJsonAsync(
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
            string? title = null;

            // Act
            var response = await Client.PostAsJsonAsync(
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