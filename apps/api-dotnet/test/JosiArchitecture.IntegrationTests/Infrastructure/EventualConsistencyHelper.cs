using Polly;
using Polly.Retry;

namespace JosiArchitecture.IntegrationTests.Infrastructure;

public static class EventualConsistencyHelper
{
    static ResiliencePipeline _pipeline = new ResiliencePipelineBuilder()
        .AddRetry(new RetryStrategyOptions { Delay = TimeSpan.FromMilliseconds(100), MaxRetryAttempts = 10 })
        .Build();

    public static async ValueTask<TResult> ExecuteAsync<TResult>(Func<CancellationToken, ValueTask<TResult>> getter, Func<TResult, bool> checker)
    {
        return await _pipeline.ExecuteAsync<TResult>(async cancellationToken =>
        {
            var result = await getter(cancellationToken);

            if (!checker(result))
            {
                throw new Exception("Check failed");
            }

            return result;
        });
    }
}
