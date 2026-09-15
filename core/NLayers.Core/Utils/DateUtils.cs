namespace NLayers.Core.Utils;

public static class DateUtils
{
    public static DateTime UtcNow => DateTime.UtcNow;
    public static string ToIsoString(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm:ssZ");
}
