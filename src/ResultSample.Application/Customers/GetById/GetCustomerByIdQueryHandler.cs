using Microsoft.EntityFrameworkCore;
using ResultSample.Abstractions.Handlers;
using ResultSample.Abstractions.Models;
using ResultSample.Domain.Customer;
using ResultSample.Infrastructure.Context;

namespace ResultSample.Application.Customers.GetById;

public sealed class GetCustomerByIdQueryHandler(ResultSampleDbContext dbContext) : IQueryHandler<GetCustomerByIdQuery>
{
    public async Task<Result> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (customer is null)
            return CustomerErrors.CustomerNotExistsError;

        var response = new GetCustomerByIdQueryResponse().Map(customer);

        return Result.Success(response);
    }
}