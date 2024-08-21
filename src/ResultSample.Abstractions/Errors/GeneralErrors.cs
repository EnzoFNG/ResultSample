using ResultSample.Abstractions.Models;

namespace ResultSample.Abstractions.Errors;

public static class GeneralErrors
{
    public static Error IdIsInvalidError(string entity)
    {
        return new Error(entity, $"Id is invalid.");
    }
}