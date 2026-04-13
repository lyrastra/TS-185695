using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingDeduction)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal class DeductionDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IDeductionReader reader;
        private readonly IDeductionUpdater updater;
        private readonly DeductionAccPostingsGetter deductionAccPostingsGetter;

        public DeductionDuplicateMerger(
            IDeductionReader reader,
            IDeductionUpdater updater, 
            DeductionAccPostingsGetter deductionAccPostingsGetter)
        {
            this.reader = reader;
            this.updater = updater;
            this.deductionAccPostingsGetter = deductionAccPostingsGetter;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = DeductionMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            saveRequest.AccountingPosting =
                await deductionAccPostingsGetter.GetAsync(DeductionMapper.MapToAccPostingRequest(response));
            await updater.UpdateAsync(saveRequest);
        }
    }
}
