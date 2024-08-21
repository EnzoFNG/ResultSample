namespace ResultSample.Abstractions.Models;

public record Error(string Key, string? Message = null)
{
    public static readonly Error None = new(string.Empty);
}