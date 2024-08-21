using ResultSample.Abstractions.Models;
using ResultSample.Domain.Customer;

namespace ResultSample.Application.Customers.GetById;

public sealed record GetCustomerByIdQueryResponse : BaseResponse<GetCustomerByIdQueryResponse, Customer>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }

    public override GetCustomerByIdQueryResponse Map(Customer customer)
    {
        Id = customer.Id;
        Name = customer.Name;
        Email = customer.Email;
        Age = customer.Age;

        return this;
    }
}