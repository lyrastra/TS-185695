using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
    [InjectAsSingleton(typeof(IPaymentToSupplierProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderBatchProvider))]
    class PaymentToSupplierProvider : IPaymentToSupplierProvider,
        IConcretePaymentOrderProvider,
        IConcretePaymentOrderBatchProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IPaymentToSupplierReader reader;
        private readonly IPaymentToSupplierEventWriter paymentToSupplierEventWriter;

        public PaymentToSupplierProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IPaymentToSupplierReader reader,
            IPaymentToSupplierEventWriter paymentToSupplierEventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.reader = reader;
            this.paymentToSupplierEventWriter = paymentToSupplierEventWriter;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }
            // костыль, так как не получается перепровести первичку в отрыве от п/п
            //await closedPeriodValidator.ValidateAsync(response.Date).ConfigureAwait(false);
            await paymentToSupplierEventWriter.WriteProvideRequiredEventAsync(response);
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
                //await closedPeriodValidator.ValidateAsync(response.Date).ConfigureAwait(false);
                await paymentToSupplierEventWriter.WriteProvideRequiredEventAsync(operation);
            }
        }
    }
}
