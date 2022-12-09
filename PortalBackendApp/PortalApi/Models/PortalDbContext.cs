using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace PortalApi.Models;

public class PortalDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<EBook> EBooks { get; set; }
    public DbSet<Annotation> Annotations { get; set; }
    public DbSet<Collection> Collections { get; set; }

    public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options)
    { }
}
