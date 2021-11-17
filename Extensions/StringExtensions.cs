#pragma warning disable CA1050 // Declare types in namespaces
using System.Diagnostics.CodeAnalysis;

public static class StringExtensions
#pragma warning restore CA1050 // Declare types in namespaces
{
    public static bool IsNullOrWhitespace(
        [NotNullWhen(false)] this string? str
    )
    {
        return string.IsNullOrWhiteSpace(
            str
        );
    }

    public static bool IsNotNullOrWhitespace(
        [NotNullWhen(true)] this string? str
    )
    {
        return !string.IsNullOrWhiteSpace(
            str
        );
    }

}
