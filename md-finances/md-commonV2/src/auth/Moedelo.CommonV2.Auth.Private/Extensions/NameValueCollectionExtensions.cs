using System.Collections.Specialized;

namespace Moedelo.CommonV2.Auth.Private.Extensions;

internal static class NameValueCollectionExtensions
{
    internal static int GetIntValue(this NameValueCollection parameters, string paramName, int defaultValue)
    {
        var paramValue = parameters[paramName];

        return string.IsNullOrEmpty(paramValue)
            ? defaultValue
            : int.TryParse(parameters[paramName], out var value)
                ? value
                : defaultValue;
    }
}