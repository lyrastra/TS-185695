using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Commands;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class RefundFromAccountablePersonCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IRefundFromAccountablePersonCommandReaderBuilder commandReaderBuilder;
        private readonly IRefundFromAccountablePersonImporter importer;
        private readonly SettingValue consumerCountSetting;

        public RefundFromAccountablePersonCommandsHostedService(
            ILogger<RefundFromAccountablePersonCommandsHostedService> logger,
            IRefundFromAccountablePersonCommandReaderBuilder commandReaderBuilder,
            IRefundFromAccountablePersonImporter importer,
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
                .OnImportWithMissingEmployee(OnImportWithMissingEmployeeAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(2, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyConstants.GroupId, cancellationToken);

            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private Task OnImportAsync(ImportRefundFromAccountablePerson commandData)
        {
            var request = RefundFromAccountablePersonMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportDuplicateAsync(ImportDuplicateRefundFromAccountablePerson commandData)
        {
            var request = RefundFromAccountablePersonMapper.Map(commandData);
            return importer.ImportAsync(request);
        }

        private Task OnImportWithMissingEmployeeAsync(ImportWithMissingEmployeeRefundFromAccountablePerson commandData)
        {
            var request = RefundFromAccountablePersonMapper.Map(commandData);
            return importer.ImportAsync(request);
        }
    }
}
