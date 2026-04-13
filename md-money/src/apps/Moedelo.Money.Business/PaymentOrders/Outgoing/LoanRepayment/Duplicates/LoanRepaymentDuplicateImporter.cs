using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanRepayment)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class LoanRepaymentDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ILoanRepaymentReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly ILoanRepaymentUpdater updater;

        public LoanRepaymentDuplicateImporter(
            ILoanRepaymentReader reader,
            ITaxPostingReader taxPostingReader,
            ILoanRepaymentUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = LoanRepaymentMapper.MapToSaveRequest(response);
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
