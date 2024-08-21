using Microsoft.EntityFrameworkCore;
using ResultSample.Abstractions.Errors;
using ResultSample.Abstractions.Handlers;
using ResultSample.Abstractions.Models;
using ResultSample.Domain.Customer;
using ResultSample.Infrastructure.Context;

namespace ResultSample.Application.Customers.Delete;

public sealed class DeleteCustomerCommandHandler(ResultSampleDbContext dbContext) : ICommandHandler<DeleteCustomerCommand>
{
    public async Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customerExists = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (customerExists is null)
            return CustomerErrors.CustomerNotExistsError;

        dbContext.Customers.Remove(customerExists);
        var success = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        if (!success)
            return DatabaseErrors.EntityNotDeletedError(nameof(Customer));

        return Result.Success("The customer was deleted successfully!");
    }
}