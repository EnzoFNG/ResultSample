using Microsoft.EntityFrameworkCore;
using ResultSample.Abstractions.Errors;
using ResultSample.Abstractions.Handlers;
using ResultSample.Abstractions.Models;
using ResultSample.Domain.Customer;
using ResultSample.Infrastructure.Context;

namespace ResultSample.Application.Customers.Edit;

public sealed class EditCustomerCommandHandler(ResultSampleDbContext dbContext) : ICommandHandler<EditCustomerCommand>
{
    public async Task<Result> Handle(EditCustomerCommand command, CancellationToken cancellationToken)
    {
        var existentCustomer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (existentCustomer is null)
            return CustomerErrors.CustomerNotExistsError;

        var customerExists = await dbContext.Customers.FirstOrDefaultAsync(x =>
            x.Email == command.Email
            && x.Id != command.Id,
            cancellationToken);

        if (customerExists is not null)
            return CustomerErrors.CustomerAlreadyExistsError;

        var result = existentCustomer.Edit(command.Email, command.Name, command.Age);

        var response = await result.Match<Customer>(
            onSuccessAsync: async customer =>
            {
                dbContext.Customers.Update(customer);
                var success = await dbContext.SaveChangesAsync(cancellationToken) > 0;

                if (!success)
                    return DatabaseErrors.EntityNotUpdatedError(nameof(Customer));

                var commandResponse = new EditCustomerCommandResponse().Map(customer);
                return Result.Success(commandResponse);
            },
            onFail: Result.Failure);

        return response;
    }
}