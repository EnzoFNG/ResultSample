using ResultSample.Abstractions.Errors;
using ResultSample.Abstractions.Queries;
using ResultSample.Domain.Customer;

namespace ResultSample.Application.Customers.GetById;

public sealed class GetCustomerByIdQuery : Query
{
    public required Guid Id { get; set; }

    protected override void Validate()
    {
        AddErrorWhen(Id == Guid.Empty, GeneralErrors.IdIsInvalidError(nameof(Customer)));
    }
}