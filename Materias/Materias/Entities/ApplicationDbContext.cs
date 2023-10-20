using Microsoft.EntityFrameworkCore;

namespace Materias.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Materia>()
            .HasOne(m => m.Requisito)
            .WithMany()
            .HasForeignKey(m => m.RequisitoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
    
    public DbSet<Materia> Materias { get; set; }
}