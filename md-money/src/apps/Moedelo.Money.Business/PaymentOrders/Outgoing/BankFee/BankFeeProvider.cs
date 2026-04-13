using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.PaymentOrders.Providing;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee
{
    [OperationType(OperationType.MemorialWarrantBankFee)]
    [InjectAsSingleton(typeof(IConcretePaymentOrderProvider))]
    class BankFeeProvider : IConcretePaymentOrderProvider
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly IBankFeeReader reader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly BankFeeEventWriter eventWriter;

        public BankFeeProvider(
            IClosedPeriodValidator closedPeriodValidator,
            IBankFeeReader reader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            BankFeeEventWriter eventWriter)
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
