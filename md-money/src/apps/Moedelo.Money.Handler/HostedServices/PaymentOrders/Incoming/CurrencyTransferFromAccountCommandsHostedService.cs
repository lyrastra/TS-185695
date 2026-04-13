using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class CurrencyTransferFromAccountCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ICurrencyTransferFromAccountCommandReaderBuilder commandReaderBuilder;
        private readonly ICurrencyTransferFromAccountImporter importer;
        private readonly SettingValue consumerCountSetting;

        public CurrencyTransferFromAccountCommandsHostedService(
            ILogger<CurrencyTransferFromAccountCommandsHostedService> logger,
            ICurrencyTransferFromAccountCommandReaderBuilder commandReaderBuilder,
            ICurrencyTransferFromAccountImporter importer,
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
                .OnImportWithMissingCurrencySettlementAccount(OnImportWithMissingCurrencySettlementAccountAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportCurrencyTransferFromAccount commandData)
        {
            var request = CurrencyTransferFromAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateCurrencyTransferFromAccount commandData)
        {
            var request = CurrencyTransferFromAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingCurrencySettlementAccountAsync(ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount commandData)
        {
            var request = CurrencyTransferFromAccountMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
