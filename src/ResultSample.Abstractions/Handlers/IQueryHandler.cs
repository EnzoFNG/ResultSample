using MediatR;
using ResultSample.Abstractions.Models;
using ResultSample.Abstractions.Queries;

namespace ResultSample.Abstractions.Handlers;

public interface IQueryHandler<in TQuery> :
    IRequestHandler<TQuery, Result>
    where TQuery : Query;