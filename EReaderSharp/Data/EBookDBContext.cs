using Microsoft.EntityFrameworkCore;

namespace EReaderSharp.Data;

public class EBookDbContext: DbContext
{
    public DbSet<EBook> EBook { get; set; }
    public string DbPath { get; }

    public EBookDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "elibrary.db");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
