using Microsoft.EntityFrameworkCore;
using ResultSample.Abstractions.Errors;
using ResultSample.Abstractions.Handlers;
using ResultSample.Abstractions.Models;
using ResultSample.Domain.Customer;
using ResultSample.Infrastructure.Context;

namespace ResultSample.Application.Customers.Create;

public sealed class CreateCustomerCommandHandler(ResultSampleDbContext dbContext) : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customerExists = await dbContext.Customers.AnyAsync(x => x.Email == command.Email, cancellationToken);

        if (customerExists)
            return CustomerErrors.CustomerAlreadyExistsError;

        var result = Customer.Create(command.Email, command.Name, command.Age);

        var response = await result.Match<Customer>(
            onSuccessAsync: async customer =>
            {
                await dbContext.Customers.AddAsync(customer, cancellationToken);
                var success = await dbContext.SaveChangesAsync(cancellationToken) > 0;

                if (!success)
                    return DatabaseErrors.EntityNotSavedError(nameof(Customer));

                var commandResponse = new CreateCustomerCommandResponse().Map(customer);
                return Result.Success(commandResponse);
            },
            onFail: Result.Failure);

        return response;
    }
}