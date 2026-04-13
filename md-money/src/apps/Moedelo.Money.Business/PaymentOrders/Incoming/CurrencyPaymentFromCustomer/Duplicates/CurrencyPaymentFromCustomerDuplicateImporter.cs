using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class CurrencyPaymentFromCustomerDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ICurrencyPaymentFromCustomerReader reader;
        private readonly ICurrencyPaymentFromCustomerUpdater updater;
        private readonly ITaxPostingReader taxPostingReader;

        public CurrencyPaymentFromCustomerDuplicateImporter(
            ICurrencyPaymentFromCustomerReader reader,
            ITaxPostingReader taxPostingReader,
            ICurrencyPaymentFromCustomerUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
            this.taxPostingReader = taxPostingReader;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = CurrencyPaymentFromCustomerMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
