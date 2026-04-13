using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [OperationType(OperationType.PaymentOrderOutgoingRefundToCustomer)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class RefundToCustomerProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IRefundToCustomerReader reader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RefundToCustomerEventWriter eventWriter;

        public RefundToCustomerProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IRefundToCustomerReader reader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RefundToCustomerEventWriter eventWriter)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.reader = reader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.eventWriter = eventWriter;
        }

        public async Task ProvideAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            if (response.OperationState.IsBadOperationState())
            {
                return;
            }
            await closedPeriodValidator.ValidateAsync(response.Date);
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year).ConfigureAwait(false);
            }
            await eventWriter.WriteProvideRequiredEventAsync(response);
        }
    }
}
