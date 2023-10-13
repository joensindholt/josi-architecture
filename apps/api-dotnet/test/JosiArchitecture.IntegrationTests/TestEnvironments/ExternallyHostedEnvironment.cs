namespace JosiArchitecture.IntegrationTests.TestEnvironments;

/// <summary>
/// Use this if you host the api externally (ie. using docker-compose in this project)
/// </summary>
public class ExternallyHostedEnvironment : ITestEnvironment
{
    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("http://localhost:8000");
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        await Task.CompletedTask;
    }
}
