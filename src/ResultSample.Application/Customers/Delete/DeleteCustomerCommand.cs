using ResultSample.Abstractions.Commands;
using ResultSample.Abstractions.Errors;
using ResultSample.Domain.Customer;

namespace ResultSample.Application.Customers.Delete;

public sealed class DeleteCustomerCommand : Command
{
    public required Guid Id { get; set; }

    protected override void Validate()
    {
        AddErrorWhen(Id == Guid.Empty, GeneralErrors.IdIsInvalidError(nameof(Customer)));
        return;
    }
}