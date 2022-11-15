using JosiArchitecture.Api;
using JosiArchitecture.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace JosiArchitecture.UnitTests
{
    [Collection("Integration tests")]
    public abstract class IntegrationTest : IAsyncLifetime
    {
        private const string ConnectionString = "Server=localhost;Database=JosiArchitecture_Test;User Id=sa;Password=letmepass!!42;TrustServerCertificate=True";

        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        private WebApplicationFactory<Startup> _factory;
        private Respawner _respawner;

        protected HttpClient Client { get; private set; }

        public async Task InitializeAsync()
        {
            await CreateSingleSharedWebClient();
            EnsureDatabaseCreated();
            await CreateRespawnerCheckpoint();
        }

        public async Task DisposeAsync()
        {
            await ResetRespawner();
        }

        private async Task ResetRespawner()
        {
            await _respawner.ResetAsync(ConnectionString);
        }

        private async Task CreateRespawnerCheckpoint()
        {
            _respawner = await Respawner.CreateAsync(ConnectionString);
        }

        private async Task CreateSingleSharedWebClient()
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                if (Client == null)
                {
                    //await StartWebHost();
                    _factory = new WebApplicationFactory<Api.Startup>()
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
                                    options.UseSqlServer(ConnectionString);
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
                dataStore.Database.EnsureCreated();
            }
        }
    }
}