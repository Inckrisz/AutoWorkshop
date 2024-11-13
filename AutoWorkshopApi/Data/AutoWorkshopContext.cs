using AutoWorkshopApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AutoWorkshopContext : DbContext
{
    public AutoWorkshopContext(DbContextOptions<AutoWorkshopContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Job> Jobs { get; set; }
}
