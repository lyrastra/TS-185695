using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Extensions
{
    internal static class AuditSpanBuilderExtensions
    {
        internal static IAuditSpanBuilder WithConnection(
            this IAuditSpanBuilder spanBuilder,
            MongoCollectionConnection connection)
        {
            spanBuilder.WithTag("DbConnection", connection);

            return spanBuilder;
        }

        internal static IAuditSpanBuilder WithConnection(
            this IAuditSpanBuilder spanBuilder,
            MongoConnection connection)
        {
            spanBuilder.WithTag("DbConnection", connection);

            return spanBuilder;
        }

        internal static IAuditSpanBuilder WithParams<T>(
            this IAuditSpanBuilder spanBuilder,
            T parameters) where T : class
        {
            spanBuilder.WithTag("Params", parameters);

            return spanBuilder;
        }
    }
}