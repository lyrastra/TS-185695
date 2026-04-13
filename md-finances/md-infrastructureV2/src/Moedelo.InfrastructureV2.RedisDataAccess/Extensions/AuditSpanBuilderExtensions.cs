using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.RedisDataAccess.Common;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Extensions
{
    internal static class AuditSpanBuilderExtensions
    {
        internal static IAuditSpanBuilder WithConnection(
            this IAuditSpanBuilder spanBuilder,
            AuditedRedisConnection connection)
        {
            spanBuilder.WithTag("DbConnection", connection.ObscuredConnectionInfo);

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

        internal static IAuditSpanBuilder WithParams<T>(
            this IAuditSpanBuilder spanBuilder,
            T parameters)
        {
            spanBuilder.WithTag("Params", parameters);

            return spanBuilder;
        }
    }
}