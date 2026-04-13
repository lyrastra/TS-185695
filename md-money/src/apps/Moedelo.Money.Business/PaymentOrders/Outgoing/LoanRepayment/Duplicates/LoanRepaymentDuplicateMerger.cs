using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanRepayment)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class LoanRepaymentDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ILoanRepaymentReader reader;
        private readonly ILoanRepaymentUpdater updater;

        public LoanRepaymentDuplicateMerger(
            ILoanRepaymentReader reader,
            ILoanRepaymentUpdater updater)
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
            var saveRequest = LoanRepaymentMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
