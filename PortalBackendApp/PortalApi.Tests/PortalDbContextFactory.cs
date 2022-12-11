using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace PortalApi.Tests;

public class PortalDbContextFactory : IDisposable
{
    private SqliteConnection? _connection;

    private DbContextOptions<PortalDbContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<PortalDbContext>().UseSqlite(_connection).Options;
    }

    public PortalDbContext CreateContext()
    {
        if (_connection == null)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = CreateOptions();
            using (var context = new PortalDbContext(options))
            {
                context.Database.EnsureCreated();
            }
        }

        return new PortalDbContext(CreateOptions());
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}
