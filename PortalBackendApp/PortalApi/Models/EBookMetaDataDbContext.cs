using Microsoft.EntityFrameworkCore;
namespace PortalApi.Models;

public class EBookMetaDataDbContext: DbContext
{
    public DbSet<EBookMetaData> EBookMetaData { get; set; }

    public EBookMetaDataDbContext(DbContextOptions<EBookMetaDataDbContext> options): base(options)
    { }
}
