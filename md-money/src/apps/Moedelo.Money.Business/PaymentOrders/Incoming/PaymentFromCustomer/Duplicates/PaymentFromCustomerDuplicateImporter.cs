using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class PaymentFromCustomerDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IPaymentFromCustomerReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IPaymentFromCustomerUpdater updater;

        public PaymentFromCustomerDuplicateImporter(
            IPaymentFromCustomerReader reader,
            ITaxPostingReader taxPostingReader,
            IPaymentFromCustomerUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = PaymentFromCustomerMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
