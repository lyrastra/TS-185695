using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Movements;
using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.MovementListStorage;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances.Movements
{
    [InjectAsSingleton(typeof(IntegrationMovementListProvider))]
    public class IntegrationMovementListProvider : IMovementListProvider
    {
        private readonly ILogger logger;
        private readonly IMovementListIntegrationStorageApiClient movementListIntegrationStorageApiClient;

        public IntegrationMovementListProvider(
            ILogger<IntegrationMovementListProvider> logger,
            IMovementListIntegrationStorageApiClient movementListIntegrationStorageApiClient)
        {
            this.logger = logger;
            this.movementListIntegrationStorageApiClient = movementListIntegrationStorageApiClient;
        }
        
        public async Task<MovementList> GetByFileIdAsync(string fileId)
        {
            var movementText = await movementListIntegrationStorageApiClient.GetTextAsync(fileId, new HttpQuerySetting(TimeSpan.FromMinutes(1)));
            
            try
            {
                return Klto1CParser.Parse(movementText, Parsers.Klto1CParser.Enums.ParseOptions.NoCheckStartBalance);
            }
            catch
            {
                logger.LogError($"Movement parsing error. File path: {fileId}");
                return null;
            }
        }
    }
}
