using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Providing.Api.Mappers.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Providing.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.ErrorTolerance;
using Moedelo.Common.Kafka.ErrorTolerance.PostgreSql;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;

namespace Moedelo.Money.Providing.Api.HostedServices.PaymentOrders.Incoming
{
    [InjectAsHostedService]
    public class PaymentFromCustomerHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IPaymentFromCustomerEventReaderBuilder eventReaderBuilder;
        private readonly IPaymentFromCustomerProvider provider;
        private readonly IPaymentFromCustomerUnprovider unprovider;
        private readonly SettingValue consumerCountSetting;

        public PaymentFromCustomerHostedService(
            ILogger<PaymentFromCustomerHostedService> logger,
            IPaymentFromCustomerEventReaderBuilder eventReaderBuilder,
            IPaymentFromCustomerProvider provider,
            IPaymentFromCustomerUnprovider unprovider,
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
                .OnSetReserve(OnSetReserveAsync)
                .SkipIfUnknownType()
                .WithFallbackOffset(KafkaConsumerConfig.AutoOffsetResetType.Latest)
                .WithRetry(new ConsumerActionRetrySettings(4, TimeSpan.FromMinutes(1), retryTimeoutStrategy: TimeoutStategies.ExponentStategy))
                .WithAutoConsumersCount()
                .WithContinueAfterPause(options => options.UsePostgresMemoryStorage())
                .RunAsync(MoneyProvidingConstants.GroupId, cancellationToken);

            return eventTask;
        }

        private Task OnCreated(PaymentFromCustomerCreated eventData)
        {
            var request = PaymentFromCustomerMapper.Map(eventData);
            return provider.ProvideAsync(request);
        }

        private Task OnUpdated(PaymentFromCustomerUpdated eventData)
        {
            var request = PaymentFromCustomerMapper.Map(eventData);
            return provider.ProvideAsync(request);
        }

        private Task OnProvideRequiredAsync(PaymentFromCustomerProvideRequired eventData)
        {
            var request = PaymentFromCustomerMapper.Map(eventData);
            return provider.ProvideAsync(request);
        }

        private Task OnDeletedAsync(PaymentFromCustomerDeleted eventData)
        {
            return unprovider.UnprovideAsync(eventData.DocumentBaseId);
        }

        private Task OnSetReserveAsync(PaymentFromCustomerSetReserve eventData)
        {
            var request = PaymentFromCustomerMapper.Map(eventData);
            return provider.UpdateReserveAsync(request);
        }
    }
}
