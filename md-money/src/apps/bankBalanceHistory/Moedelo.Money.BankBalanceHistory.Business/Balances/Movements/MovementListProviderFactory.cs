using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Enums;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Movements;
using System;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances.Movements
{
    [InjectAsSingleton(typeof(IMovementListProviderFactory))]
    internal class MovementListProviderFactory : IMovementListProviderFactory
    {
        private readonly IntegrationMovementListProvider integrationMovementListProvider;
        private readonly IntegrationReconcilationMovementListTempFileProvider integrationReconcilationMovementListTempFileProvider;
        private readonly IntegrationMovementListForUserProvider integrationMovementListForUserProvider;

        public MovementListProviderFactory(
            IntegrationMovementListProvider integrationMovementListProvider,
            IntegrationReconcilationMovementListTempFileProvider integrationReconcilationMovementListTempFileProvider, 
            IntegrationMovementListForUserProvider integrationMovementListForUserProvider)
        {
            this.integrationMovementListProvider = integrationMovementListProvider;
            this.integrationReconcilationMovementListTempFileProvider = integrationReconcilationMovementListTempFileProvider;
            this.integrationMovementListForUserProvider = integrationMovementListForUserProvider;
        }

        public IMovementListProvider GetProvider(MovementListSourceType type)
        {
            return type switch
            {
                MovementListSourceType.FromIntegration => integrationMovementListProvider,
                MovementListSourceType.FromIntegrationReconcilationTempFile => integrationReconcilationMovementListTempFileProvider,
                MovementListSourceType.FromUserImport => integrationMovementListForUserProvider,
                _ => throw new ArgumentOutOfRangeException($"Unkown provider type: {type}")
            };
        }
    }
}
