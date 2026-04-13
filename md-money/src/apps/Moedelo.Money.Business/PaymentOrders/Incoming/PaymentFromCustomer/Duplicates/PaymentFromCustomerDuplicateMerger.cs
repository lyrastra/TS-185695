using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class PaymentFromCustomerDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IPaymentFromCustomerReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IPaymentFromCustomerUpdater updater;

        public PaymentFromCustomerDuplicateMerger(
            IPaymentFromCustomerReader reader,
            ITaxPostingReader taxPostingReader,
            IPaymentFromCustomerUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = PaymentFromCustomerMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
