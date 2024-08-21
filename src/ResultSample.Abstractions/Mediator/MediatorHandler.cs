using MediatR;
using ResultSample.Abstractions.Commands;
using ResultSample.Abstractions.Models;
using ResultSample.Abstractions.Queries;

namespace ResultSample.Abstractions.Mediator;

public sealed class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    public async Task<Result> SendCommandAsync<TCommand>(TCommand command)
        where TCommand : Command
    {
        command.Validate();

        if (!command.IsValid)
            return Result.Failure([.. command.Errors!]);

        return await mediator.Send(command);
    }

    public async Task<Result> SendQueryAsync<TQuery>(TQuery query)
        where TQuery : Query
    {
        query.Validate();

        if (!query.IsValid)
            return Result.Failure([.. query.Errors!]);

        return await mediator.Send(query);
    }
}