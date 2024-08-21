using ResultSample.Abstractions.Commands;
using ResultSample.Abstractions.Errors;
using ResultSample.Domain.Customer;
using System.Text.Json.Serialization;

namespace ResultSample.Application.Customers.Edit;

public sealed class EditCustomerCommand : Command
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }

    protected override void Validate()
    {
        AddErrorWhen(Id == Guid.Empty, GeneralErrors.IdIsInvalidError(nameof(Customer)));
    }
}