namespace MyApp.Modules.Shared.Extensions;

public static class StringExtensions
{
    public static string ToPascalCase(this string input)
    {
        return input.Substring(0, 1).ToUpperInvariant() + input.Substring(1);
    }
}
