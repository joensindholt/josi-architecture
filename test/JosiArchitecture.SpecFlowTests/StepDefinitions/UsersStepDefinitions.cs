using JosiArchitecture.IntegrationTests;
using System.Net.Http.Json;
using JosiArchitecture.Core.Users.Commands.CreateUser;
using JosiArchitecture.Core.Users.Queries.GetUsers;
using JosiArchitecture.IntegrationTests.Infrastructure;

namespace JosiArchitecture.SpecFlowTests.StepDefinitions
{
    [Binding]
    public sealed class UsersStepDefinitions
    {
        [Given("I add a user named '(.*)'")]
        public async Task AddAUserNamed(string name)
        {
            await Hooks.Hooks.Client.PostAsJsonAsync(
                "/users",
                new CreateUserRequest
                {
                    Name = name
                },
                CancellationToken.None);
        }

        [Then("I get a list of users containing one named '(.*)'")]
        public async Task GetAListOfUsersContainingOneNamed(string name)
        {
            var users = await EventualConsistencyHelper.ExecuteAsync(
                async cancellationToken =>
                {
                    var httpResponse = await Hooks.Hooks.Client.GetAsync("/users", cancellationToken);
                    var usersResponse = await httpResponse.Content.ReadAsAsync<GetUsersResponse>();
                    var users = usersResponse.Users.ToList();
                    return users;
                },
                checker: users => users.Any(u => u.Name == name));

            users.Should().NotBeNull();
            users.Should().NotBeEmpty();
            users.Should().Contain(x => x.Name == name);
        }
    }
}