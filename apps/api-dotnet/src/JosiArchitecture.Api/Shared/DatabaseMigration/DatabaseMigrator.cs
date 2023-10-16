using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using JosiArchitecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Serilog;

namespace JosiArchitecture.Api.Shared.DatabaseMigration;

public class DatabaseMigrator : IHostedService
{
    private readonly DataStore _dataStore;
    private readonly ILogger<DatabaseMigrator> _logger;

    public DatabaseMigrator(IServiceProvider services)
    {
        var scope = services.CreateScope();
        _dataStore = scope.ServiceProvider.GetRequiredService<DataStore>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseMigrator>>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await MigrateDatabaseAsync(cancellationToken);
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

    private async Task MigrateDatabaseAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            try
            {
                await _dataStore.Database.MigrateAsync(cancellationToken);
                return;
            }
            catch (Exception ex)
            {
                if (ex is SocketException || ex is NpgsqlException)
                {
                    _logger.LogInformation($"Waiting for database to be available");
                    await Task.Delay(500, cancellationToken);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
