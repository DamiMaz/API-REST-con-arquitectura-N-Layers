namespace NLayers.Core.Utils;

public static class StringUtils
{
    public static bool IsNullOrEmpty(string? value) => string.IsNullOrWhiteSpace(value);
    public static string Capitalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        return char.ToUpper(value[0]) + (value.Length > 1 ? value[1..] : string.Empty);
    }
}
