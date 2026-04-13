using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.Common.Logging.Extensions;

internal static class StringExtensions
{
    private const RegexOptions RegExOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
    private static readonly TimeSpan RegExMaxTimeout = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Название json-свойств, которые должны быть маскированы
    /// </summary>
    private static readonly string[] SensitiveJsonProperties =
        new[] { "password", "passwordmd5", "publictoken", "phone", "token", "apikey", "access_token", "executioncontexttoken" };

    /// <summary>
    /// Название cвойств, которые должны быть маскированы (не только как json-свойства)
    /// </summary>
    private static readonly string[] SensitiveGenericProperties = { "password" };

    private static readonly string[] SensitiveProperties = SensitiveJsonProperties
        .Concat(SensitiveGenericProperties)
        .Distinct()
        .ToArray();

    private static readonly KeyValuePair<Regex, string>[] SensitivePropertiesReplacements =
        EnumerateRegexOverSensitiveJsonProperty(SensitiveJsonProperties)
            .Concat(SensitiveGenericProperties.SelectMany(EnumerateRegexOverGenericSensitiveProperty))
            .ToArray();

    private static readonly string[] CustomMarkers = { @"http://", @"https://", @"mongodb://" };
    private static readonly KeyValuePair<Regex, string>[] CustomReplacements = EnumerateCustomRegex().ToArray();

    private static IEnumerable<KeyValuePair<Regex, string>> EnumerateCustomRegex()
    {
        yield return new(new Regex($@"\b(http|https|mongodb)://(\w+):(.+?)@", RegExOptions, RegExMaxTimeout),
            @"$1://***:***@");
    }

    internal static string MaskSensitiveJsonFields(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        if (SensitiveProperties.Any(propName => value.IndexOf(propName, StringComparison.OrdinalIgnoreCase) != -1))
        {
            value = SensitivePropertiesReplacements.Aggregate(value, (current, pair) =>
                pair.Key.Replace(current, pair.Value));
        }

        if (CustomMarkers.Any(marker => value.IndexOf(marker, StringComparison.OrdinalIgnoreCase) != -1))
        {
            value = CustomReplacements.Aggregate(value, (current, pair) =>
                pair.Key.Replace(current, pair.Value));
        }

        return value;
    }

    private static IEnumerable<KeyValuePair<Regex, string>> EnumerateRegexOverSensitiveJsonProperty(
        IEnumerable<string> propertyNames)
    {
        var propertiesSelector = string.Join("|", propertyNames);

        yield return new(new Regex($@"(""({propertiesSelector})""):("")(.*?)("")", RegExOptions, RegExMaxTimeout),
            @"$1:$3***$5");
        yield return new(
            new Regex($@"(\\""({propertiesSelector})\\"")(\s*:\s*\\"")(.*?)(\\"")", RegExOptions, RegExMaxTimeout),
            @"$1$3***$5");
    }

    private static IEnumerable<KeyValuePair<Regex, string>> EnumerateRegexOverGenericSensitiveProperty(
        string propertyName)
    {
        yield return new(new Regex($@"(,\s*{propertyName})=([^,]+)(,|$)", RegExOptions, RegExMaxTimeout),
            @"$1=***$3");
        yield return new(new Regex($@"(;\s*{propertyName})=([^;]+)(;|$)", RegExOptions, RegExMaxTimeout),
            @"$1=***$3");
    }
}
