#nullable enable
using System;
using System.Text.RegularExpressions;

namespace Moedelo.Common.Audit.Extensions;

internal sealed class ReplacingRegex
{
    public ReplacingRegex(string RegexPattern, string replacement)
    {
        regex = new Regex(RegexPattern, RegexOptions.Compiled, MaxTimeout);
        matchEvaluator = null;
        this.replacement = replacement;
    }

    public ReplacingRegex(string RegexPattern, MatchEvaluator matchEvaluator)
    {
        regex = new Regex(RegexPattern, RegexOptions.Compiled, MaxTimeout);
        this.matchEvaluator = matchEvaluator;
        replacement = null;
    }

    private static readonly TimeSpan MaxTimeout = TimeSpan.FromSeconds(2);

    private readonly Regex regex;
    private readonly MatchEvaluator? matchEvaluator;
    private readonly string? replacement;

    internal string Replace(string value) => matchEvaluator != null
        ? regex.Replace(value, matchEvaluator)
        : regex.Replace(value, replacement!);
};
