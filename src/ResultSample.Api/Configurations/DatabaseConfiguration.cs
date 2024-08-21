using Bogus;
using Microsoft.EntityFrameworkCore;
using ResultSample.Domain.Customer;
using ResultSample.Infrastructure.Context;

namespace ResultSample.Api.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<ResultSampleDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb")
            .EnableSensitiveDataLogging();
        });

        services.AddScoped<ResultSampleDbContext>();

        services.SeedData();

        return services;
    }

    private static void SeedData(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<ResultSampleDbContext>();

        var customerExists = dbContext.Customers.Any();

        if (customerExists)
            return;

        var faker = new Faker<Customer>()
            .CustomInstantiator(f =>
            {
                var randomAge = DateTime.Now.Year - f.Person.DateOfBirth.Year;

                var customer = Customer.Create(f.Internet.Email(), f.Person.FullName, randomAge);

                return customer.Response!;
            });

        var customers = faker.Generate(100);

        dbContext.Customers.AddRange(customers);
        dbContext.SaveChanges();
    }
}