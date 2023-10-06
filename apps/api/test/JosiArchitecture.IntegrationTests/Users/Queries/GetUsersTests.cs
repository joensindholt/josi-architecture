using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JosiArchitecture.Core.Users.Commands.CreateUser;
using JosiArchitecture.Core.Users.Queries.GetUsers;
using JosiArchitecture.IntegrationTests.Infrastructure;
using Polly;
using Polly.Retry;

namespace JosiArchitecture.IntegrationTests.Users.Queries;

[Collection(IntegrationTestsCollection.Name)]
public class GetUsersTests
{
    private readonly IntegrationTestFixture _fixture;

    public GetUsersTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetUsers_ReturnsStatusCodeOk_And_AListOfUsers()
    {
        // Arrange
        var name = _fixture.Faker.Name.FullName();

        await _fixture.Client.PostAsJsonAsync("/users", new CreateUserRequest
        {
            Name = name
        });

        // Act
        var users = await EventualConsistencyHelper.ExecuteAsync(
            async cancellationToken =>
            {
                var httpResponse = await _fixture.Client.GetAsync("/users", cancellationToken);
                var usersResponse = await httpResponse.Content.ReadAsAsync<GetUsersResponse>();
                var users = usersResponse.Users.ToList();
                return users;
            },
            checker: users => users.Any(u => u.Name == name));

        // Assert
        users.Should().NotBeNull();
        users.Should().NotBeNullOrEmpty();
        users.Should().Contain(l => l.Name == name && l.Id > 0, $"User '{name}' should be in list of users after adding her/him");
    }

    [Fact]
    public async Task GetUsers_SortsUsersCorrectly()
    {
        // Arrange
        var userRequests = new List<CreateUserRequest>
        {
            new() { Name = "Anton" },
            new() { Name = "Ålholm" },
            new() { Name = "AAbenholm" }
        };

        foreach (var user in userRequests)
        {
            await _fixture.Client.PostAsJsonAsync("/users", user);
        }

        // Act
        var users = await EventualConsistencyHelper.ExecuteAsync(
            async cancellationToken =>
            {
                var httpResponse = await _fixture.Client.GetAsync("/users?orderBy=name", cancellationToken);
                var usersResponse = await httpResponse.Content.ReadAsAsync<GetUsersResponse>();
                var users = usersResponse.Users.ToList();
                return users;
            },
            checker: users => users.Any(u => u.Name == userRequests.Last().Name));

        // Assert
        users.FindIndex(u => u.Name == "Anton").Should().BeLessThan(users.FindIndex(u => u.Name == "Ålholm"));
        users.FindIndex(u => u.Name == "Anton").Should().BeLessThan(users.FindIndex(u => u.Name == "AAbenholm"));
        users.FindIndex(u => u.Name == "AAbenholm").Should().BeLessThan(users.FindIndex(u => u.Name == "Ålholm"));
    }
}