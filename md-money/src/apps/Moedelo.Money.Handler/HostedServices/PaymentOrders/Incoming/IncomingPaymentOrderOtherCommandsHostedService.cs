using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Other;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Handler.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands;
using Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

namespace Moedelo.Money.Handler.HostedServices.PaymentOrders.Incoming
{
    public class IncomingPaymentOrderOtherCommandsHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IIncomingPaymentOrderOtherCommandReaderBuilder builder;
        private readonly SettingValue consumerCountSetting;
        private readonly IOtherIncomingImporter importer;

        public IncomingPaymentOrderOtherCommandsHostedService(ILogger<IncomingPaymentOrderOtherCommandsHostedService> logger,
            IIncomingPaymentOrderOtherCommandReaderBuilder builder,
            ISettingRepository settingRepository,
            IOtherIncomingImporter importer)
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

        private async Task OnImportAsync(ImportOtherIncoming commandData)
        {
            var request = IncomingOtherMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        private async Task OnImportDuplicateAsync(ImportOtherIncomingDuplicate commandData)
        {
            var request = IncomingOtherMapper.Map(commandData);
            await importer.ImportAsync(request);
        }

        // todo: удалить команду и связанный код после релиза AP-10710 (встроено в ImportOtherOutgoing)
        private async Task OnImportWithMissingContractorAsync(ImportOtherIncomingWithMissingContragent commandData)
        {
            var request = IncomingOtherMapper.Map(commandData);
            await importer.ImportAsync(request);
        }
    }
}
