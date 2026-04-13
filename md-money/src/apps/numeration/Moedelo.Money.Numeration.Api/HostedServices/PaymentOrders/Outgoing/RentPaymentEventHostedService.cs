using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Numeration.Api.HostedServices.LoggerExtension;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.AspNetCore.Mvc.Attributes;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events;
using Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Settings.Abstractions;

namespace Moedelo.Money.Numeration.Api.HostedServices.PaymentOrders.Outgoing;

[InjectAsHostedService]
public class RentPaymentEventHostedService : BackgroundService
{
    private readonly ILogger logger;
    private readonly IRentPaymentEventReader eventReader;
    private readonly INumberSetter numberSetter;
    private readonly SettingValue consumerCountSetting;

    public RentPaymentEventHostedService(
        ILogger<RentPaymentEventHostedService> logger,
        IRentPaymentEventReader eventReader,
        INumberSetter numberSetter,
        ISettingRepository settingRepository)
    {
        this.logger = logger;
        this.eventReader = eventReader;
        this.numberSetter = numberSetter;
        consumerCountSetting = settingRepository.Get("MoneyEventConsumerCount");
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogHostedServiceIsStarting(GetType().Name);
        var consumerCount = consumerCountSetting.GetIntValueOrDefault(NumerationConstants.ConsumerCount);

        await eventReader
            .ReadAsync(NumerationConstants.GroupId, OnCreated, OnUpdated, OnDeleted, consumerCount: consumerCount, cancellationToken: cancellationToken);

        logger.LogHostedServiceIsStopped(GetType().Name);
    }

    private async Task OnCreated(RentPaymentCreatedMessage eventData, KafkaMessageValueMetadata valueMetadata)
    {
        if (!int.TryParse(eventData.Number, out int lastNumber))
        {
            return;
        }
        await numberSetter.Last(eventData.SettlementAccountId, eventData.Date.Year, lastNumber);
    }

    private Task OnUpdated(RentPaymentUpdatedMessage eventData, KafkaMessageValueMetadata valueMetadata)
    {
        return Task.CompletedTask;
    }

    private Task OnDeleted(RentPaymentDeletedMessage eventData, KafkaMessageValueMetadata valueMetadata)
    {
        return Task.CompletedTask;
    }
}