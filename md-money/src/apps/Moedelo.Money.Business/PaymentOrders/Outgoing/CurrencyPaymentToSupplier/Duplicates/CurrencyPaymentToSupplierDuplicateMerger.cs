using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class CurrencyPaymentToSupplierDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ICurrencyPaymentToSupplierReader reader;
        private readonly ICurrencyPaymentToSupplierUpdater updater;
        private readonly ITaxPostingReader taxPostingReader;

        public CurrencyPaymentToSupplierDuplicateMerger(
            ICurrencyPaymentToSupplierReader reader,
            ICurrencyPaymentToSupplierUpdater updater,
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
            var saveRequest = CurrencyPaymentToSupplierMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
