using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class BankFeeCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IBankFeeCommandReaderBuilder commandReaderBuilder;
        private readonly IBankFeeImporter importer;
        private readonly SettingValue consumerCountSetting;

        public BankFeeCommandsHostedService(
            ILogger<BankFeeCommandsHostedService> logger,
            IBankFeeCommandReaderBuilder commandReaderBuilder,
            IBankFeeImporter importer,
            ISettingRepository settingRepository)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.importer = importer;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportBankFee commandData)
        {
            var request = BankFeeMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateBankFee commandData)
        {
            var request = BankFeeMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
