using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moedelo.AccountingStatements.Kafka.Abstractions.TradingFee;
using Moedelo.AccountingStatements.Kafka.Abstractions.TradingFee.Events;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Handler.Extensions;

namespace Moedelo.Money.Handler.HostedServices.AccountingStatements
{
    public class TradingFeeAccountingStatementEventHostedService : BackgroundService
    {
        private readonly ILogger logger;
        private readonly ITradingFeeAccountingStatementEventReader commandReader;
        private readonly IBudgetaryPaymentEventWriter budgetaryPaymentEventWriter;
        private readonly IUnifiedBudgetaryPaymentEventWriter unifiedBudgetaryPaymentEventWriter;
        private readonly IPaymentOrderGetter paymentOrderGetter;

        public TradingFeeAccountingStatementEventHostedService(
            ILogger<TradingFeeAccountingStatementEventHostedService> logger,
            ITradingFeeAccountingStatementEventReader commandReader,
            IBudgetaryPaymentEventWriter budgetaryPaymentEventWriter,
            IUnifiedBudgetaryPaymentEventWriter unifiedBudgetaryPaymentEventWriter,
            IPaymentOrderGetter paymentOrderGetter)
        {
            this.logger = logger;
            this.commandReader = commandReader;
            this.budgetaryPaymentEventWriter = budgetaryPaymentEventWriter;
            this.unifiedBudgetaryPaymentEventWriter = unifiedBudgetaryPaymentEventWriter;
            this.paymentOrderGetter = paymentOrderGetter;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogHostedServiceIsStarting(GetType().Name);
           
            await commandReader.ReadAsync(MoneyConstants.GroupId, OnCreate, OnDelete, cancellationToken: cancellationToken);
            logger.LogHostedServiceIsStopped(GetType().Name);
        }

        private async Task OnCreate(TradingFeeCreatedMessage message, KafkaMessageValueMetadata metadata)
        {
            OperationType operationType;

            try
            {
                operationType = await paymentOrderGetter.GetOperationTypeAsync(message.PaymentBaseId);
            }
            catch (OperationNotFoundException)
            {
                // если операция не найдена - значит её уже удалили
                // соответственно к бух справке уже ничего привязать нельзя
                return;
            }

            if (operationType == Enums.OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)
            {
                var request = new UnifiedBudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest
                {
                    DocumentBaseId = message.PaymentBaseId,
                    AccountingStatementDate = message.Date,
                    AccountingStatementSum = message.Sum,
                    AccountingStatementBaseId = message.DocumentBaseId
                };
                await unifiedBudgetaryPaymentEventWriter.WriteUpdateAfterAccountingStatementCreatedEventAsync(request);
            }
            else 
            {
                var request = new BudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest
                {
                    DocumentBaseId = message.PaymentBaseId,
                    AccountingStatementDate = message.Date,
                    AccountingStatementSum = message.Sum,
                    AccountingStatementBaseId = message.DocumentBaseId
                };
                await budgetaryPaymentEventWriter.WriteUpdateAfterAccountingStatementCreatedEventAsync(request);
            }
        }

        private Task OnDelete(TradingFeeDeletedMessage message, KafkaMessageValueMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}
