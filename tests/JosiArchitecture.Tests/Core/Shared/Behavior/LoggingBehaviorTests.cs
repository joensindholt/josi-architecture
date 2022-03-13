using JosiArchitecture.Core.Shared.Cqs;
using JosiArchitecture.Tests.Shared;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JosiArchitecture.Tests.Core.Shared.Behavior
{
    [Collection(CqsTestsCollection.Name)]
    public class LoggingBehaviorTests
    {
        private CqsTestsFixture fixture;

        public LoggingBehaviorTests(CqsTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task WhenAnUnhandledExceptionOccurs_ThenItShouldBeLogged()
        {
            fixture.Reset();

            // Arrange
            var command = new TestCommand(() => throw new Exception("I'm failing"));

            // Act
            try
            {
                await fixture.Mediator.Send(command);
            }
            catch (Exception)
            {
                // Ignore
            }

            // Assert
            fixture.LoggerMock.VerifyLogging($"Unhandled exception occured handling {typeof(TestCommand).Name}", LogLevel.Error);
        }

        [Fact]
        public async Task WhenRequestIsProcessedSuccesfully_ThenASuccessShouldBeLogger()
        {
            fixture.Reset();

            // Arrange
            var command = new TestCommand(() => { /*Do nothing*/ });

            // Act
            await fixture.Mediator.Send(command);

            // Assert
            fixture.LoggerMock.VerifyLogging($"Handled {typeof(TestCommand).Name}", LogLevel.Information);
        }
    }
}