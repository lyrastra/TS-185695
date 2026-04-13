using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Commands;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class LoanObtainingCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ILoanObtainingCommandReaderBuilder commandReaderBuilder;
        private readonly ILoanObtainingImporter importer;
        private readonly SettingValue consumerCountSetting;

        public LoanObtainingCommandsHostedService(
            ILogger<LoanObtainingCommandsHostedService> logger,
            ILoanObtainingCommandReaderBuilder commandReaderBuilder,
            ILoanObtainingImporter importer,
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
                .OnImportWithMissingContract(OnImportWithMissingContractAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportLoanObtaining commandData)
        {
            var request = LoanObtainingMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateLoanObtaining commandData)
        {
            var request = LoanObtainingMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportWithMissingContractAsync(ImportWithMissingContractLoanObtaining commandData)
        {
            var request = LoanObtainingMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorLoanObtaining commandData)
        {
            var request = LoanObtainingMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }
    }
}
