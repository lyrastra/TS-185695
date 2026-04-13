using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingLoanObtaining)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class LoanObtainingDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly ILoanObtainingReader reader;
        private readonly ILoanObtainingUpdater updater;

        public LoanObtainingDuplicateImporter(
            ILoanObtainingReader reader,
            ILoanObtainingUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = LoanObtainingMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
