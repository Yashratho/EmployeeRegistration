using EmployeeRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistration.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee_Mst> Employees { get; set; }
    public DbSet<Country_Mst> Countries { get; set; }
    public DbSet<State_Mst> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee_Mst>()
            .HasOne(e => e.State)
            .WithMany()
            .HasForeignKey(e => e.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee_Mst>()
            .HasOne(e => e.Country)
            .WithMany()
            .HasForeignKey(e => e.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<State_Mst>()
            .HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Country_Mst>().HasData(
            new Country_Mst { CountryId = 1, CountryName = "India" },
            new Country_Mst { CountryId = 2, CountryName = "USA" }
        );

        modelBuilder.Entity<State_Mst>().HasData(
            new State_Mst { StateId = 1, StateName = "Maharashtra", CountryId = 1 },
            new State_Mst { StateId = 2, StateName = "Karnataka", CountryId = 1 },
            new State_Mst { StateId = 3, StateName = "California", CountryId = 2 },
            new State_Mst { StateId = 4, StateName = "Texas", CountryId = 2 }
        );
    }
}
