using articuloApp.model;
using Microsoft.EntityFrameworkCore;

namespace articuloApp.data;

public class AppDbContext : DbContext
{
    public DbSet<Fabricante> Fabricantes { get; set; }
    public DbSet<Articulo> Articulos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "articulo.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>()
            .Property(a => a.Precio)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Fabricante>()
            .HasMany(f => f.Articulos)
            .WithOne(a => a.Fabricante)
            .HasForeignKey(a => a.FabricanteId);
    }
}
