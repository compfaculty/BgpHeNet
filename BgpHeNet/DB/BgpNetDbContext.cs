using BgpHeNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BgpHeNet;

public class BgpNetDbContext : DbContext
{
    private const string ConnectionString = "Data Source=db.sqlite;";
    public DbSet<ASN> ASNs { get; set; } = null!;
    
    public DbSet<Prefix> Prefixes { get; set; } = null!;

    public DbSet<IP> IPs { get; set; } = null!;

    // public DbSet<PTR> PTRs { get; set; }
    public DbSet<Url> URLs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }
}