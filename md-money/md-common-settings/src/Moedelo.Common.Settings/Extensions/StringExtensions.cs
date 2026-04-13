namespace Moedelo.Common.Settings.Extensions;

internal static class StringExtensions
{
    internal static string RemovePrefix(this string value, string prefix)
    {
        if (value.StartsWith(prefix))
        {
            return value.Substring(prefix.Length);
        }

        return value;
    }
}
