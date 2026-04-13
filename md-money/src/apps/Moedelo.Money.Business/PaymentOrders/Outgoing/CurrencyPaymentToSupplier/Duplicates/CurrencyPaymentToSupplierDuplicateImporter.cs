using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    class CurrencyPaymentToSupplierDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ICurrencyPaymentToSupplierReader reader;
        private readonly ICurrencyPaymentToSupplierUpdater updater;
        private readonly ITaxPostingReader taxPostingReader;

        public CurrencyPaymentToSupplierDuplicateImporter(
            ICurrencyPaymentToSupplierReader reader,
            ITaxPostingReader taxPostingReader,
            ICurrencyPaymentToSupplierUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
            this.taxPostingReader = taxPostingReader;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = CurrencyPaymentToSupplierMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.ProvideInAccounting = true;
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
