using ResultSample.Abstractions.Commands;

namespace ResultSample.Application.Customers.Create;

public sealed class CreateCustomerCommand : Command
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int Age { get; set; }

    protected override void Validate() 
    {
        return;
    }
}