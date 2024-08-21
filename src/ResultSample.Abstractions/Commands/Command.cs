using ResultSample.Abstractions.Models;

namespace ResultSample.Abstractions.Commands;

public abstract class Command : ErrorObject, ICommand;