using System;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JosiArchitecture.Api.Shared.DatabaseMigration;

public class DatabaseMigrator : IHostedService
{
    private readonly DataStore _dataStore;

    public DatabaseMigrator(IServiceProvider services)
    {
        var scope = services.CreateScope();
        _dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            MigrateDatabase();
        }
        catch (Exception)
        {
            await Log.CloseAndFlushAsync();
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void MigrateDatabase()
    {
        _dataStore.Database.Migrate();
    }
}
