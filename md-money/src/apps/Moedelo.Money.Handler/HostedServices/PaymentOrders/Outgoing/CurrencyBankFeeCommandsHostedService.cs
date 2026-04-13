using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class CurrencyBankFeeCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyBankFeeCommandReaderBuilder commandReaderBuilder;
        private readonly ICurrencyBankFeeImporter importer;
        private readonly SettingValue consumerCountSetting;

        public CurrencyBankFeeCommandsHostedService(
            ILogger<CurrencyBankFeeCommandsHostedService> logger,
            ICurrencyBankFeeCommandReaderBuilder commandReaderBuilder,
            ICurrencyBankFeeImporter importer,
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
                .OnImportWithMissingExchangeRate(OnImportWithMissingExchangeRateAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportCurrencyBankFee commandData)
        {
            var request = CurrencyBankFeeMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateCurrencyBankFee commandData)
        {
            var request = CurrencyBankFeeMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingExchangeRateAsync(ImportWithMissingExchangeRateCurrencyBankFee commandData)
        {
            var request = CurrencyBankFeeMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
