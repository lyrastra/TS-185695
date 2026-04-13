using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class CurrencyPaymentFromCustomerHostedCommandsService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyPaymentFromCustomerCommandReaderBuilder commandReaderBuilder;
        private readonly ICurrencyPaymentFromCustomerImporter importer;
        private readonly SettingValue consumerCountSetting;

        public CurrencyPaymentFromCustomerHostedCommandsService(
            ILogger<CurrencyPaymentFromCustomerHostedCommandsService> logger,
            ICurrencyPaymentFromCustomerCommandReaderBuilder commandReaderBuilder,
            ICurrencyPaymentFromCustomerImporter importer,
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
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .OnImportWithMissingExchangeRate(OnImportWithMissingExchangeRateAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportCurrencyPaymentFromCustomer commandData)
        {
            var request = CurrencyPaymentFromCustomerMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateCurrencyPaymentFromCustomer commandData)
        {
            var request = CurrencyPaymentFromCustomerMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingContractorAsync(ImportWithMissingContractorCurrencyPaymentFromCustomer commandData)
        {
            var request = CurrencyPaymentFromCustomerMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingExchangeRateAsync(ImportWithMissingExchangeRateCurrencyPaymentFromCustomer commandData)
        {
            var request = CurrencyPaymentFromCustomerMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
