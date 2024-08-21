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

        var response = Result.Success;
        await result.Match(
            onSuccess: async customer =>
            {
                dbContext.Customers.Update(customer);
                var success = await dbContext.SaveChangesAsync(cancellationToken) > 0;

                if (!success)
                {
                    response = DatabaseErrors.EntityNotUpdatedError(nameof(Customer));
                    return;
                }

                var commandResponse = new EditCustomerCommandResponse().Map(customer);
                response = Result<EditCustomerCommandResponse>.Success(commandResponse);
            },
            onFail: errors =>
            {
                response = Result.Failure(errors);
                return Task.CompletedTask;
            });

        return response;
    }
}