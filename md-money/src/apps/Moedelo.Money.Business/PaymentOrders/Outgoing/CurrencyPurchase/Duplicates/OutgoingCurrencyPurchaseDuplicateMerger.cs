using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class OutgoingCurrencyPurchaseDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IOutgoingCurrencyPurchaseReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IOutgoingCurrencyPurchaseUpdater updater;

        public OutgoingCurrencyPurchaseDuplicateMerger(
            IOutgoingCurrencyPurchaseReader reader,
            ITaxPostingReader taxPostingReader,
            IOutgoingCurrencyPurchaseUpdater updater)
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
            var saveRequest = OutgoingCurrencyPurchaseMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.Date = request.Date;
            saveRequest.ProvideInAccounting = true;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
