using ResultSample.Abstractions.Models;
using ResultSample.Domain.Customer;

namespace ResultSample.Application.Customers.GetAll;

public sealed class GetAllCustomersQueryResponse
{
    public long TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<GetAllCustomersQueryItemResponse> Data { get; set; } = [];
}

public sealed record GetAllCustomersQueryItemResponse : BaseResponse<GetAllCustomersQueryItemResponse, Customer>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }

    public override GetAllCustomersQueryItemResponse Map(Customer customer)
    {
        Id = customer.Id;
        Name = customer.Name;
        Email = customer.Email;
        Age = customer.Age;

        return this;
    }
}