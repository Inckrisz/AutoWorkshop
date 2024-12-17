
using AutoWorkshopApi.Models;
using Microsoft.EntityFrameworkCore;

public class AutoWorkshopContext : DbContext
{
    public AutoWorkshopContext(DbContextOptions<AutoWorkshopContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Job> Jobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kapcsolat beállítása
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Client)  
            .WithMany(c => c.Jobs)  
            .HasForeignKey(j => j.ClientId)  
            .OnDelete(DeleteBehavior.Cascade);  
    }
}
