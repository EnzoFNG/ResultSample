using MediatR;
using ResultSample.Abstractions.Models;

namespace ResultSample.Abstractions.Commands;

public interface ICommand : IRequest<Result>, IBaseRequest
{ }