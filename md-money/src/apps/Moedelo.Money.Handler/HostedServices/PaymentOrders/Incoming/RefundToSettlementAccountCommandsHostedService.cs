using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Commands;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class RefundToSettlementAccountCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IRefundToSettlementAccountCommandReaderBuilder builder;
        private readonly SettingValue consumerCountSetting;
        private readonly IRefundToSettlementAccountImporter importer;

        public RefundToSettlementAccountCommandsHostedService(ILogger<RefundToSettlementAccountCommandsHostedService> logger,
            IRefundToSettlementAccountCommandReaderBuilder builder,
            ISettingRepository settingRepository,
            IRefundToSettlementAccountImporter importer)
        {
            this.logger = logger;
            this.builder = builder;
            this.importer = importer;

            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await builder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, stoppingToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportRefundToSettlementAccount commandData)
        {
            var request = RefundToSettlementAccountMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportRefundToSettlementAccountDuplicate commandData)
        {
            var request = RefundToSettlementAccountMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportWithMissingContractorAsync(ImportRefundToSettlementAccountWithMissingContragent commandData)
        {
            var request = RefundToSettlementAccountMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
