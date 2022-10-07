namespace EReaderSharp.Data;
using Microsoft.EntityFrameworkCore;

public class EBookDbContext: DbContext
{
    public DbSet<EBook> EBook { get; set; }

    public EBookDbContext(DbContextOptions<EBookDbContext> options): base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
