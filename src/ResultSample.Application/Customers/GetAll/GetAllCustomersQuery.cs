using ResultSample.Abstractions.Queries;
using ResultSample.Domain.Customer;

namespace ResultSample.Application.Customers.GetAll;

public sealed class GetAllCustomersQuery : PaginatedQuery
{
    public string? EmailLike { get; set; }
    public string? NameLike { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }

    protected override void Validate()
    {
        if (EmailLike is not null)
            AddErrorWhen(EmailLike == string.Empty, CustomerErrors.EmailIsInvalidError);

        if (NameLike is not null)
            AddErrorWhen(NameLike == string.Empty, CustomerErrors.EmailIsInvalidError);
    }
}