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
    [InjectAsSingleton(typeof(IntegrationMovementListForUserProvider))]
    public class IntegrationMovementListForUserProvider: IMovementListProvider
    {
        private readonly ILogger logger;
        private readonly IMovementListUserStorageApiClient  storageApiClient;

        public IntegrationMovementListForUserProvider(
            ILogger<IntegrationMovementListForUserProvider> logger, 
            IMovementListUserStorageApiClient storageApiClient)
        {
            this.logger = logger;
            this.storageApiClient = storageApiClient;
        }
        
        public async Task<MovementList> GetByFileIdAsync(string fileId)
        {
            var movementText = await storageApiClient.GetTextAsync(fileId, new HttpQuerySetting(TimeSpan.FromMinutes(1)));
            
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