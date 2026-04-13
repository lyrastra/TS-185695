using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission;
using Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Events;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.AccountingStatements
{
    public class AcquiringCommissionAccountingStatementEventHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IAcquiringCommissionAccountingStatementEventReader commandReader;
        private readonly IRetailRevenueEventWriter retailRevenueEventWriter;

        public AcquiringCommissionAccountingStatementEventHostedService(
            ILogger<AcquiringCommissionAccountingStatementEventHostedService> logger,
            IAcquiringCommissionAccountingStatementEventReader commandReader,
            IRetailRevenueEventWriter retailRevenueEventWriter)
        {
            this.logger = logger;
            this.commandReader = commandReader;
            this.retailRevenueEventWriter = retailRevenueEventWriter;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);
           
            await commandReader.ReadAsync(MoneyConstants.GroupId, OnCreate, OnDelete, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreate(AcquiringCommissionCreatedMessage message, KafkaMessageValueMetadata metadata)
        {
            var request = new RetailRevenueAfterAccountingAtatementCreatedUpdateRequest
            {
                DocumentBaseId = message.PaymentBaseId,
                AccountingStatementDate = message.Date,
                AccountingStatementSum = message.Sum,
                AccountingStatementBaseId = message.DocumentBaseId
            };
            await retailRevenueEventWriter.WriteUpdateAfterAccountingAtatementCreatedEventAsync(request);
        }

        private Task OnDelete(AcquiringCommissionDeletedMessage message, KafkaMessageValueMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}
