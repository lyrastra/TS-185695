using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Outgoing
{
    public class LoanIssueCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ILoanIssueCommandReaderBuilder commandReaderBuilder;
        private readonly ILoanIssueImporter importer;

        public LoanIssueCommandsHostedService(
            ILogger<LoanIssueCommandsHostedService> logger,
            ILoanIssueCommandReaderBuilder commandReaderBuilder,
            ILoanIssueImporter importer)
        {
            this.logger = logger;
            this.commandReaderBuilder = commandReaderBuilder;
            this.importer = importer;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);

            await commandReaderBuilder
                .OnImport(OnImportAsync)
                .OnImportDuplicate(OnImportDuplicateAsync)
                .OnImportWithMissingContract(OnImportWithMissingContractAsync)
                .OnImportWithMissingContractor(OnImportWithMissingContractorAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnImportAsync(ImportLoanIssue commandData)
        {
            var request = LoanIssueMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportDuplicateAsync(ImportDuplicateLoanIssue commandData)
        {
            var request = LoanIssueMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportWithMissingContractAsync(ImportWithMissingContractLoanIssue commandData)
        {
            var request = LoanIssueMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }

        private async Task OnImportWithMissingContractorAsync(ImportWithMissingContractorLoanIssue commandData)
        {
            var request = LoanIssueMapper.Map(commandData);
            await importer.ImportAsync(request).ConfigureAwait(false);
        }
    }
}
