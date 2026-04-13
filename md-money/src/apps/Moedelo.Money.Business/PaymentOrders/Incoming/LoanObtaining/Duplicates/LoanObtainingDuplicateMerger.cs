using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingLoanObtaining)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class LoanObtainingDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ILoanObtainingReader reader;
        private readonly ILoanObtainingUpdater updater;

        public LoanObtainingDuplicateMerger(
            ILoanObtainingReader reader,
            ILoanObtainingUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = LoanObtainingMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
