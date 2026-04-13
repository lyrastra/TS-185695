using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [OperationType(OperationType.PaymentOrderIncomingPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IPaymentFromCustomerProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderBatchProvider))]
    class PaymentFromCustomerProvider : IPaymentFromCustomerProvider,
        IConcretePaymentOrderProvider,
        IConcretePaymentOrderBatchProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IPaymentFromCustomerReader reader;
        private readonly IPaymentFromCustomerEventWriter eventWriter;

        public PaymentFromCustomerProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentFromCustomerReader reader,
            IPaymentFromCustomerEventWriter eventWriter)
        {
            this.reader = reader;
            this.closedPeriodValidator = closedPeriodValidator;
            this.eventWriter = eventWriter;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }
            // костыль, так как не получается перепровести первичку в отрыве от п/п
            // await closedPeriodValidator.ValidateAsync(response.Date);
            // Если Документ повторяется то мы упадем при проведении в Moedelo.AccountingStatements.Api в FromMoney_PaymentFromCustomerEventsReadHostedService на ToDictionary
            // нужно исследовать задвояшки
            var documentLinksId = response.Documents?.Data?.Select(l => l.DocumentBaseId).ToList();
            await DocumentLinksDuplicateValidator.Validate(documentLinksId);
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }

        public async Task ProvideAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var operations = await reader.GetByBaseIdsAsync(documentBaseIds);
            var validStateOperations = operations
                .Where(x => !x.OperationState.IsBadOperationState())
                .ToArray();
            foreach (var operation in validStateOperations)
            {
                // костыль, так как не получается перепровести первичку в отрыве от п/п
                // await closedPeriodValidator.ValidateAsync(response.Date);
                // Если Документ повторяется то мы упадем при проведении в Moedelo.AccountingStatements.Api в FromMoney_PaymentFromCustomerEventsReadHostedService на ToDictionary
                // нужно исследовать задвояшки
                var documentLinksId = operation.Documents?.Data?.Select(l => l.DocumentBaseId).ToList();
                await DocumentLinksDuplicateValidator.Validate(documentLinksId);
                await eventWriter.WriteProvideRequiredEventAsync(operation);
            }
        }
    }
}
