using MediatR;
using ResultSample.Abstractions.Commands;
using ResultSample.Abstractions.Models;

namespace ResultSample.Abstractions.Handlers;

public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand, Result>
    where TCommand : Command;