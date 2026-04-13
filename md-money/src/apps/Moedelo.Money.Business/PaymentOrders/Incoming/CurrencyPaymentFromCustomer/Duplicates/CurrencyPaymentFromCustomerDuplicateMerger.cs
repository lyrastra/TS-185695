using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class CurrencyPaymentFromCustomerDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ICurrencyPaymentFromCustomerReader reader;
        private readonly ICurrencyPaymentFromCustomerUpdater updater;
        private readonly ITaxPostingReader taxPostingReader;

        public CurrencyPaymentFromCustomerDuplicateMerger(
            ICurrencyPaymentFromCustomerReader reader,
            ICurrencyPaymentFromCustomerUpdater updater,
            ITaxPostingReader taxPostingReader)
        {
            this.reader = reader;
            this.updater = updater;
            this.taxPostingReader = taxPostingReader;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = CurrencyPaymentFromCustomerMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
