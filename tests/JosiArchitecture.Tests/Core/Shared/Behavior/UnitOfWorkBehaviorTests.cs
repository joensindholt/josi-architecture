using JosiArchitecture.Tests.Shared;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JosiArchitecture.Tests.Core.Shared.Behavior
{
    [Collection(CqsTestsCollection.Name)]
    public class UnitOfWorkBehaviorTests
    {
        private CqsTestsFixture fixture;

        public UnitOfWorkBehaviorTests(CqsTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task It_CompletesUnitOfWork_When_CommandCompletes()
        {
            fixture.Reset();

            // Arrange
            var command = new TestCommand(() => { /*Do nothing*/ });

            // Act
            await fixture.Mediator.Send(command);

            // Assert
            fixture.UnitOfWorkMock.Verify(uow => uow.CompleteAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}