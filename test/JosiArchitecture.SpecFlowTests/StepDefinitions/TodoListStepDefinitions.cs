using JosiArchitecture.Core.Todos.Commands.AddTodoList;
using JosiArchitecture.Core.Todos.Queries.GetTodoLists;
using JosiArchitecture.IntegrationTests;
using System.Net.Http.Json;

namespace JosiArchitecture.SpecFlowTests.StepDefinitions
{
    [Binding]
    public sealed class TodoListStepDefinitions
    {
        private GetTodoListsResponse? _todoListsResponse;

        [Given("I add a todo list named '(.*)'")]
        public async Task IAddATodoList(string title)
        {
            await Hooks.Hooks.Client.PostAsJsonAsync(
                "/todolists",
                new AddTodoListCommand
                {
                    Title = title
                },
                CancellationToken.None);
        }

        [When("I request all todo lists")]
        public async Task IRequestAllTodoLists()
        {
            var response = await Hooks.Hooks.Client.GetAsync("/todolists");
            response.EnsureSuccessStatusCode();
            _todoListsResponse = await response.Content.ReadAsAsync<GetTodoListsResponse>();
        }

        [Then("I get a list of todo lists containing one named '(.*)'")]
        public void ThenTheResultShouldBe(string title)
        {
            _todoListsResponse.Should().NotBeNull();
            _todoListsResponse.TodoLists.Should().NotBeEmpty();
            _todoListsResponse.TodoLists.Should().Satisfy(x => x.Title == title);
        }
    }
}