// ReSharper disable ClassNeverInstantiated.Global

using JosiArchitecture.IntegrationTests.TestEnvironments;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace JosiArchitecture.IntegrationTests;

[CollectionDefinition("Integration tests collection")]
public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestFixture>
{
    public const string Name = "Integration tests collection";
}

public class IntegrationTestFixture : IAsyncLifetime
{
    public HttpClient Client => _testEnvironment.Client;

    public Bogus.Faker Faker => new();

    private ITestEnvironment _testEnvironment = null!;

    public async Task InitializeAsync()
    {
        _testEnvironment = new ExternallyHostedEnvironment();
        await _testEnvironment.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await _testEnvironment.DisposeAsync();
    }
}
