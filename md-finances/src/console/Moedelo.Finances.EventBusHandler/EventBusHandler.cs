using Moedelo.AccountV2.Client.ConsoleUser;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Integrations;
using Moedelo.Finances.Client.Money;
using Moedelo.Finances.Client.Money.Dtos;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Models.EventBus;
using System;
using System.Threading.Tasks;

namespace Moedelo.Finances.EventBusHandler
{
    [InjectAsSingleton]
    public class EventBusHandler : IDI, IDisposable
    {
        private const string TAG = nameof(EventBusHandler);
        private const string ConsoleName = "MoedeloFinancesApiEventBusHandler";
        private readonly TimeSpan delayOnRetry = TimeSpan.FromHours(24);
        private const int RetriesLimit = 30;

        private readonly ILogger logger;
        private readonly IConsumerFactory consumerFactory;
        private readonly IConsoleUserApiClient consoleUserApi;
        private readonly IMoneyReconciliationClient moneyReconciliationClient;

        public EventBusHandler(
            ILogger logger,
            IConsumerFactory consumerFactory,
            IConsoleUserApiClient consoleUserApi,
            IMoneyReconciliationClient moneyReconciliationClient)
        {
            this.logger = logger;
            this.consumerFactory = consumerFactory;
            this.consoleUserApi = consoleUserApi;
            this.moneyReconciliationClient = moneyReconciliationClient;
        }

        public void ProcessStart()
        {
            logger.Info(TAG, $"Console \"{ConsoleName}\" is starting");
            var consoleUser = consoleUserApi.GetOrCreateByLoginAsync(ConsoleName).Result;
            if (consoleUser == null)
            {
                logger.Error(TAG, $"User \"{ConsoleName}\" is not found");
                return;
            }

            var consumetWithRetryOptions = new ConsumerWithRetryOptions(RetriesLimit, _ => delayOnRetry);
            var consumerStartTaskList = new[]
            {
                consumerFactory.StartConsumerWithRetryAsync(EventBusMessages.IntegrationsMovementListReviseEvent, (m, r) => CreateReconciliationReportAndSendEmailAsync(m, r, consoleUser.Id), consumetWithRetryOptions),
                consumerFactory.StartConsumerWithRetryAsync(EventBusMessages.IntegrationsMovementListReviseForUserEvent, (m, r) => DoReconciliationForUserAsync(m, r, consoleUser.Id), consumetWithRetryOptions)
            };
            Task.WhenAll(consumerStartTaskList).Wait();
            logger.Info(TAG, $"Console \"{ConsoleName}\" is started");
        }

        private async Task CreateReconciliationReportAndSendEmailAsync(MovementListReviseEvent m, uint r, int userId)
        {
            logger.Info(TAG, $"Got MovementListReviseEvent {r}", extraData: m);

            if (r > RetriesLimit)
            {
                logger.Info(TAG, $"Reconciliation stopped because of the limit on the number of attempts to retry ({r})", extraData: m);
                return;
            }

            try
            {
                var request = new ReconciliationForBackofficeRequestDto
                {
                    Email = m.Email,
                    Login = m.Login,
                    FirmId = m.FirmId,
                    FileId = m.MongoObjectId,
                    SettlementNumber = m.SettlementNumber,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Status = m.Status
                };
                logger.Info(TAG, "Started reconciliation for backoffice", extraData: request);
                await moneyReconciliationClient.ReconcileForBackofficeAsync(m.FirmId, userId, request).ConfigureAwait(false);
                logger.Info(TAG, "Finished reconciliation for backoffice", extraData: request);
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex, extraData: m);
                throw;
            }
        }

        private async Task DoReconciliationForUserAsync(MovementListReviseForUserEvent m, uint r, int userId)
        {
            logger.Info(TAG, $"Got MovementListReviseForUserEvent {r}", extraData: m);

            if (r > RetriesLimit)
            {
                logger.Info(TAG, $"Reconciliation stopped because of the limit on the number of attempts to retry ({r})", extraData: m);
                return;
            }

            try
            {
                var request = new ReconciliationForUserRequestDto
                {
                    FirmId = m.FirmId,
                    FileId = m.MongoObjectId,
                    SettlementNumber = m.SettlementNumber,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Status = m.Status,
                    SessionId = m.Guid,
                    IsManual = m.IsManual
                };
                logger.Info(TAG, "Started reconciliation for user", extraData: request);
                await moneyReconciliationClient.ReconcileForUserAsync(m.FirmId, userId, request).ConfigureAwait(false);
                logger.Info(TAG, "Finished reconciliation for user", extraData: request);
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex, extraData: m);
                throw;
            }
        }

        public void ProcessStop()
        {
        }

        public void Dispose()
        {
        }
    }
}