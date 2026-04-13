using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.Business.SettlementAccounts;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Domain.SettlementAccounts;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.PaymentImport.Client;
using Moedelo.PaymentImport.Client.MovementList.Storage;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.Finances.Business.Services.Money.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationService : IReconciliationService
    {
        private const string TAG = nameof(ReconciliationService);

        private readonly ILogger logger;
        private readonly IBalanceReconcilationDao balanceReconcilationDao;
        private readonly IMoneyOperationRemover moneyOperationRemover;
        private readonly ISettlementAccountsReader settlementAccountsReader;
        private readonly IMovementListReconciliationStorageClient movementListReconciliationStorageClient;
        private readonly IPaymentImportQueueClient importQueueClient;

        public ReconciliationService(
            ILogger logger,
            IBalanceReconcilationDao balanceReconcilationDao,
            IMoneyOperationRemover moneyOperationRemover,
            ISettlementAccountsReader settlementAccountsReader,
            IMovementListReconciliationStorageClient movementListReconciliationStorageClient,
            IPaymentImportQueueClient importQueueClient)
        {
            this.logger = logger;
            this.balanceReconcilationDao = balanceReconcilationDao;
            this.moneyOperationRemover = moneyOperationRemover;
            this.settlementAccountsReader = settlementAccountsReader;
            this.movementListReconciliationStorageClient = movementListReconciliationStorageClient;
            this.importQueueClient = importQueueClient;
        }

        public Task<ReconciliationResult> GetLastInfoAsync(int firmId, int settlementAccountId)
        {
            return balanceReconcilationDao.GetLastAsync(firmId, settlementAccountId);
        }

        public async Task<ReconciliationResult[]> GetLastWithDiffInfoAsync(int firmId, ReconciliationStatus reconciliationStatus, IReadOnlyCollection<int> settlementAccountIds)
        {
            var reconcilations = await balanceReconcilationDao.GetLastBySettlementAccountIdsAsync(firmId, reconciliationStatus, settlementAccountIds)
                .ConfigureAwait(false);

            return reconcilations.Where(r => (r.ExcessOperations != null && r.ExcessOperations.Any()) || (r.MissingOperations != null && r.MissingOperations.Any())).ToArray();
        }

        public async Task<ReconciliationBusinessModel> GetLastAsync(IUserContext userContext, int? settlementAccountId)
        {
            var reconciliation = await balanceReconcilationDao
                .GetLastAsync(userContext.FirmId, settlementAccountId)
                .ConfigureAwait(false);
            if(reconciliation == null)
            {
                return null;
            }
            var settlementAccount = await settlementAccountsReader.GetSettlementAccount(userContext.FirmId, userContext.UserId, reconciliation.SettlementAccountId)
                .ConfigureAwait(false);
            if(settlementAccount == null)
            {
                return null;
            }
            return new ReconciliationBusinessModel
            {
                ReconciliationResult = reconciliation,
                SettlementAccount = settlementAccount
            };
        }

        public async Task<ReconciliationBusinessModel> GetBySessionIdAsync(IUserContext userContext, Guid sessionId)
        {
            var reconciliation = await balanceReconcilationDao
                .GetBySessionIdAsync(userContext.FirmId, sessionId)
                .ConfigureAwait(false);

            var settlementAccount = await settlementAccountsReader.GetSettlementAccount(userContext.FirmId, userContext.UserId, reconciliation.SettlementAccountId)
                .ConfigureAwait(false);

            if (settlementAccount == null)
            {
                return null;
            }
            return new ReconciliationBusinessModel
            {
                ReconciliationResult = reconciliation,
                SettlementAccount = settlementAccount
            };
        }

        public async Task CompleteAsync(IUserContext userContext, Guid sessionId, IReadOnlyCollection<long> excessOperations, IReadOnlyCollection<long> missingOperations)
        {
            try
            {
                var reconciliation = await balanceReconcilationDao.GetBySessionIdAsync(userContext.FirmId, sessionId).ConfigureAwait(false);
                if (reconciliation.Status != ReconciliationStatus.Ready)
                {
                    logger.Error(TAG, $"Unable to complete reconcilation with sessionId - {sessionId} and status {reconciliation.Status}{(int)reconciliation.Status}", context: userContext.GetAuditContext());
                    return;
                }

                if (excessOperations.Any())
                {
                    await moneyOperationRemover.DeleteAsync(userContext.FirmId, userContext.UserId, excessOperations.ToList()).ConfigureAwait(false);
                }

                var documentsForImport = new List<string>();
                foreach (var documentId in missingOperations)
                {
                    var document = reconciliation.MissingOperations.FirstOrDefault(x => x.Id == documentId);
                    if (document != null)
                    {
                        documentsForImport.Add(document.DocumentSection);
                    }
                }

                if (documentsForImport.Count == 0)
                {
                    await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, sessionId, ReconciliationStatus.Completed).ConfigureAwait(false);
                }

                logger.Info(TAG, $"Reconcilation import start. documents Count={documentsForImport.Count}. sessionId - {sessionId}", userContext.GetAuditContext(), sessionId);

                var settlementAccount = await settlementAccountsReader.GetSettlementAccount(userContext.FirmId, userContext.UserId, reconciliation.SettlementAccountId)
                    .ConfigureAwait(false);

                var mongoId = await SaveToMongoAsync(userContext, sessionId, settlementAccount, reconciliation.CreateDate, documentsForImport);

                // поставить сверку в очередь импорта
                await importQueueClient.AppendReconcilationEventAsync(new ReconciliationEventAppendRequestDto
                {
                    FirmId = userContext.FirmId,
                    MongoObjectId = mongoId,
                    SettlementAccountId = settlementAccount.Id,
                    ReconcilationSessionId = sessionId,
                    ReconcilationDate = reconciliation.ReconcilationDate
                }).ConfigureAwait(false);

                logger.Info(TAG, $"Reconcilation import set to queue. sessionId - {sessionId}", userContext.GetAuditContext(), sessionId);

                await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, sessionId, ReconciliationStatus.Completed).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, sessionId, ReconciliationStatus.Error).ConfigureAwait(false);
                logger.Error(TAG, $"Reconcilation failed with sessionId - {sessionId}", e, userContext.GetAuditContext(), sessionId);
                throw;
            }
        }

        public async Task CancelAsync(int firmId, Guid sessionId)
        {
            var result = await balanceReconcilationDao.GetBySessionIdAsync(firmId, sessionId).ConfigureAwait(false);
            if (result.Status != ReconciliationStatus.Ready)
            {
                return;
            }
            await balanceReconcilationDao.SetStatusAsync(firmId, sessionId, ReconciliationStatus.Completed).ConfigureAwait(false);
        }

        public Task<bool> IsAnyInProgressAsync(int firmId, int settlementAccountId)
        {
            return balanceReconcilationDao.IsAnyInProgressAsync(firmId, settlementAccountId);
        }

        public Task<Guid?> GetLastSessionInProcessAsync(int firmId, int settlementAccountId)
        {
            return balanceReconcilationDao.GetLastSessionInProcessAsync(firmId, settlementAccountId);
        }

        public async Task<BalanceReconcilation[]> GetByDateAsync(int firmId, IReadOnlyCollection<int> settlementAccountIds, DateTime date)
        {
            var result = await balanceReconcilationDao.GetByDateAsync(firmId, settlementAccountIds, date).ConfigureAwait(false);

            return result.ToArray();
        }

        private async Task<string> SaveToMongoAsync(IUserContext userContext, Guid sessionId, SettlementAccount settlementAccount, DateTime createDate, IReadOnlyCollection<string> documentsForImport)
        {
            var builder = new StringBuilder();
            builder.AppendLine("1CClientBankExchange");
            builder.AppendLine("Кодировка=Windows");
            builder.AppendLine($"ДатаСоздания={createDate:dd.MM.yyyy}");
            builder.AppendLine($"ВремяСоздания={createDate:HH:mm:ss}");
            //builder.AppendLine($"ДатаНачала={1:dd.MM.yyyy}", transfers.StartDate);
            //builder.AppendLine($"ДатаКонца={1:dd.MM.yyyy}", transfers.EndDate);
            builder.AppendLine($"РасчСчет={settlementAccount.Number}");

            foreach (var documentsForImpor in documentsForImport)
            {
                builder.AppendLine(documentsForImpor);
            }

            var content = Encoding.UTF8.GetBytes(builder.ToString());

            var mongoId = await movementListReconciliationStorageClient.SaveAsync(new SaveMovementListDto
            {
                FileName = $@"reconciliation\{userContext.FirmId}\{sessionId}",
                FileData = content,
                FirmId = userContext.FirmId
            }).ConfigureAwait(false);
            return mongoId;
        }
    }
}
