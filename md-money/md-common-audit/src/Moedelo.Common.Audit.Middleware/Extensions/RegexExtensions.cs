using System.Text.RegularExpressions;

namespace Moedelo.Common.Audit.Middleware.Extensions;

internal static class RegexExtensions
{
    internal static bool IsAnyMatched(this Regex[] regexList, string path)
    {
        // перебираем без LINQ
        foreach (var regex in regexList)
        {
            if (regex.IsMatch(path))
            {
                return true;
            }
        }

        return false;
    }
}
