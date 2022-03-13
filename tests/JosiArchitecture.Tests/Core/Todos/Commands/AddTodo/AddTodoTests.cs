using FluentAssertions;
using FluentValidation;
using JosiArchitecture.Core.Todos.Commands.AddTodo;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JosiArchitecture.Tests.Core.Todos.Commands.AddTodo
{
    [Collection(CqsTestsCollection.Name)]
    public class AddTodoTests
    {
        private CqsTestsFixture fixture;

        public AddTodoTests(CqsTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task WhenAddingAValidTodo_ThenTheCommandShouldSucceed()
        {
            fixture.Reset();

            // Arrange
            var command = new AddTodoCommand
            {
                Title = "My test todo"
            };

            // Act
            var response = await fixture.Mediator.Send(command);

            // Assert
            response.Id.Should().BeGreaterThan(0);

            fixture.UnitOfWorkMock.Verify(uow => uow.CompleteAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task WhenAddingTodoWithNoTitle_ThenTheRequestShouldFail()
        {
            fixture.Reset();

            // Arrange
            var command = new AddTodoCommand
            {
                Title = null
            };

            // Act
            Func<Task> act = () => fixture.Mediator.Send(command);

            // Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("*'Title' must not be empty.*");
        }
    }
}