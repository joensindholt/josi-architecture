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

namespace JosiArchitecture.IntegrationTests
{
    public class TestEnvironment : IAsyncDisposable
    {
        private MsSqlTestcontainer? _dbContainer;
        private Respawner? _respawner;
        private string? _databaseConnectionString;

        public IServiceProvider? ApplicationServices { get; private set; }
        public HttpClient? Client { get; private set; }

        public async Task InitializeAsync()
        {
            await InitializeTestDatabaseContainer();

            // Create Web App
            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                    });

                    builder.ConfigureTestServices(services =>
                    {
                        var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<DataStore>));
                        services.Remove(descriptor);

                        services.AddDbContext<DataStore>(options =>
                        {
                            //options.UseInMemoryDatabase("Whatever");
                            options.UseSqlServer(_databaseConnectionString);
                        });
                    });
                });

            ApplicationServices = factory.Services;

            EnsureDatabaseCreated();
            _respawner = await Respawner.CreateAsync(_databaseConnectionString);

            Client = factory.CreateClient();
        }

        public async Task ResetDatabaseAsync()
        {
            if (_respawner != null && _databaseConnectionString != null)
            {
                await _respawner.ResetAsync(_databaseConnectionString);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbContainer != null)
            {
                await _dbContainer.StopAsync();
                await _dbContainer.CleanUpAsync();
            }
        }

        private async Task InitializeTestDatabaseContainer()
        {
            var password = "letmepass!!42";

            var testcontainersBuilder =
                    new TestcontainersBuilder<MsSqlTestcontainer>()
                        .WithDatabase(new MsSqlTestcontainerConfiguration
                        {
                            Password = password
                        });

            _dbContainer = testcontainersBuilder.Build();
            await _dbContainer.StartAsync();

            _databaseConnectionString = $"Server=localhost,{_dbContainer.Port};Database=JosiArchitecture_Test;User Id=sa;Password={password};TrustServerCertificate=True";
        }

        private void EnsureDatabaseCreated()
        {
            using var scope = ApplicationServices!.CreateScope();
            var dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
            dataStore.Database.Migrate();
        }
    }
}