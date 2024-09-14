using Microsoft.EntityFrameworkCore;

public class BlockListContext : Microsoft.EntityFrameworkCore.DbContext
{
     public BlockListContext (DbContextOptions<BlockListContext> options)
            : base(options)
        {
        }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=postgresdb;Username=postgres;Password=postgres");

    public DbSet<BlockedUser> BlockedUsers { get; set; }
}
