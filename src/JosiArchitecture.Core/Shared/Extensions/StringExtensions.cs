namespace JosiArchitecture.Core.Shared.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string input)
    {
        if (input.Length < 1)
        {
            return input;
        }

        return input![0].ToString().ToUpper() + input[1..];
    }

    public static string CamelCase(this string input)
    {
        if (input.Length < 1)
        {
            return input;
        }

        return input![0].ToString().ToLower() + input[1..];
    }
}