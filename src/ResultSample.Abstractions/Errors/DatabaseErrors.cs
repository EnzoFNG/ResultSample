using ResultSample.Abstractions.Models;

namespace ResultSample.Abstractions.Errors;

public static class DatabaseErrors
{
    public static Error EntityNotSavedError(string entity)
    {
        return new Error(entity, $"An error was ocurred while saving the {entity}.");
    }

    public static Error EntityNotUpdatedError(string entity)
    {
        return new Error(entity, $"An error was ocurred while update the {entity}.");
    }

    public static Error EntityNotDeletedError(string entity)
    {
        return new Error(entity, $"An error was ocurred while deleting the {entity}.");
    }
}