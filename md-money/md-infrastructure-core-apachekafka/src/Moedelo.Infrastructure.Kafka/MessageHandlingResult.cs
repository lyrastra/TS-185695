using System;
using System.Diagnostics;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka;

internal abstract record MessageHandlingResultBase(
    IConsumeResultWrap ConsumeResult,
    bool Success,
    Exception? Exception,
    TimeSpan Duration);

internal sealed record MessageHandlingResult<TMessage>(
    IConsumeResultWrap ConsumeResult,
    bool Success,
    TMessage? Message,
    Exception? Exception,
    TimeSpan Duration) : MessageHandlingResultBase(ConsumeResult, Success, Exception, Duration)
{
    internal static MessageHandlingResult<TMessage> CreateSuccess(
        IConsumeResultWrap consumeResult, TMessage message, Stopwatch watch)
        => new(consumeResult, true, message, null, Duration: TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds));
        
    internal static MessageHandlingResult<TMessage> CreateFailed(
        IConsumeResultWrap consumeResult, TMessage? message, Exception exception, Stopwatch watch)
        => new(consumeResult, false, message, exception, Duration: TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds));
}