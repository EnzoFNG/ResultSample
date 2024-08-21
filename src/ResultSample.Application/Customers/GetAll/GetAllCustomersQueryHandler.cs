using Microsoft.EntityFrameworkCore;
using ResultSample.Abstractions.Extensions;
using ResultSample.Abstractions.Handlers;
using ResultSample.Abstractions.Models;
using ResultSample.Infrastructure.Context;

namespace ResultSample.Application.Customers.GetAll;

public sealed class GetAllCustomersQueryHandler(ResultSampleDbContext dbContext) : IQueryHandler<GetAllCustomersQuery>
{
    public async Task<Result> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        var customers = await dbContext.Customers
            .When(query.NameLike.IsNotEmpty(), x => EF.Functions.Like(x.Name, $"%{query.NameLike}%"))
            .When(query.EmailLike.IsNotEmpty(), x => EF.Functions.Like(x.Email, $"%{query.EmailLike}%"))
            .When(query.MinAge is not null, x => x.Age >= query.MinAge)
            .When(query.MaxAge is not null, x => x.Age <= query.MaxAge)
            .Skip(query.Skip)
            .Take(query.Size)
            .Select(x => new GetAllCustomersQueryItemResponse().Map(x))
            .ToListAsync(cancellationToken);

        var count = await dbContext.Customers.LongCountAsync(cancellationToken);

        var response = new GetAllCustomersQueryResponse
        {
            TotalItems = count,
            Page = query.Page,
            PageSize = customers.Count,
            Data = customers
        };

        return Result.Success(response);
    }
}