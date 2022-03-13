using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Api;
using JosiArchitecture.Core.Shared;
using JosiArchitecture.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace JosiArchitecture.Tests
{
    public abstract class IntegrationTest : IAsyncLifetime
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        //private IHost _host;
        private WebApplicationFactory<Startup> _factory;

        public IntegrationTest()
        {
            if (_factory != null)
            {
                EnsureCleanDatabase();
            }
        }

        protected HttpClient Client { get; private set; }

        public async Task InitializeAsync()
        {
            await CreateSingleSharedWebClient();
            EnsureCleanDatabase();
        }

        public async Task DisposeAsync()
        {
            //_host.Dispose();
            await Task.CompletedTask;
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
                                var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<DataStore>));
                                services.Remove(descriptor);

                                services.AddDbContext<DataStore>(options => options.UseSqlite("Filename=Test.db"));
                            });

                            builder.ConfigureTestServices(services =>
                            {
                                ////var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<DataStore>));
                                ////services.Remove(descriptor);

                                //services.AddDbContext<DataStore>(options => options.UseSqlite("Filename=Test.db"));
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

        private void EnsureCleanDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
                System.Console.WriteLine("dataStore.Database.IsSqlite", dataStore.Database.IsSqlite());
                dataStore.Database.EnsureDeleted();
                dataStore.Database.EnsureCreated();
            }
        }

        private Assembly GetApiAssembly() => typeof(Startup).Assembly;

        private Assembly GetCoreAssembly() => typeof(IQueryDataStore).Assembly;

        private Assembly GetDataAssembly() => typeof(DataStore).Assembly;

        //private async Task StartWebHost()
        //{
        //    _host = await new HostBuilder()
        //        .ConfigureWebHost(webBuilder =>
        //        {
        //            webBuilder
        //                .UseTestServer()
        //                .ConfigureServices((services) =>
        //                {
        //                    // Custom test services
        //                    services.AddDbContext<DataStore>(options => options.UseSqlite("Filename=Test.db"));
        //                    services.AddLogging();

        //                    // Regular app services
        //                    services.AddApiServices();
        //                    services.AddCoreServices(GetApiAssembly(), GetCoreAssembly(), GetDataAssembly());
        //                    services.AddDataServices();
        //                })
        //                .Configure(app =>
        //                {
        //                    app.UseRouting();
        //                    app.UseEndpoints(endpoints =>
        //                    {
        //                        endpoints.MapControllers();
        //                    });
        //                });
        //        })
        //        .StartAsync();
        //}
    }
}