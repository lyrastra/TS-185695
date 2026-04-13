using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class OutgoingCurrencyPurchaseDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IOutgoingCurrencyPurchaseReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IOutgoingCurrencyPurchaseUpdater updater;

        public OutgoingCurrencyPurchaseDuplicateImporter(
            IOutgoingCurrencyPurchaseReader reader,
            ITaxPostingReader taxPostingReader,
            IOutgoingCurrencyPurchaseUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = OutgoingCurrencyPurchaseMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            saveRequest.ProvideInAccounting = true;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
