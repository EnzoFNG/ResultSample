using Microsoft.EntityFrameworkCore;
using ResultSample.Domain.Customer;

namespace ResultSample.Infrastructure.Context;

public sealed class ResultSampleDbContext(DbContextOptions<ResultSampleDbContext> options)
    : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<Customer>()
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Customer>()
          .Property(x => x.Age)
          .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}