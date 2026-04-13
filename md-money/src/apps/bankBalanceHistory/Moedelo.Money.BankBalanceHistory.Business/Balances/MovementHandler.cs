using Microsoft.Extensions.Logging;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Enums;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances.Movements;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.ConsoleUser;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events;
using Moedelo.Money.BankBalanceHistory.DataAccess.Abstractions;
using Moedelo.Money.BankBalanceHistory.Domain;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances
{
    [InjectAsSingleton(typeof(IMovementHandler))]
    public class MovementHandler : IMovementHandler
    {
        private readonly IBalancesDao balancesDao;
        private readonly ILogger<IMovementHandler> logger;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IConsoleUserGetter consoleUserGetter;
        private readonly IMovementListProviderFactory movementListProviderFactory;
        private readonly IMovementEventWriter movementEventWriter;

        public MovementHandler(
            IBalancesDao balancesDao,
            ILogger<IMovementHandler> logger,
            ISettlementAccountApiClient settlementAccountApiClient,
            IConsoleUserGetter consoleUserGetter,
            IMovementListProviderFactory movementListProviderFactory,
            IMovementEventWriter movementEventWriter)
        {
            this.balancesDao = balancesDao;
            this.logger = logger;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.consoleUserGetter = consoleUserGetter;
            this.movementListProviderFactory = movementListProviderFactory;
            this.movementEventWriter = movementEventWriter;
        }

        public async Task ProcessMovementAsync(
            int firmId,
            string fileId,
            MovementListSourceType sourceType)
        {
            if (string.IsNullOrWhiteSpace(fileId))
            {
                logger.LogWarning("Null fileId");
                return;
            }

            var movementListProvider = movementListProviderFactory.GetProvider(sourceType);
            var movementList = await movementListProvider.GetByFileIdAsync(fileId);
            if (movementList == null)
            {
                logger.LogWarning("Null movementList");
                return;
            }

            var settlementAccountId = await GetSettlementAccountIdAsync(firmId, movementList.SettlementAccount);
            if (settlementAccountId == null)
            {
                logger.LogInformation($"SettlementAccountId для счета {movementList.SettlementAccount} не найден. Ждем 10 сек...");
                await Task.Delay(TimeSpan.FromSeconds(10));
                
                settlementAccountId = await GetSettlementAccountIdAsync(firmId, movementList.SettlementAccount);
                if (settlementAccountId == null)
                {
                    logger.LogInformation($"SettlementAccountId для счета {movementList.SettlementAccount} не найден");
                    return;
                }
            }

            IReadOnlyCollection<BankBalanceUpdateRequest> requests;
            try
            {
                requests = BalanceRequestCreator.Create(settlementAccountId.Value, movementList);
                SetIsUserMovementFlag(requests, sourceType == MovementListSourceType.FromUserImport);
                
                if (movementList.Documents.Count > 0 && movementList.EndBalance.HasValue && movementList.EndBalance.Value != requests.Last().EndBalance)
                {
                    logger.LogInformation($"Не сходится Конечный остаток: settlement account id = {settlementAccountId}, date = {movementList.StartDate}, {movementList.EndBalance.Value} != {requests.Last().EndBalance} fileId: {fileId} firmId: {firmId}");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Сreation of balance request failed: {ex.Message}. FirmId = {firmId}, SettlementAccountId = {settlementAccountId} FileId = {fileId},.");
                return;
            }

            await balancesDao.UpdateAsync(firmId, requests);
            await PublishMovementProcessedEvent(firmId, settlementAccountId.Value, movementList);
        }

        private async Task<int?> GetSettlementAccountIdAsync(int firmId, string number)
        {
            var userId = await consoleUserGetter.GetConsoleUserId();
            var settlementAccountDtos = await settlementAccountApiClient.GetByNumbersAsync((FirmId)firmId, (UserId)userId,
                [number]);
            return settlementAccountDtos?.FirstOrDefault()?.Id;
        }

        private static void SetIsUserMovementFlag(IReadOnlyCollection<BankBalanceUpdateRequest> requests, bool isUserMovement)
        {
            foreach (var request in requests)
                request.IsUserMovement = isUserMovement;
        }

        private async Task PublishMovementProcessedEvent(int firmId, int settlementAccountId, Parsers.Klto1CParser.Models.Klto1CParser.MovementList movementList)
        {
            var processedEvent = new MovementProcessedEvent
            {
                FirmId = firmId,
                SettlementAccountId = settlementAccountId,
                StartDate = movementList.StartDate.Date,
                EndDate = movementList.EndDate.Date,
            };

            await movementEventWriter.WriteProcessedEventAsync(processedEvent);
        }
    }
}
