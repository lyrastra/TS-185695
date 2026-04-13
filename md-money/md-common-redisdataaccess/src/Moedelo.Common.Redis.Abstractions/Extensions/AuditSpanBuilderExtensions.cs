#nullable enable
using System.Collections.Generic;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.Common.Redis.Abstractions.Extensions;

internal static class AuditSpanBuilderExtensions
{
    internal static IAuditSpanBuilder WithConnection(
        this IAuditSpanBuilder spanBuilder,
        RedisConnection connection)
    {
        spanBuilder.WithTag("DbConnection", connection);

        return spanBuilder;
    }

    internal static IAuditSpanBuilder WithKey(
        this IAuditSpanBuilder spanBuilder,
        string key)
    {
        spanBuilder.WithTag("Key", key);

        return spanBuilder;
    }

    internal static IAuditSpanBuilder WithKeys(
        this IAuditSpanBuilder spanBuilder,
        IReadOnlyCollection<string> keys)
    {
        spanBuilder.WithTag("Keys", keys);

        return spanBuilder;
    }

    internal static IAuditSpanBuilder WithParams(
        this IAuditSpanBuilder spanBuilder,
        object parameters)
    {
        spanBuilder.WithTag("Params", parameters);

        return spanBuilder;
    }

    internal static IAuditSpanBuilder WithDistributedLock(
        this IAuditSpanBuilder spanBuilder,
        string lockKey,
        DistributedLockSettings? settings)
    {
        if (spanBuilder.IsEnabled)
        {
            spanBuilder.WithKey(lockKey);
            if (settings != null)
            {
                spanBuilder.WithTag("Settings", settings);
            }
        }

        return spanBuilder;
    }
}