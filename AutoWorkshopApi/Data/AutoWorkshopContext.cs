
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
            .HasOne(j => j.Client)  // Kapcsolódik a Client entitáshoz
            .WithMany(c => c.Jobs)  // Több Job tartozhat egy Clienthez
            .HasForeignKey(j => j.ClientId)  // A Foreign Key a Job osztályban van
            .OnDelete(DeleteBehavior.Cascade);  // Cascade törlés (opcionális)
    }
}
