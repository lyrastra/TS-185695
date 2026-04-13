using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Maintenance;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.BankBalanceHistory.DataAccess.Abstractions;
using Moedelo.Money.BankBalanceHistory.Api.Extensions;

namespace Moedelo.Money.BankBalanceHistory.Api.HostedServices
{
    /// <summary>
    /// Очистка данных удаленных фирм
    /// </summary>
    [InjectAsHostedService]
    public class FirmMaintenanceHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IFirmMaintenanceEventReaderBuilder eventReaderBuilder;
        private readonly IBalancesDao balancesDao;

        public FirmMaintenanceHostedService(
            ILogger<FirmMaintenanceHostedService> logger,
            IFirmMaintenanceEventReaderBuilder eventReaderBuilder,
            IBalancesDao balancesDao)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.balancesDao = balancesDao;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await eventReaderBuilder.OnFirmsWereDeletedEvent(OnFirmsWereDeletedEvent)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(6, TimeSpan.FromMinutes(10)))
                .WithConsumerCount(1)
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnFirmsWereDeletedEvent(FirmsWereDeletedEvent firmsWereDeletedEvent)
        {
            return balancesDao.CleanUpByFirmIdsAsync(firmsWereDeletedEvent.FirmIds);
        }
    }
}
