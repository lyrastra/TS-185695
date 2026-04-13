using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingLoanReturn)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class LoanReturnDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ILoanReturnReader reader;
        private readonly ITaxPostingReader taxPostingReader;
        private readonly ILoanReturnUpdater updater;

        public LoanReturnDuplicateImporter(
            ILoanReturnReader reader,
            ITaxPostingReader taxPostingReader,
            ILoanReturnUpdater updater)
        {
            this.reader = reader;
            this.taxPostingReader = taxPostingReader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = LoanReturnMapper.MapToSaveRequest(response);
            saveRequest.TaxPostings = response.TaxPostingsInManualMode
                ? await taxPostingReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false)
                : new TaxPostingsData();
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
