using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using IPaymentToSupplierProvider = Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.IPaymentToSupplierProvider;

namespace Moedelo.Money.Providing.Api.HostedServices.PaymentOrders.Outgoing
{
    [InjectAsHostedService]
    public class PaymentToSupplierHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPaymentToSupplierEventReaderBuilder eventReaderBuilder;
        private readonly IPaymentToSupplierProvider provider;
        private readonly IPaymentToSupplierUnprovider unprovider;
        private readonly SettingValue consumerCountSetting;

        public PaymentToSupplierHostedService(
            ILogger<PaymentToSupplierHostedService> logger,
            IPaymentToSupplierEventReaderBuilder eventReaderBuilder,
            IPaymentToSupplierProvider provider,
            IPaymentToSupplierUnprovider unprovider,
            ISettingRepository settingRepository)
        {
            this.logger = logger;
            this.eventReaderBuilder = eventReaderBuilder;
            this.provider = provider;
            this.unprovider = unprovider;
            consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var consumerCount = consumerCountSetting.GetIntValueOrDefault(1);
            logger.LogInformation($"Started consumer with group {MoneyProvidingConstants.GroupId} consumerCount:{consumerCount}");

            var eventTask = eventReaderBuilder
                .OnCreated(OnCreated)
                .OnUpdated(OnUpdated)
                .OnProvideRequired(OnProvideRequiredAsync)
                .OnDeleted(OnDeletedAsync)
                .OnSetReserveAsync(OnSetReserveAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(5, TimeSpan.FromMinutes(1)))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyProvidingConstants.GroupId, cancellationToken);

            return eventTask;
        }

        private Task OnCreated(PaymentToSupplierCreated eventData)
        {
            var request = PaymentToSupplierMapper.Map(eventData);
            return provider.ProvideAsync(request);
        }

        private Task OnUpdated(PaymentToSupplierUpdated eventData)
        {
            var request = PaymentToSupplierMapper.Map(eventData);
            return provider.ProvideAsync(request);
        }

        private Task OnProvideRequiredAsync(PaymentToSupplierProvideRequired eventData)
        {
            var request = PaymentToSupplierMapper.Map(eventData);
            return provider.ProvideAsync(request);
        }

        private Task OnDeletedAsync(PaymentToSupplierDeleted eventData)
        {
            return unprovider.UnprovideAsync(eventData.DocumentBaseId);
        }

        private Task OnSetReserveAsync(PaymentToSupplierSetReserve eventData)
        {
            var request = PaymentToSupplierMapper.Map(eventData);
            return provider.UpdateReserveAsync(request);
        }
    }
}
