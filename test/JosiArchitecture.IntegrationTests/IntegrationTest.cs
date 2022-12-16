using JosiArchitecture.IntegrationTests;

namespace JosiArchitecture.UnitTests
{
    [CollectionDefinition("Integration tests collection")]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestFixture>
    {
        public const string Name = "Integration tests collection";
    }

    public class IntegrationTestFixture : IAsyncLifetime
    {
        public TestEnvironment? TestEnvironment { get; private set; }

        public HttpClient? Client => TestEnvironment?.Client;

        public async Task InitializeAsync()
        {
            TestEnvironment = new TestEnvironment();
            await TestEnvironment.InitializeAsync();
        }

        public async Task DisposeAsync()
        {
            if (TestEnvironment != null)
            {
                await TestEnvironment.DisposeAsync();
            }
        }

        public async Task ResetDatabase()
        {
            if (TestEnvironment == null)
            {
                throw new InvalidOperationException("Cannot reset database before test environment has been initialized");
            }

            await TestEnvironment.ResetDatabaseAsync();
        }
    }
}