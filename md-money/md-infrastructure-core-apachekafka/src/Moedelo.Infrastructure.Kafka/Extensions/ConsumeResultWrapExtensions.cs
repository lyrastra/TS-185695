using System;
using Confluent.Kafka;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Extensions;

public static class ConsumeResultWrapExtensions
{
    public static ConsumeResult<string, string> ToRawConsumeResult(this IConsumeResultWrap consumeResult)
    {
        // вот такую грязь пришлось написать, более изящного архитектурного решения пока не нашлось
        if (consumeResult is ConsumeResultWrapImpl impl)
        {
            return impl.consumeResult;
        }

        throw new InvalidOperationException("Этот случай не реализован");
    }
}
