using System;
using System.Text.RegularExpressions;
using Confluent.Kafka;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Extensions;

internal static class ConsumeResultExtensions
{
    private static readonly Regex TokenRegEx = new Regex(
        @"[\w]*\.{1}[\w]*.{1}[\w-]*",
        RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Multiline,
        TimeSpan.FromSeconds(2));
    private static readonly Func<string, string> RemoveToken = message => TokenRegEx.Replace(message, string.Empty);

    internal static string MessageLogPresentation(this ConsumeResult<string, string> consumeResult)
    {
        return RemoveToken(RemoveToken(consumeResult?.Message?.ToJsonString() ?? "null"));
    }

    internal static string MessageLogPresentation(this IConsumeResultWrap consumeResult)
    {
        return RemoveToken(RemoveToken(consumeResult?.MessageValue?.ToJsonString() ?? "null"));
    }

    internal static string OffsetLogPresentation(this ConsumeResult<string, string> consumeResult)
    {
        return consumeResult?.TopicPartitionOffset?.ToShortString() ?? "?offset?";
    }

    internal static string OffsetLogPresentation(this IConsumeResultWrap consumeResult)
    {
        return consumeResult?.TopicPartitionOffset.ToShortString() ?? "?offset?";
    }
}
