using ResultSample.Abstractions.Commands;
using ResultSample.Abstractions.Models;
using ResultSample.Abstractions.Queries;

namespace ResultSample.Abstractions.Mediator;

public interface IMediatorHandler
{
    Task<Result> SendCommandAsync<TCommand>(TCommand command) where TCommand : Command;
    Task<Result> SendQueryAsync<TQuery>(TQuery query) where TQuery : Query;
}