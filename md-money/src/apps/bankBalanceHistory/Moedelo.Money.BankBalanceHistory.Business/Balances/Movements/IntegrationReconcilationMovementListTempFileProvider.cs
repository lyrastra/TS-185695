using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Movements;
using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Reconciliation;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances.Movements
{
    [InjectAsSingleton(typeof(IntegrationReconcilationMovementListTempFileProvider))]
    public class IntegrationReconcilationMovementListTempFileProvider : IMovementListProvider
    {
        private readonly ILogger logger;
        private readonly IReconciliationTempFileApiClient reconciliationTempFileApiClient;

        public IntegrationReconcilationMovementListTempFileProvider(
            ILogger<IntegrationReconcilationMovementListTempFileProvider> logger,
            IReconciliationTempFileApiClient reconciliationTempFileApiClient)
        {
            this.logger = logger;
            this.reconciliationTempFileApiClient = reconciliationTempFileApiClient;
        }
        
        public async Task<MovementList> GetByFileIdAsync(string fileId)
        {
            var movementText = await reconciliationTempFileApiClient.GetTextAsync(fileId, new HttpQuerySetting(TimeSpan.FromMinutes(1)));
            
            try
            {
                return Klto1CParser.Parse(movementText, Parsers.Klto1CParser.Enums.ParseOptions.NoCheckStartBalance);
            }
            catch
            {
                logger.LogError($"Movement revise parsing error. File path: {fileId}");
                return null;
            }
        }
    }
}
