using System.Security.Cryptography;

namespace NLayers.Core.Helpers;

public static class TokenHelper
{
    public static string GenerateRandomToken(int length = 32)
    {
        var randomBytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToHexString(randomBytes);
    }
}
