namespace ResultSample.Abstractions.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string? text)
        => string.IsNullOrWhiteSpace(text);

    public static bool IsNotEmpty(this string? text)
        => !string.IsNullOrWhiteSpace(text);
}