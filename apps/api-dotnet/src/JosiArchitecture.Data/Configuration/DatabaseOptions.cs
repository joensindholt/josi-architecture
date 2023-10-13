// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace JosiArchitecture.Data.Configuration;

public class DatabaseOptions
{
    public DatabaseProvider? Provider { get; set; }

    public bool? UseSingularTableNames { get; set; }

    public string? Schema { get; set; }

    public enum DatabaseProvider
    {
        SqlServer,
        Postgres
    }
}

