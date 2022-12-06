using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using JosiArchitecture.Api;
using JosiArchitecture.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace JosiArchitecture.UnitTests
{
    [CollectionDefinition("Integration tests collection")]
    public class IntegrationTestsCollection : ICollectionFixture<IntegrationTestFixture>
    {
        public const string Name = "Integration tests collection";
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    public class IntegrationTestFixture : IAsyncLifetime
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private Respawner _respawner;
        private string _connectionString;
        private WebApplicationFactory<Program> _factory;
        private MsSqlTestcontainer _dbContainer;

        public HttpClient Client { get; private set; }

        public async Task InitializeAsync()
        {
            await CreateSingleSharedWebClient();
            EnsureDatabaseCreated();
            await CreateRespawnerCheckpoint();
        }

        public async Task DisposeAsync()
        {
            await ResetDatabase();
            await _dbContainer.StopAsync();
            await _dbContainer.CleanUpAsync();
        }

        public async Task ResetDatabase()
        {
            await _respawner.ResetAsync(_connectionString);
        }

        private async Task CreateRespawnerCheckpoint()
        {
            _respawner = await Respawner.CreateAsync(_connectionString);
        }

        private async Task CreateSingleSharedWebClient()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (Client == null)
                {
                    // Initialize container with database
                    var testcontainersBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
                        .WithDatabase(new MsSqlTestcontainerConfiguration
                        {
                            Password = "letmepass!!42"
                        });

                    _dbContainer = testcontainersBuilder.Build();
                    await _dbContainer.StartAsync();

                    _connectionString = $"Server=localhost,{_dbContainer.Port};Database=JosiArchitecture_Test;User Id=sa;Password=letmepass!!42;TrustServerCertificate=True";

                    _factory = new WebApplicationFactory<Program>()
                        .WithWebHostBuilder(builder =>
                        {
                            builder.ConfigureServices(services =>
                            {
                            });

                            builder.ConfigureTestServices(services =>
                            {
                                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataStore>));
                                services.Remove(descriptor);

                                services.AddDbContext<DataStore>(options =>
                                {
                                    //options.UseInMemoryDatabase("Whatever");
                                    options.UseSqlServer(_connectionString);
                                });
                            });
                        });

                    Client = _factory.CreateClient();
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private void EnsureDatabaseCreated()
        {
            _factory = _factory ?? throw new NotSupportedException("The WebApplicationFactory must be initialized prioer to calling this method");

            using (var scope = _factory.Services.CreateScope())
            {
                var dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
                dataStore.Database.Migrate();
            }
        }
    }
}