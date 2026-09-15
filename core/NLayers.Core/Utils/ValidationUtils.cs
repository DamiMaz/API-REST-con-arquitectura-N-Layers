namespace NLayers.Core.Utils;

public static class ValidationUtils
{
    public static bool IsValidPrice(decimal price) => price > 0;
    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return email.Contains('@') && email.Contains('.');
    }
}
