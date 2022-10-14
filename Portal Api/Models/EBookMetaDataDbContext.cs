namespace Portal_Api.Models;
using Microsoft.EntityFrameworkCore;

public class EBookMetaDataDbContext: DbContext
{
    public DbSet<EBookMetaData> EBookMetaData { get; set; }

    public EBookMetaDataDbContext(DbContextOptions<EBookMetaDataDbContext> options): base(options)
    { }
}
