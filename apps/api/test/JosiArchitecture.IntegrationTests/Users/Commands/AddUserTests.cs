using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using JosiArchitecture.Core.Users.Commands.CreateUser;

namespace JosiArchitecture.IntegrationTests.Users.Commands;

[Collection(IntegrationTestsCollection.Name)]
public class AddUserTests
{
    private const string UsersEndpoint = "/users";

    private readonly IntegrationTestFixture _fixture;

    public AddUserTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddUser_ReturnsStatusCodeCreated_WhenRequestIsValid()
    {
        // Arrange
        var name = new Faker().Random.Word();

        // Act
        var response = await _fixture.Client.PostAsJsonAsync(
            UsersEndpoint,
            new CreateUserRequest
            {
                Name = name
            },
            CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task AddUser_ReturnsStatusCodeBadRequest_WhenRequestIsInvalid()
    {
        // Arrange
        string? name = null;

        // Act
        var response = await _fixture.Client.PostAsJsonAsync(
            UsersEndpoint,
            new CreateUserRequest
            {
                Name = name!
            },
            CancellationToken.None);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
