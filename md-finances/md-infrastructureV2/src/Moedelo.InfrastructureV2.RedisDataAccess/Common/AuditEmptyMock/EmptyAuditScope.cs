using System;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common.AuditEmptyMock
{
    internal class EmptyAuditScope : IAuditScope
    {
        private static readonly Lazy<EmptyAuditScope> lazy =
            new Lazy<EmptyAuditScope>(() => new EmptyAuditScope());

        private EmptyAuditScope()
        {
        }

        public static EmptyAuditScope Instance => lazy.Value;

        public IAuditSpan Span { get; } = EmptyAuditSpan.Instance;

        public bool IsEnabled => false;

        public void Dispose()
        {
        }
    }
}