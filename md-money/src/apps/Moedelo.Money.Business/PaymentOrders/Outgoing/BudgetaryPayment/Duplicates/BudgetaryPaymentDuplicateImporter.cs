using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Duplicates
{
    [OperationType(OperationType.BudgetaryPayment)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal class BudgetaryPaymentDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IBudgetaryPaymentReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly IBudgetaryPaymentUpdater updater;

        public BudgetaryPaymentDuplicateImporter(
            IBudgetaryPaymentReader reader,
            ITaxPostingReader taxPostingReader,
            IBudgetaryPaymentUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = BudgetaryPaymentMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.IsPaid = true;
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
