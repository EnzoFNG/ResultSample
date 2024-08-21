using MediatR;
using ResultSample.Abstractions.Models;

namespace ResultSample.Abstractions.Queries;

public interface IQuery : IRequest<Result>, IBaseRequest;