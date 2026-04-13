using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingDeduction)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal class DeductionDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IDeductionReader reader;
        private readonly IDeductionUpdater updater;
        private readonly DeductionAccPostingsGetter deductionAccPostingsGetter;

        public DeductionDuplicateImporter(
            IDeductionReader reader,
            IDeductionUpdater updater, 
            DeductionAccPostingsGetter deductionAccPostingsGetter)
        {
            this.reader = reader;
            this.updater = updater;
            this.deductionAccPostingsGetter = deductionAccPostingsGetter;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = DeductionMapper.MapToSaveRequest(response);
            saveRequest.IsPaid = true;
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            saveRequest.AccountingPosting =
                await deductionAccPostingsGetter.GetAsync(DeductionMapper.MapToAccPostingRequest(response));
            await updater.UpdateAsync(saveRequest);
        }
    }
}
