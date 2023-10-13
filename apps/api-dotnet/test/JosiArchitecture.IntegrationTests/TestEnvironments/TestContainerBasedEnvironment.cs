// using JosiArchitecture.Api;
// using JosiArchitecture.Data;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.AspNetCore.TestHost;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Testcontainers.MsSql;
//
// namespace JosiArchitecture.IntegrationTests.TestEnvironments;
//
// /// <summary>
// /// Use this if you want the test suite to spin up containers for external dependencies (.ie the database) by itself.
// /// </summary>
// public class TestContainerBasedEnvironment : ITestEnvironment
// {
//     private MsSqlContainer? _dbContainer;
//     private string? _databaseConnectionString;
//
//     private IServiceProvider? _applicationServices;
//
//     public HttpClient Client { get; private set; } = null!;
//
//     public async Task InitializeAsync()
//     {
//         await InitializeTestDatabaseContainer();
//
//         // Create Web App
//         var factory = new WebApplicationFactory<Program>()
//             .WithWebHostBuilder(builder =>
//             {
//                 builder.ConfigureServices(_ => { });
//
//                 builder.ConfigureTestServices(services =>
//                 {
//                     var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<DataStore>));
//                     services.Remove(descriptor);
//
//                     services.AddDbContext<DataStore>(options =>
//                     {
//                         options.UseSqlServer(_databaseConnectionString!);
//                     });
//                 });
//             });
//
//         _applicationServices = factory.Services;
//
//         EnsureDatabaseCreated();
//
//         Client = factory.CreateClient();
//     }
//
//     public async Task DisposeAsync()
//     {
//         if (_dbContainer != null)
//         {
//             await _dbContainer.StopAsync();
//             await _dbContainer.DisposeAsync();
//         }
//     }
//
//     private async Task InitializeTestDatabaseContainer()
//     {
//         const string password = "letmepass!!42";
//
//         var testContainersBuilder =
//             new MsSqlBuilder()
//                 .WithPassword(password)
//                 .WithPortBinding(MsSqlBuilder.MsSqlPort, MsSqlBuilder.MsSqlPort)
//                 .WithExposedPort(MsSqlBuilder.MsSqlPort);
//
//         _dbContainer = testContainersBuilder.Build();
//
//         await _dbContainer.StartAsync();
//
//         _databaseConnectionString = $"Server=localhost,{MsSqlBuilder.MsSqlPort};Database=JosiArchitecture_Test;User Id=sa;Password={password};TrustServerCertificate=True";
//     }
//
//     private void EnsureDatabaseCreated()
//     {
//         using var scope = _applicationServices!.CreateScope();
//         var dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
//         dataStore.Database.Migrate();
//     }
// }
