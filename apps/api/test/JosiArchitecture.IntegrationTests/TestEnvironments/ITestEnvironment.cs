namespace JosiArchitecture.IntegrationTests.TestEnvironments;

public interface ITestEnvironment
{
    HttpClient Client { get; }

    Task InitializeAsync();

    Task DisposeAsync();
}