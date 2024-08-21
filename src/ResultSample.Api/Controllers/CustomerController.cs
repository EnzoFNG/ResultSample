using Microsoft.AspNetCore.Mvc;
using ResultSample.Abstractions.Controllers;
using ResultSample.Abstractions.Mediator;
using ResultSample.Application.Customers.Create;
using ResultSample.Application.Customers.Delete;
using ResultSample.Application.Customers.Edit;
using ResultSample.Application.Customers.GetAll;
using ResultSample.Application.Customers.GetById;

namespace ResultSample.Api.Controllers;

[Route("api/v1/customers")]
public sealed class CustomerController(IMediatorHandler mediator) : CustomController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllCustomersQuery query)
    {
        var result = await mediator.SendQueryAsync(query);

        return CustomResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var query = new GetCustomerByIdQuery()
        {
            Id = id
        };

        var result = await mediator.SendQueryAsync(query);

        return CustomResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerCommand command)
    {
        var result = await mediator.SendCommandAsync(command);

        return CreatedAtCustomResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditAsync(Guid id, [FromBody] EditCustomerCommand command)
    {
        command.Id = id;
        var result = await mediator.SendCommandAsync(command);

        return CustomResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EditAsync(Guid id)
    {
        var command = new DeleteCustomerCommand
        {
            Id = id
        };
        var result = await mediator.SendCommandAsync(command);

        return CustomResponse(result);
    }
}