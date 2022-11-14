using JosiArchitecture.Core.Shared.Cqs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JosiArchitecture.Tests.Shared
{
    public class TestCommand : ICommand<TestResponse>
    {
        public Action TestAction { get; set; }

        public TestCommand(Action action)
        {
            TestAction = action;
        }
    }

    public class TestResponse
    {
    }

    public class FailingCommandHandler : ICommandHandler<TestCommand, TestResponse>
    {
        public Task<TestResponse> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            request.TestAction();
            return Task.FromResult(new TestResponse());
        }
    }
}
